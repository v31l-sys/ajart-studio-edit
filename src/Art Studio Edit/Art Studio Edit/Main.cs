using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Ionic.Zlib;
using PcapDotNet.Core;
using PcapDotNet.Packets;

namespace Art_Studio_Edit
{
    public partial class Main : Form
    {
        //Just some global self-describing variables
        static string uuid = "";
        static byte[] _keybytes_ = new byte[0];
        static byte[] _ivbytes_ = new byte[0];

        //Lazy way of thread locking lol
        private volatile bool _shouldStop;

        //AMF3 Parser Implemented in C++/CLI
        static AMF3SpecCLI.AMFParser AMFParser = new AMF3SpecCLI.AMFParser();

        public Main()
        {
            InitializeComponent();
            AMFParser.Initialize();

            /////////////////////////////////////////////////////////////////////////
            /// Setting up automatic audio playback (NAudio)
            MemoryStream mp3file = new MemoryStream(Properties.Resources.PowerISO);
            NAudio.Wave.WaveStream pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(new NAudio.Wave.Mp3FileReader(mp3file));
            NAudio.Wave.BlockAlignReductionStream stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
            NAudio.Wave.DirectSoundOut output = new NAudio.Wave.DirectSoundOut(700); //audio latency (to stop crackling)
            output.PlaybackStopped += (pbss, pbse) => {  System.Threading.Thread.Sleep(1000); stream.Position = 0; output.Play(); };
            output.Init(stream);
            output.Play();
            /////////////////////////////////////////////////////////////////////////

            //users.dat holds captured usernames and uuids locally
            //format: username > xxxxx-xxxx-xxx...(36 character uuid)

            if (!File.Exists("users.dat"))
                File.Create("users.dat").Close();

            foreach(string line in File.ReadAllLines("users.dat"))
            {
                uuid_combobox.Items.Add(line);
            }

            if(uuid_combobox.Items.Count > 0)
                uuid_combobox.SelectedIndex = 0;
        }

        //Toggles packet capturing to get a UUID on or off
        //Reads packets through WinPcap
        private void getuuid_button_Click(object sender, EventArgs e)
        {
            if(uuid_capture.Text == "[UUID Capture is Off]")
            {
                uuid_capture.Text = "[UUID Capture is On]";
                _shouldStop = false;

                IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;
                for (int i = 0; i < allDevices.Count - 1; i++)
                {
                    new Thread(() => { MonitorNetwork(allDevices[i]); }).Start();
                }

            } else
            {
                uuid_capture.Text = "[UUID Capture is Off]";
               _shouldStop = true;
            }
        }

        //Network monitoring function, given a networking interface
        private void MonitorNetwork(PacketDevice selectedDevice)
        {
            // Open the device
            using (PacketCommunicator communicator =
                selectedDevice.Open(65536,                                  // portion of the packet to capture
                                                                            // 65536 guarantees that the whole packet will be captured on all the link layers
                                    PacketDeviceOpenAttributes.Promiscuous, // promiscuous mode
                                    1000))                                  // read timeout
            {
                communicator.SetFilter("ip and tcp");
                string comstring = "";
                string uuidchk = "";
                string userNamechk = "";

                // Retrieve the packets
                Packet packet;
                do
                {
                    PacketCommunicatorReceiveResult result = communicator.ReceivePacket(out packet);
                    switch (result)
                    {
                        case PacketCommunicatorReceiveResult.Timeout:
                            // Timeout elapsed
                            continue;
                        case PacketCommunicatorReceiveResult.Ok:
                            comstring = Encoding.Default.GetString(packet.Buffer);

                            if (comstring.Contains("uuid"))
                            {
                                //Process UUID
                                Match match = Regex.Match(comstring, "(?:uuid\":\")([\\w|-]+)(?:\")");
                                uuidchk = match.Groups[1].ToString();

                                if (uuidchk.Length != 36)
                                    break;
                                 
                                match = Regex.Match(comstring, "(?:userName\":\")([\\w]+)(?:\")");
                                userNamechk = match.Groups[1].ToString();

                                uuid = uuidchk;

                                if (_shouldStop == false)
                                {
                                    _shouldStop = true;
                                    Invoke((MethodInvoker)delegate ()
                                    {
                                        using (StreamWriter w = File.AppendText("users.dat"))
                                        {
                                            w.WriteLine(userNamechk + " > " + uuidchk);
                                        }
                                        uuid_combobox.Items.Add(userNamechk + " > " + uuidchk);
                                        uuid_combobox.SelectedIndex = uuid_combobox.Items.Count - 1;
                                        mainListBox.Items.Add(uuid);
                                        uuid_capture.Text = "[UUID Capture is Off]";
                                    });
                                }
                            }
                            break;
                        default:
                            throw new InvalidOperationException("Invalid Operation... Software Error");
                    }
                } while (!_shouldStop);
            }
        }

        //Simple generic encryption algorithm
        public static byte[] SimpleEncrypt(SymmetricAlgorithm algorithm, CipherMode cipherMode, byte[] key, byte[] iv, byte[] bytes)
        {
            algorithm.Mode = cipherMode;
            algorithm.Padding = PaddingMode.Zeros;
            algorithm.Key = key;
            algorithm.IV = iv;

            using (var encryptor = algorithm.CreateEncryptor())
            {
                return encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            }
        }

