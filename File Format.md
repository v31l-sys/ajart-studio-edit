# Technical File Format Overview:
The .ajart / .ajgart format is like an onion when it comes to understanding the file format as a whole. Working with this file requires the UUID of the user that created it in the first place since it used to encrypt the file during the export. The file (uncompressed) is also an AMF (Action Message Format) object (from ActionScript 3) with these attributes:

#### AMF Object:
1. "b": The Image Byte Data
2. "p": The Full UUID
3. "h": The String "aja2id" or "ajg1id"

Note for "h" attribute:
aja2id = regular artwork
ajg1id = pixel specific artwork

They UUID key itself is 32 alphanumeric characters (all lowercase), but includes 4 dashes making the total length 36 characters.

*An example of this key*:
**c6v3n2a8-4396-5134-90z7-2s89a23br89q**

Getting this key requires sniffing packets going between your browser and Animal Jam AWS servers, although it is also *technically* possible to retrieve the key from browser memory... but good luck with that.

#### Decryption:
Interestingly the developers chose to use the UUID as the *Initialization Vector* and *Key* for the Rijndael (AES) Cipher in CBC mode, with Zeros as padding. Calculating these values is a matter of using this "algorithm":

```cs
string uuid = "c6v3n2a8-4396-5134-90z7-2s89a23br89q";
string _key_ = "";
string _iv_  = "";

int _counter_ = 0;
while (_key_.Length < 16)
{
    _key_ += uuid.ElementAt(_counter_++);
    _iv_  += uuid.ElementAt(_counter_++);
}

_keybytes_ = Encoding.Default.GetBytes(_key_);
_ivbytes_  = Encoding.Default.GetBytes(_iv_);
```

Now that you know the key and initialization vector (as well as the mode of operation and padding), you can decrypt the file which will present you with a compressed file.

#### Compressed File:
By observing your work with the hex editor, the first couple of bytes you should see will be 78 DA, which is a dead giveaway this is a zlib compressed file, where 78 DA stands for the best compression possible. Knowing this find a capable library in the programming language of your choice that can decompress a zlib file.

#### Working With AMF:
And now you have a valid AMF formatted file, you may notice that the default image byte data is a .PNG, and by scrolling to the very bottom you'll find the UUID and hardcoded string "aja2id". This tutorial won't go into many details of AMF, but using an IDE like FlashDevelop will let you tinker with the file using native ActionScript 3. If you can find a library that is able to read AMF data in a programming language of your choice then this could also be used.

*Because of some small quirks in the format AJ uses and the particular C++ libraries I was messing with, when I first recreated these files with custom images and since I wanted them to be exact, I had to hard code the second byte to hex 0B, after adding the byte data of the image (with attribute "b") the hex values { 03, 68, 06, 0D, 61, 6A, 61, 32, 69, 64, 03, 70, 06, 49 } (attribute "h", and value "aja2id"), then add attribute "p" (the uuid, as hex of course), and finally at the very end of the file a 01.*

Ultimately what I did was not really necessary and any working AMF library you use, regardless of order of attributes or small byte changes, will load just the same in the AJ Art Studio (but you must use the same attributes). All attributes are necessary because the code checks that the UUID is the same as the user loading the file, loads the image data from the "b" attribute, and also checks the "h" "aja2id" string.

#### Image Extraction:
The final part of this process involves using whatever AMF library you chose to pull the data. Image data is in attribute "b", and you should be storing that as a byte array. After this simply save the byte data with the extension of ".png".

#### Creating .ajart and .ajgart Files:
This is basically the same process we went through in reverse, interestingly though the AJ Art Studio (because of the way ActionScript 3 handles image data) will load any compatible file formats, such as jpg/jpeg, and even the first frame of a gif, and of course png. It is important to know however that the Art Studio canvas is only 711 x 434 pixels, and any extra pixels will simply be cropped off. So I suggest fitting the image to this frame, by either cropping or stretching. If you intend on importing pixel art instead this is slightly different -- 714p x 434p, but downscaled to 51p x 31p in the AJ art editor (a scalar of 14). So to do this you would do your pixel art on a 51p x 31p grid and then use your image editing program to scale that up by 14, also make sure to use either the .PNG or .BMP format and to use **hard** scaling (so the pixels aren't smoothed out). If you don't intend on loading pixel art using this pixel specific art format then you can technically also encode it as a regular .ajart file at 711 x 434.

Create an AMF object:
"b" Image Byte Data
"p" UUID
"h" The String "aja2id" or "ajg1id"

Compress this file using zlib compression, and then encrypt the file using the initialization vector and key bytes calculated above. With this you can now load your custom artwork into the AJ Art Studio.

#### Final:
And thus we have made .ajart and .ajgart files interoperable with essentially all image editors, and can even import images of multiple different types (jpg/jpeg, png, gif, etc). The final note I would like to give is that there is some arbitrary file size limit (around 512kb), so if it doesn't work and it's a larger file you may need to compress the image by saving it as a jpg/jpeg + adjust the compression / quality.