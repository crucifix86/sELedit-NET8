namespace LBLIBRARY
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class ArchiveEngine : IDisposable
    {
        private bool disposed;
        public PCKStream PckFile;
        public List<PCKFileEntry> Files;
        private int _CompressionLevel = 9;

        public ArchiveEngine(string path)
        {
            this.PckFile = new PCKStream(path, null);
            this.Files = this.ReadFileTable(this.PckFile).ToList<PCKFileEntry>();
        }

        private void CleanUp(bool clean)
        {
            if (!this.disposed && clean)
            {
                this.PckFile.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            this.Files = null;
            this.PckFile.Dispose();
            GC.SuppressFinalize(this);
        }

        ~ArchiveEngine()
        {
            this.CleanUp(true);
        }

        public int GetFilesCount(PCKStream stream)
        {
            stream.Seek((long) (-8), SeekOrigin.End);
            return stream.ReadInt32();
        }

        public byte[] ReadFile(PCKStream stream, PCKFileEntry file)
        {
            if (file == null)
            {
                return new byte[0];
            }
            stream.Seek((long) file.Offset, SeekOrigin.Begin);
            byte[] bytes = stream.ReadBytes(file.CompressedSize);
            return ((file.CompressedSize < file.Size) ? PCKZlib.Decompress(bytes, file.Size) : bytes);
        }

        public PCKFileEntry[] ReadFileTable(PCKStream stream)
        {
            stream.Seek((long) (-8), SeekOrigin.End);
            stream.Seek(-272L, SeekOrigin.End);
            long offset = stream.ReadUInt32() ^ ((uint) stream.key.KEY_1);
            PCKFileEntry[] entryArray = new PCKFileEntry[stream.ReadInt32()];
            stream.Seek(offset, SeekOrigin.Begin);
            for (int i = 0; i < entryArray.Length; i++)
            {
                int count = stream.ReadInt32() ^ stream.key.KEY_1;
                stream.ReadInt32();
                entryArray[i] = new PCKFileEntry(stream.ReadBytes(count));
            }
            return entryArray;
        }

        public int CompressionLevel
        {
            get => 
                this._CompressionLevel;
            set
            {
                if (this._CompressionLevel != value)
                {
                    this._CompressionLevel = value;
                }
            }
        }
    }
}

