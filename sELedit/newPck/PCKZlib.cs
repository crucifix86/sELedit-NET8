using System;
using System.IO;
using Ionic.Zlib;

namespace sELedit.newPck
{
    public static class PCKZlib
    {
        public static byte[] Decompress(byte[] bytes, int size)
        {
            try
            {
                using (var input = new MemoryStream(bytes))
                using (var output = new MemoryStream())
                using (var zlibStream = new ZlibStream(input, CompressionMode.Decompress))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = zlibStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, bytesRead);
                    }
                    
                    byte[] result = output.ToArray();
                    if (result.Length != size)
                    {
                        Array.Resize(ref result, size);
                    }
                    return result;
                }
            }
            catch
            {
                Console.WriteLine("Bad zlib data");
                return new byte[size];
            }
        }

        public static byte[] Compress(byte[] bytes, int CompressionLevel)
        {
            try
            {
                using (var output = new MemoryStream())
                {
                    using (var zlibStream = new ZlibStream(output, CompressionMode.Compress, (CompressionLevel)CompressionLevel))
                    {
                        zlibStream.Write(bytes, 0, bytes.Length);
                    }
                    
                    byte[] compressed = output.ToArray();
                    return compressed.Length >= bytes.Length ? bytes : compressed;
                }
            }
            catch
            {
                return bytes;
            }
        }

        public static void CopyStream(Stream input, Stream output, int Size)
        {
            byte[] buffer = new byte[Size];
            int count;
            while ((count = input.Read(buffer, 0, Size)) > 0)
                output.Write(buffer, 0, count);
            output.Flush();
        }
    }
}