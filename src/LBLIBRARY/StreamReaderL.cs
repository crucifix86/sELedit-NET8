namespace LBLIBRARY
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class StreamReaderL : StreamReader
    {
        public StreamReaderL(string path, Encoding e) : base(path, e)
        {
        }

        public StreamReaderL(byte[] File, Encoding e) : base(new MemoryStream(File), e)
        {
        }

        public string ReadNonEmptyLine()
        {
            string str = base.ReadLine();
            return ((!str.Contains(":") || str.ToLower().StartsWith("float:")) ? base.ReadLine() : str);
        }

        public string ReadWhile(string T, List<string> below)
        {
            string item = base.ReadLine();
            if (item.StartsWith(T))
            {
                return item;
            }
            if (below != null)
            {
                below.Add(item);
            }
            return this.ReadWhile(T, below);
        }

        public string TryReadLine()
        {
            try
            {
                return base.ReadLine();
            }
            catch
            {
                return "";
            }
        }
    }
}

