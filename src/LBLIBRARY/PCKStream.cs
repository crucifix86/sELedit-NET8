namespace LBLIBRARY
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public class PCKStream : IDisposable
    {
        protected BufferedStream pck;
        protected BufferedStream pkx;
        private string path = "";
        public long Position;
        public PCKKey key = new PCKKey();
        private const uint PCK_MAX_SIZE = 0x7fffff00;
        private const int BUFFER_SIZE = 0x2000000;

        public PCKStream(string path, PCKKey key = null)
        {
            this.path = path;
            if (key != null)
            {
                this.key = key;
            }
            this.pck = new BufferedStream(new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite), 0x2000000);
            if (File.Exists(path.Replace(".pck", ".pkx")) && (Path.GetExtension(path) != ".cup"))
            {
                this.pkx = new BufferedStream(new FileStream(path.Replace(".pck", ".pkx"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite), 0x2000000);
            }
        }

        public void Dispose()
        {
            if (this.pck == null)
            {
                BufferedStream pck = this.pck;
            }
            else
            {
                this.pck.Close();
            }
            if (this.pkx == null)
            {
                BufferedStream pkx = this.pkx;
            }
            else
            {
                this.pkx.Close();
            }
        }

        public long GetLenght() => 
            (this.pkx != null) ? (this.pck.Length + this.pkx.Length) : this.pck.Length;

        public byte[] ReadBytes(int count)
        {
            byte[] buffer = new byte[count];
            int offset = 0;
            if (this.Position >= this.pck.Length)
            {
                if ((this.Position > this.pck.Length) && (this.pkx != null))
                {
                    offset = this.pkx.Read(buffer, 0, count);
                }
            }
            else
            {
                offset = this.pck.Read(buffer, 0, count);
                if ((offset < count) && (this.pkx != null))
                {
                    this.pkx.Seek(0L, SeekOrigin.Begin);
                    offset += this.pkx.Read(buffer, offset, count - offset);
                }
            }
            this.Position += count;
            return buffer;
        }

        public int ReadInt32() => 
            BitConverter.ToInt32(this.ReadBytes(4), 0);

        public uint ReadUInt32() => 
            BitConverter.ToUInt32(this.ReadBytes(4), 0);

        public void Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    this.Position = offset;
                    break;

                case SeekOrigin.Current:
                    this.Position += offset;
                    break;

                case SeekOrigin.End:
                    this.Position = this.GetLenght() + offset;
                    break;

                default:
                    break;
            }
            if (this.Position < this.pck.Length)
            {
                this.pck.Seek(this.Position, SeekOrigin.Begin);
            }
            else
            {
                this.pkx.Seek(this.Position - this.pck.Length, SeekOrigin.Begin);
            }
        }

        public void WriteBytes(byte[] array)
        {
            if ((this.Position + array.Length) < 0x7fffff00L)
            {
                this.pck.Write(array, 0, array.Length);
            }
            else if ((this.Position + array.Length) > 0x7fffff00L)
            {
                this.pkx ??= new BufferedStream(new FileStream(this.path.Replace(".pck", ".pkx"), FileMode.Create, FileAccess.ReadWrite), 0x2000000);
                if (this.Position > 0x7fffff00L)
                {
                    this.pkx.Write(array, 0, array.Length);
                }
                else
                {
                    this.pkx ??= new BufferedStream(new FileStream(this.path.Replace(".pck", ".pkx"), FileMode.Create, FileAccess.ReadWrite), 0x2000000);
                    this.pck.Write(array, 0, (int) (0x7fffff00L - this.Position));
                    this.pkx.Write(array, (int) (0x7fffff00L - this.Position), array.Length - ((int) (0x7fffff00L - this.Position)));
                }
            }
            this.Position += array.Length;
        }

        public void WriteInt16(short value)
        {
            this.WriteBytes(BitConverter.GetBytes(value));
        }

        public void WriteInt32(int value)
        {
            this.WriteBytes(BitConverter.GetBytes(value));
        }

        public void WriteUInt32(uint value)
        {
            this.WriteBytes(BitConverter.GetBytes(value));
        }
    }
}

