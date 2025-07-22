namespace LBLIBRARY
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class PCKFileEntry
    {
        public PCKFileEntry()
        {
        }

        public PCKFileEntry(byte[] bytes)
        {
            this.Read(bytes);
        }

        public void Read(byte[] bytes)
        {
            if (bytes.Length < 0x114)
            {
                bytes = PCKZlib.Decompress(bytes, 0x114);
            }
            BinaryReader reader = new BinaryReader(new MemoryStream(bytes));
            string[] separator = new string[] { "\0" };
            this.Path = Encoding.GetEncoding(0x3a8).GetString(reader.ReadBytes(260)).Split(separator, StringSplitOptions.RemoveEmptyEntries)[0].Replace("/", @"\").ToLower();
            this.Offset = reader.ReadUInt32();
            this.Size = reader.ReadInt32();
            this.CompressedSize = reader.ReadInt32();
            reader.Close();
        }

        public byte[] Write(int CompressionLevel)
        {
            byte[] buffer = new byte[0x114];
            BinaryWriter writer1 = new BinaryWriter(new MemoryStream(buffer));
            writer1.Write(Encoding.GetEncoding("GB2312").GetBytes(this.Path.Replace("/", @"\")));
            writer1.BaseStream.Seek(260L, SeekOrigin.Begin);
            writer1.Write(this.Offset);
            writer1.Write(this.Size);
            writer1.Write(this.CompressedSize);
            writer1.Write(0);
            writer1.BaseStream.Seek(0L, SeekOrigin.Begin);
            writer1.Close();
            byte[] buffer2 = PCKZlib.Compress(buffer, CompressionLevel);
            return ((buffer2.Length < 0x114) ? buffer2 : buffer);
        }

        public string Path { get; set; }

        public uint Offset { get; set; }

        public int Size { get; set; }

        public int CompressedSize { get; set; }
    }
}