        //Simple generic decryption algorithm
        public static byte[] SimpleDecrypt(SymmetricAlgorithm algorithm, CipherMode cipherMode, byte[] key, byte[] iv, byte[] bytes)
        {
            algorithm.Mode = cipherMode;
            algorithm.Padding = PaddingMode.Zeros;
            algorithm.Key = key;
            algorithm.IV = iv;

            using (var encryptor = algorithm.CreateDecryptor())
            {
                return encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            }
        }

        private void browse_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "File Selection";
            fdlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fdlg.Filter = "Convertable files|*.ajart;*.ajgart;*.png;*.jpg;*.bmp";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = fdlg.FileName;
            }
        }

        private void decode_button_Click(object sender, EventArgs e)
        {
            if (uuid.Length != 36)
            {
                mainListBox.Items.Add("Invalid UUID...");
            }
            else if (!File.Exists(textBox3.Text))
            {
                mainListBox.Items.Add("Invalid Browse...");
            }
            else
            {
                mainListBox.Items.Add("Decrypting File...");
                //ENC -> DECOMP -> AMF -> PNG
                string file = textBox3.Text;
                byte[] byteData = new byte[0];
                //AES encryption scheme is used by AJ for these files
                byteData = SimpleDecrypt(new RijndaelManaged(), CipherMode.CBC, _keybytes_, _ivbytes_, File.ReadAllBytes(file));

                byte[] buffuncompress = new byte[1024];

                mainListBox.Items.Add("Decompressing File...");
                try
                {
                    using (Stream decompstream = new MemoryStream(byteData))
                    {
                        using (ZlibStream decompressionStream = new ZlibStream(decompstream, Ionic.Zlib.CompressionMode.Decompress))
                        {
                            var resultStream = new MemoryStream();
                            int read;

                            while ((read = decompressionStream.Read(buffuncompress, 0, buffuncompress.Length)) > 0)
                            {
                                resultStream.Write(buffuncompress, 0, read);
                            }

                            byteData = resultStream.ToArray();
                        }
                    }

                    byteData = AMFParser.deserialize(byteData);
                    mainListBox.Items.Add("Saving file (as .PNG)");
                    File.WriteAllBytes(file.Substring(0, file.IndexOf('.')) + ".png", byteData);
                    mainListBox.Items.Add("Finished!");
                }
                catch (Ionic.Zlib.ZlibException)
                {
                    mainListBox.Items.Add("Failed Decompression...");
                    mainListBox.Items.Add("Do you have the right account selected?");
                }
            }
        }

        private void encode_button_Click(object sender, EventArgs e)
        {
            if (uuid.Length != 36)
            {
                mainListBox.Items.Add("Invalid UUID...");
            }
            else if (!File.Exists(textBox3.Text))
            {
                mainListBox.Items.Add("Invalid Browse...");
            }
            else
            {
                bool pixelArtwork = false;
                DialogResult dialogResult = MessageBox.Show("Are you encoding regular art (Yes) or pixel art (No)?", "Encoding Type", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    pixelArtwork = true;
                    mainListBox.Items.Add("Encoding Pixel Art...");
                }
                else
                {
                    mainListBox.Items.Add("Encoding Regular Art...");
                }

                string file = textBox3.Text;
                //PNG -> AMF -> Comp -> Enc
                mainListBox.Items.Add("Serializing File...");
                Dictionary<string, object> srcData = new Dictionary<string, object>();

                try
                {
                    byte[] byteData = AMFParser.serialize(File.ReadAllBytes(file), uuid, pixelArtwork);

                    mainListBox.Items.Add("Compressing File...");

                    using (Stream compstream = new MemoryStream(byteData))
                    {
                        using (ZlibStream compressionStream = new ZlibStream(compstream, Ionic.Zlib.CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestCompression))
                        {
                            var resultStream = new MemoryStream();
                            int read;

                            while ((read = compressionStream.Read(byteData, 0, byteData.Length)) > 0)
                            {
                                resultStream.Write(byteData, 0, read);
                            }

                            byteData = resultStream.ToArray();
                            mainListBox.Items.Add("Encrypting File... (as .ajart)");

                            //AES encryption scheme is used by AJ for these files
                            byteData = SimpleEncrypt(new RijndaelManaged(), CipherMode.CBC, _keybytes_, _ivbytes_, byteData);
                        }
                    }

                    File.WriteAllBytes(file.Substring(0, file.IndexOf('.')) + (pixelArtwork == false ? ".ajart" : ".ajgart"), byteData);
                    mainListBox.Items.Add("Finished!");
                }
                catch (FileNotFoundException)
                {
                    mainListBox.Items.Add("Failed To Encode...");
                    mainListBox.Items.Add("File Not Found Exception");
                }
                catch (Ionic.Zlib.ZlibException ze)
                {
                    mainListBox.Items.Add("Failed Compression...");
                    mainListBox.Items.Add(ze.Message);
                }
            }
        }

        //When the selected user / uuid changes, set the key bytes and
        //initialization vector bytes accordingly
        private void uuid_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = uuid_combobox.GetItemText(uuid_combobox.SelectedItem);
            Match match = Regex.Match(temp, @"(?:> )([\w|-]+)");
            uuid = match.Groups[1].ToString();

            if (uuid.Length == 36)
            {
                string _key_ = "";
                string _iv_ = "";

                int _counter_ = 0;
                while (_key_.Length < 16)
                {
                    _key_ += uuid.ElementAt(_counter_++);
                    _iv_ += uuid.ElementAt(_counter_++);
                }

                _keybytes_ = Encoding.Default.GetBytes(_key_);
                _ivbytes_ = Encoding.Default.GetBytes(_iv_);
            }
        }
    }
}
