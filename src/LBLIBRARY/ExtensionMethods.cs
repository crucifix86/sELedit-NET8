namespace LBLIBRARY
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class ExtensionMethods
    {
        private static string[] sp = new string[] { ":" };

        public static string Decode(this byte[] array, Encoding e) => 
            e.GetString(array);

        public static byte[] GetBytesFromString(this string str, int l, Encoding e)
        {
            str = str.Split(new char[1])[0];
            byte[] destinationArray = new byte[l];
            if (e.GetByteCount(str) > l)
            {
                Array.Copy(e.GetBytes(str), 0, destinationArray, 0, l);
            }
            else
            {
                Array.Copy(e.GetBytes(str), destinationArray, e.GetByteCount(str));
            }
            return destinationArray;
        }

        public static string GetEcmLineValue(this string Line) => 
            Line.Split(sp, StringSplitOptions.RemoveEmptyEntries).LastOrDefault<string>().Replace(" ", "");

        public static string GetIconNameFromString(this string Source)
        {
            char[] separator = new char[] { '\\' };
            return Source.Split(separator).Last<string>();
        }

        public static PWHelper.Elements RemoveNonObjectList(this PWHelper.Elements Source, List<int> ls)
        {
            for (int i = Source.ElementsLists.Count - 1; i >= ((IEnumerable<int>) ls).Min(); i--)
            {
                if (ls.Contains(i))
                {
                    Source.ElementsLists.RemoveAt(i);
                }
            }
            return Source;
        }

        public static Bitmap ResizeImage(this Bitmap Source, int Width, int Height)
        {
            Bitmap image = new Bitmap(Width, Height);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.DrawImage(Source, 0, 0, Width, Height);
                graphics.Dispose();
            }
            return image;
        }

        public static string RoundToSix(this decimal value, int ZerosCount)
        {
            string source = value.ToString().Replace(",", ".");
            if (!source.Contains<char>('.'))
            {
                source = source + ".000000";
            }
            else
            {
                char[] separator = new char[] { '.' };
                string[] strArray = source.Split(separator);
                if ((strArray[1].Length < 6) && ((ZerosCount - strArray[1].Length) >= 0))
                {
                    source = source + new string('0', ZerosCount - strArray[1].Length);
                }
            }
            return source;
        }

        public static string RoundToSix(this float value, int ZerosCount)
        {
            string source = value.ToString().Replace(",", ".");
            if (!source.Contains<char>('.'))
            {
                source = source + ".000000";
            }
            else
            {
                char[] separator = new char[] { '.' };
                string[] strArray = source.Split(separator);
                if ((strArray[1].Length < 6) && ((ZerosCount - strArray[1].Length) >= 0))
                {
                    source = source + new string('0', ZerosCount - strArray[1].Length);
                }
            }
            return source;
        }

        public static string[] SplitEcmLine(this string Line)
        {
            Func<string, string> selector = e => e.Replace(" ", "");
            return Line.Split(sp, StringSplitOptions.RemoveEmptyEntries).Select<string, string>(selector).ToArray<string>();
        }

        public static int SumBytes(this List<string> Types)
        {
            int num = 0;
            foreach (string str in Types)
            {
                if (str.Contains("int32") || (str.Contains("link") || (str.Contains("combo") || str.Contains("float"))))
                {
                    num += 4;
                    continue;
                }
                if (str.Contains("byte") && !str.Contains("AUTO"))
                {
                    char[] separator = new char[] { ':' };
                    num += Convert.ToInt32(str.Split(separator)[1]);
                    continue;
                }
                if (str.Contains("wstring"))
                {
                    char[] separator = new char[] { ':' };
                    num += Convert.ToInt32(str.Split(separator)[1]);
                    continue;
                }
                if (str.Contains("string"))
                {
                    char[] separator = new char[] { ':' };
                    num += Convert.ToInt32(str.Split(separator)[1]);
                    continue;
                }
                if (str.Contains("byte:AUTO"))
                {
                    num = -1;
                    break;
                }
            }
            return num;
        }

        public static bool ToBoolean(this string value) => 
            value == "1";

        public static decimal ToDecimal(this string value) => 
            Convert.ToDecimal(value.Replace('.', ','));

        public static int ToInt32(this string value) => 
            Convert.ToInt32(value);

        public static List<string> ToLines(this MemoryStream ms, Encoding e)
        {
            if (ms == null)
            {
                return null;
            }
            string text1 = new StreamReader(ms, e).ReadToEnd();
            string str = text1.Contains("\r\n") ? "\r\n" : "\n";
            string[] separator = new string[] { str };
            return text1.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
        }

        public static float ToSingle(this string value) => 
            Convert.ToSingle(value.Replace('.', ','));

        public static string ToString(this byte[] Array, Encoding e) => 
            e.GetString(Array).TrimEnd(new char[1]);

        public static string[] TrySplit(this string value, char separator)
        {
            if (!value.Contains<char>(separator))
            {
                return new string[] { value };
            }
            char[] chArray1 = new char[] { separator };
            return value.Split(chArray1);
        }

        public static void WriteParameter(this StreamWriter sw, string Parameter, bool value)
        {
            sw.WriteLine(Parameter + ": " + (value ? 1 : 0));
        }

        public static void WriteParameter(this StreamWriter sw, string Parameter, decimal value)
        {
            sw.WriteLine(Parameter + ": " + value.RoundToSix(6));
        }

        public static void WriteParameter(this StreamWriter sw, string Parameter, int value)
        {
            sw.WriteLine(Parameter + ": " + value);
        }

        public static void WriteParameter(this StreamWriter sw, string Parameter, float value)
        {
            sw.WriteLine(Parameter + ": " + value.RoundToSix(6));
        }

        public static void WriteParameter(this StreamWriter sw, string Parameter, string value)
        {
            sw.WriteLine(Parameter + ": " + value);
        }

        public static void WriteParameter(this StreamWriter sw, string Parameter, decimal[] value, bool UseComma)
        {
            string[] strArray = new string[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                strArray[i] = value[i].RoundToSix(6);
            }
            string separator = UseComma ? ", " : " ";
            sw.WriteLine(Parameter + ": " + string.Join(separator, strArray));
        }

    }
}

