using System;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Art_Studio_Edit
{
    static class Initializer
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {
            /////////////////////////////////////////////////////////////////////////
            /// All Relevant DLLs are saved to the lib folder
            ExtractResourceToFile("Art_Studio_Edit.Resources.AMF3SpecCLI.dll", "AMF3SpecCLI.dll");
            ExtractResourceToFile("Art_Studio_Edit.Resources.Ionic.Zlib.dll", "Ionic.Zlib.dll");
            ExtractResourceToFile("Art_Studio_Edit.Resources.NAudio.dll", "NAudio.dll");
            ExtractResourceToFile("Art_Studio_Edit.Resources.PcapDotNet.Base.dll", "PcapDotNet.Base.dll");
            ExtractResourceToFile("Art_Studio_Edit.Resources.PcapDotNet.Core.dll", "PcapDotNet.Core.dll");
            ExtractResourceToFile("Art_Studio_Edit.Resources.PcapDotNet.Core.Extensions.dll", "PcapDotNet.Core.Extensions.dll");
            ExtractResourceToFile("Art_Studio_Edit.Resources.PcapDotNet.Packets.dll", "PcapDotNet.Packets.dll");
            /////////////////////////////////////////////////////////////////////////

            //Some rendering tinkering to make WinForms look nice.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        public static void ExtractResourceToFile(string resourceName, string filename)
        {
            if (!System.IO.File.Exists(filename))
                using (System.IO.Stream s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                using (System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create))
                {
                    byte[] b = new byte[s.Length];
                    s.Read(b, 0, b.Length);
                    fs.Write(b, 0, b.Length);
                }
        }
    }
}
