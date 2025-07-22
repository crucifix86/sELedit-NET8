using sELedit.DDSReader;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sELedit.newPck.read
{
    class ReadPCK
    {
        public void /*static List<Icon> */LoadIconList(string PckPath)
        {
            ArchiveEngine pck = new ArchiveEngine(PckPath);
            Bitmap img = LoadDDSImage(((IEnumerable<byte>)pck.ReadFile(pck.PckFile, pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(d => d.Path == "surfaces\\iconset\\iconlist_ivtrm.dds")).ElementAt<PCKFileEntry>(0))).ToArray<byte>());
            // return PWHelper.LoadIconList(el, img, PWHelper.CreateLines(pck));
        }
        public static Bitmap LoadDDSImage(byte[] ByteArray)
        {
            // Create a MemoryStream from the byte array
            using (MemoryStream memoryStream = new MemoryStream(ByteArray))
            {
                // Create a new DDS object with that stream and call GetBitmap() to get the Bitmap
                Bitmap bitmap = DDS.LoadImage(memoryStream);
                return bitmap;
            }
        }

















    }
}
