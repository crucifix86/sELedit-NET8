namespace LBLIBRARY
{
    using sELedit.DDSReader;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class PWHelper
    {
        private static Bitmap Big;
        private static Bitmap Small;

        public PWHelper()
        {
            Small = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("LBLIBRARY.Small.ico"));
            Big = new Bitmap(base.GetType().Assembly.GetManifestResourceStream("LBLIBRARY.Standard.ico"));
        }

        private static List<string> CreateLines(ArchiveEngine pck)
        {
            byte[] buffer = pck.ReadFile(pck.PckFile, pck.Files.Where<PCKFileEntry>(i => i.Path == @"surfaces\iconset\iconlist_ivtrm.txt").ElementAt<PCKFileEntry>(0)).ToArray<byte>();
            List<string> list = new List<string>();
            StreamReader reader = new StreamReader(new MemoryStream(buffer), Encoding.GetEncoding(0x3a8));
            int num = 0;
            int num2 = 0;
            while (true)
            {
                if (num2 < buffer.Count<byte>())
                {
                    list.Add(reader.ReadLine());
                    if (list[num2] != null)
                    {
                        num++;
                        num2++;
                        continue;
                    }
                }
                list.RemoveAll(v => ReferenceEquals(v, null));
                list.ForEach(z => z.ToLower());
                return list;
            }
        }

        public static Bitmap LinkImages(List<ShopIcon> Surfaces_icons)
        {
            Bitmap image = new Bitmap(0x480, ((Surfaces_icons.Count * 0x80) / 9) + 0x37);
            Graphics graphics = Graphics.FromImage(image);
            int x = 0;
            int y = 0;
            for (int i = 0; i < Surfaces_icons.Count; i++)
            {
                graphics.DrawImage(Surfaces_icons[i].Icon, x, y);
                x += 0x80;
                if (image.Width == x)
                {
                    y += 0x80;
                    x = 0;
                }
            }
            return image;
        }

        public static Bitmap LoadDDSImage(byte[] ByteArray)
        {
            try
            {
                if (ByteArray == null || ByteArray.Length == 0)
                {
                    System.Diagnostics.Debug.WriteLine("LoadDDSImage: ByteArray is null or empty");
                    return null;
                }
                
                using (MemoryStream memoryStream = new MemoryStream(ByteArray))
                {
                    Bitmap bitmap = DDS.LoadImage(memoryStream);
                    return bitmap;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadDDSImage failed: {ex.Message}");
                return null;
            }
        }

        public static List<Icon> LoadIconList(Elements el, ArchiveEngine pck)
        {
            Func<PCKFileEntry, bool> predicate = d => d.Path == @"surfaces\iconset\iconlist_ivtrm.dds";
            Bitmap img = LoadDDSImage(pck.ReadFile(pck.PckFile, pck.Files.Where<PCKFileEntry>(predicate).ElementAt<PCKFileEntry>(0)).ToArray<byte>());
            if (img == null)
            {
                System.Diagnostics.Debug.WriteLine("LoadIconList: Failed to load DDS image");
                return new List<Icon>();
            }
            return LoadIconList(el, img, CreateLines(pck));
        }

        public static List<Icon> LoadIconList(Elements el, string PckPath)
        {
            ArchiveEngine pck = new ArchiveEngine(PckPath);
            Func<PCKFileEntry, bool> predicate = d => d.Path == @"surfaces\iconset\iconlist_ivtrm.dds";
            Bitmap img = LoadDDSImage(pck.ReadFile(pck.PckFile, pck.Files.Where<PCKFileEntry>(predicate).ElementAt<PCKFileEntry>(0)).ToArray<byte>());
            if (img == null)
            {
                System.Diagnostics.Debug.WriteLine("LoadIconList: Failed to load DDS image from PCK");
                return new List<Icon>();
            }
            return LoadIconList(el, img, CreateLines(pck));
        }

        public static List<Icon> LoadIconList(Elements el, Bitmap img, List<string> Text)
        {
            List<Icon> list = new List<Icon>();
            Text.RemoveRange(0, 4);
            int x = 0;
            int y = 0;
            for (int i = 0; i < Text.Count; i++)
            {
                Icon icon1 = new Icon();
                icon1.IconName = Text[i];
                icon1.StandardImage = img.Clone(new Rectangle(x, y, 0x20, 0x20), img.PixelFormat);
                Icon item = icon1;
                item.ResizedImage = item.StandardImage.ResizeImage(0x15, 0x15);
                list.Add(item);
                if ((x + 0x20) == img.Width)
                {
                    y += 0x20;
                    x = 0;
                }
            }
            using (List<Elements.List>.Enumerator enumerator = el.ElementsLists.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    foreach (Elements.Item item in enumerator.Current.Items)
                    {
                        string itn = item.Icon;
                        if (item.Icon != null)
                        {
                            itn = itn.ToLower();
                        }
                        int num4 = list.FindIndex(z => z.IconName == itn);
                        if (num4 != -1)
                        {
                            item.IconImage = list[num4].ResizedImage;
                            item.Standard_image = list[num4].StandardImage;
                        }
                    }
                }
            }
            return list;
        }

        public static List<Icon> LoadIconList(Elements el, string Iconlist_dds, string iconlist_txt) => 
            LoadIconList(el, LoadDDSImage(File.ReadAllBytes(Iconlist_dds)), File.ReadAllLines(iconlist_txt, Encoding.GetEncoding(0x3a8)).ToList<string>());

        public static List<Desc> LoadItemExtDesc(ArchiveEngine pck)
        {
            Func<PCKFileEntry, bool> predicate = i => i.Path.StartsWith(@"configs\item_ext_desc");
            IEnumerable<PCKFileEntry> source = pck.Files.Where<PCKFileEntry>(predicate);
            byte[] buffer = pck.ReadFile(pck.PckFile, source.ElementAt<PCKFileEntry>(0)).ToArray<byte>();
            StreamReader reader = new StreamReader(new MemoryStream(buffer), Encoding.GetEncoding(0x3a8));
            List<string> list = new List<string>();
            int num = 0;
            int num2 = 0;
            while (true)
            {
                if (num2 < buffer.Count<byte>())
                {
                    list.Add(reader.ReadLine());
                    if (list[num2] != null)
                    {
                        num++;
                        num2++;
                        continue;
                    }
                }
                Predicate<string> match = v => ReferenceEquals(v, null);
                list.RemoveAll(match);
                List<Desc> list2 = new List<Desc>();
                foreach (string str in list)
                {
                    if (str.StartsWith("/") || (str.StartsWith("#") || (str.StartsWith("^") || ((str == "") || !str.Contains("\"")))))
                    {
                        continue;
                    }
                    char[] separator = new char[] { '"' };
                    string[] strArray = str.Split(separator);
                    if (strArray.Count<string>() > 1)
                    {
                        int num3;
                        int.TryParse(strArray[0], out num3);
                        if ((num3 != 0) && (num3 != -1))
                        {
                            list2.Add(new Desc(num3, strArray[1]));
                        }
                    }
                }
                return list2;
            }
        }

        private static void ReadConfigFile(string AppStart, int vers, Elements el)
        {
            StreamReader reader = new StreamReader(Directory.GetFiles(AppStart + @"\configs", "PW_*_v" + vers + ".cfg")[0], Encoding.UTF8);
            el.ListsAmount = Convert.ToInt32(reader.ReadLine());
            el.DialogsListPosition = Convert.ToInt32(reader.ReadLine());
            int num = 0;
            while (num < el.ListsAmount)
            {
                string str = "";
                while (true)
                {
                    if (str != "")
                    {
                        Elements.List item = new Elements.List {
                            ListName = str,
                            ListType = reader.ReadLine()
                        };
                        string[] separator = new string[] { ";" };
                        item.TypesNames = reader.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                        string[] textArray2 = new string[] { ";" };
                        item.Types = reader.ReadLine().Split(textArray2, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                        el.ElementsLists.Add(item);
                        num++;
                        break;
                    }
                    str = reader.ReadLine();
                }
            }
            reader.Close();
        }

        private static void ReadElementLists(BinaryReader br, Elements el)
        {
            for (int i = 0; i < el.ListsAmount; i++)
            {
                if ((i == 20) && (el.Version >= 10))
                {
                    br.ReadBytes(4);
                    byte[] buffer = br.ReadBytes(4);
                    br.ReadBytes(BitConverter.ToInt32(buffer, 0));
                    br.ReadBytes(4);
                }
                if ((i == 100) && (el.Version >= 10))
                {
                    br.ReadBytes(4);
                    byte[] buffer2 = br.ReadBytes(4);
                    br.ReadBytes(BitConverter.ToInt32(buffer2, 0));
                }
                if (!el.NonObjectListBytesAmount.Keys.Contains<int>(i))
                {
                    el.ElementsLists[i].ItemsAmount = br.ReadInt32();
                    el.ElementsLists[i].Items = new List<Elements.Item>();
                    int num6 = 0;
                    while (num6 < el.ElementsLists[i].ItemsAmount)
                    {
                        Elements.Item item = new Elements.Item {
                            Id = br.ReadInt32()
                        };
                        int num7 = 0;
                        while (true)
                        {
                            if (num7 >= el.ElementsLists[i].Types.Count)
                            {
                                el.ElementsLists[i].Items.Add(item);
                                num6++;
                                break;
                            }
                            if (!(((((el.ElementsLists[i].TypesNames[num7] == "Name") | (el.ElementsLists[i].TypesNames[num7] == "Иконка")) | (el.ElementsLists[i].TypesNames[num7] == "file_icon")) | (el.ElementsLists[i].TypesNames[num7] == "Кол-во в ячейке")) | (el.ElementsLists[i].TypesNames[num7] == "pile_num_max")))
                            {
                                item.Values.Add(ReadValue(br, el.ElementsLists[i].Types[num7]));
                            }
                            else if (el.ElementsLists[i].TypesNames[num7] == "Name")
                            {
                                item.Name = br.ReadBytes(0x40).ToString(Encoding.Unicode);
                            }
                            else if ((el.ElementsLists[i].TypesNames[num7] == "Иконка") | (el.ElementsLists[i].TypesNames[num7] == "file_icon"))
                            {
                                item.Icon = br.ReadBytes(0x80).ToString(Encoding.GetEncoding(0x3a8)).GetIconNameFromString();
                            }
                            else
                            {
                                item.MaxAmount = br.ReadInt32();
                            }
                            num7++;
                        }
                    }
                }
                else if (i == 0x39)
                {
                    el.NpcsList.ItemsAmount = br.ReadInt32();
                    for (int j = 0; j < el.NpcsList.ItemsAmount; j++)
                    {
                        Elements.Item item = new Elements.Item {
                            Id = br.ReadInt32(),
                            Name = br.ReadBytes(0x40).ToString(Encoding.Unicode)
                        };
                        br.BaseStream.Seek((long) (el.NonObjectListBytesAmount[i] - 0x44), SeekOrigin.Current);
                        el.NpcsList.Items.Add(item);
                    }
                }
                else if (el.NonObjectListBytesAmount[i] != -1)
                {
                    br.BaseStream.Seek((long) (el.NonObjectListBytesAmount[i] * br.ReadInt32()), SeekOrigin.Current);
                }
                else
                {
                    byte[] bytes = Encoding.GetEncoding("GBK").GetBytes(@"facedata\");
                    long position = br.BaseStream.Position;
                    int num4 = -72 - bytes.Length;
                    bool flag = true;
                    while (true)
                    {
                        if (!flag)
                        {
                            br.BaseStream.Position = position;
                            br.BaseStream.Seek((long) num4, SeekOrigin.Current);
                            break;
                        }
                        flag = false;
                        for (int j = 0; j < bytes.Length; j++)
                        {
                            num4++;
                            if (br.ReadByte() != bytes[j])
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static Elements ReadElements(string FilePath, string ApplicationStartUpPath, bool RemoveNonItemLists)
        {
            Elements el = new Elements();
            BinaryReader br = new BinaryReader(File.Open(FilePath, FileMode.Open));
            el.Version = br.ReadInt16();
            ReadConfigFile(ApplicationStartUpPath, el.Version, el);
            el.NonObjectListBytesAmount = new Dictionary<int, int>();
            List<int> list1 = new List<int>();
            list1.Add(0xbb);
            list1.Add(0xb9);
            list1.Add(0xb7);
            list1.Add(0xb5);
            list1.Add(180);
            list1.Add(0xb3);
            list1.Add(0xb2);
            list1.Add(0xb1);
            list1.Add(0xb0);
            list1.Add(0xaf);
            list1.Add(0xae);
            list1.Add(0xad);
            list1.Add(0xac);
            list1.Add(170);
            list1.Add(0xa9);
            list1.Add(0xa8);
            list1.Add(0xa7);
            list1.Add(0xa6);
            list1.Add(0xa5);
            list1.Add(0xa4);
            list1.Add(0xa3);
            list1.Add(0xa1);
            list1.Add(160);
            list1.Add(0x9f);
            list1.Add(0x9e);
            list1.Add(0x9d);
            list1.Add(0x9c);
            list1.Add(0x9b);
            list1.Add(0x99);
            list1.Add(0x98);
            list1.Add(150);
            list1.Add(0x95);
            list1.Add(0x94);
            list1.Add(0x93);
            list1.Add(0x92);
            list1.Add(0x91);
            list1.Add(0x90);
            list1.Add(0x8f);
            list1.Add(0x8e);
            list1.Add(0x8b);
            list1.Add(0x8a);
            list1.Add(0x89);
            list1.Add(0x88);
            list1.Add(0x84);
            list1.Add(0x83);
            list1.Add(0x81);
            list1.Add(0x80);
            list1.Add(0x7f);
            list1.Add(0x7e);
            list1.Add(120);
            list1.Add(0x6f);
            list1.Add(110);
            list1.Add(0x6d);
            list1.Add(0x69);
            list1.Add(0x68);
            list1.Add(0x67);
            list1.Add(0x66);
            list1.Add(0x65);
            list1.Add(100);
            list1.Add(0x5e);
            list1.Add(0x5d);
            list1.Add(0x5b);
            list1.Add(90);
            list1.Add(0x58);
            list1.Add(0x57);
            list1.Add(0x55);
            list1.Add(0x54);
            list1.Add(0x52);
            list1.Add(0x51);
            list1.Add(80);
            list1.Add(0x4f);
            list1.Add(0x4e);
            list1.Add(0x4d);
            list1.Add(0x4c);
            list1.Add(0x49);
            list1.Add(0x48);
            list1.Add(0x47);
            list1.Add(70);
            list1.Add(0x45);
            list1.Add(0x44);
            list1.Add(0x43);
            list1.Add(0x42);
            list1.Add(0x41);
            list1.Add(0x40);
            list1.Add(0x3f);
            list1.Add(0x3e);
            list1.Add(0x3d);
            list1.Add(60);
            list1.Add(0x3b);
            list1.Add(0x3a);
            list1.Add(0x39);
            list1.Add(0x38);
            list1.Add(0x37);
            list1.Add(0x36);
            list1.Add(0x35);
            list1.Add(0x34);
            list1.Add(0x33);
            list1.Add(50);
            list1.Add(0x31);
            list1.Add(0x30);
            list1.Add(0x2f);
            list1.Add(0x2e);
            list1.Add(0x2d);
            list1.Add(0x2c);
            list1.Add(0x2b);
            list1.Add(0x2a);
            list1.Add(0x29);
            list1.Add(40);
            list1.Add(0x27);
            list1.Add(0x26);
            list1.Add(0x25);
            list1.Add(0x24);
            list1.Add(0x22);
            list1.Add(0x20);
            list1.Add(30);
            list1.Add(20);
            list1.Add(0x12);
            list1.Add(0x10);
            list1.Add(14);
            list1.Add(13);
            list1.Add(11);
            list1.Add(10);
            list1.Add(8);
            list1.Add(7);
            list1.Add(5);
            list1.Add(4);
            list1.Add(2);
            list1.Add(1);
            list1.Add(0);
            List<int> ls = list1;
            if (RemoveNonItemLists)
            {
                for (int k = 0; k < el.ElementsLists.Count; k++)
                {
                    if (ls.Contains(k))
                    {
                        el.NonObjectListBytesAmount.Add(k, el.ElementsLists[k].Types.SumBytes());
                    }
                }
            }
            for (int i = 0; i < el.ElementsLists.Count; i++)
            {
                if (i != 0x3a)
                {
                    el.ElementsLists[i].TypesNames.RemoveAt(0);
                    el.ElementsLists[i].Types.RemoveAt(0);
                }
            }
            br.BaseStream.Seek(2L, SeekOrigin.Current);
            if (el.Version >= 10)
            {
                br.BaseStream.Seek(4L, SeekOrigin.Current);
            }
            ReadElementLists(br, el);
            br.Close();
            if (RemoveNonItemLists)
            {
                el.RemoveNonObjectList(ls);
            }
            Func<Elements.List, IEnumerable<Elements.Item>> selector = z => z.Items;
            el.Items = el.ElementsLists.SelectMany<Elements.List, Elements.Item>(selector).ToList<Elements.Item>();
            el.InListAmount = new int[el.ElementsLists.Count];
            for (int j = 0; j < el.ElementsLists.Count; j++)
            {
                Func<Elements.List, int> func2 = v => v.ItemsAmount;
                List<int> list2 = el.ElementsLists.Select<Elements.List, int>(func2).ToList<int>();
                list2.RemoveRange(j + 1, el.ElementsLists.Count - (j + 1));
                el.InListAmount[j] = ((IEnumerable<int>) list2).Sum();
            }
            return el;
        }

        public static List<ShopIcon> ReadSurfacesIcons(ArchiveEngine pck)
        {
            List<ShopIcon> list = new List<ShopIcon>();
            Func<PCKFileEntry, bool> predicate = i => (!i.Path.StartsWith(@"surfaces\百宝阁\") || !i.Path.Contains(".dds")) ? (i.Path.StartsWith(@"surfaces\竞拍品\") && i.Path.Contains(".dds")) : true;
            foreach (PCKFileEntry entry in pck.Files.Where<PCKFileEntry>(predicate).ToList<PCKFileEntry>())
            {
                list.Add(new ShopIcon(entry.Path, LoadDDSImage(pck.ReadFile(pck.PckFile, entry).ToArray<byte>())));
            }
            return list;
        }

        private static object ReadValue(BinaryReader br, string type)
        {
            if (type.Contains("int32") || (type.Contains("link") || type.Contains("combo")))
            {
                return br.ReadInt32();
            }
            if (type.Contains("float"))
            {
                return br.ReadSingle();
            }
            if (type.Contains("byte"))
            {
                char[] chArray1 = new char[] { ':' };
                return br.ReadBytes(Convert.ToInt32(type.Split(chArray1)[1]));
            }
            if (type.Contains("wstring"))
            {
                char[] chArray2 = new char[] { ':' };
                return Encoding.Unicode.GetString(br.ReadBytes(Convert.ToInt32(type.Split(chArray2)[1]))).ToString().Split(new char[1])[0];
            }
            if (!type.Contains("string"))
            {
                return "";
            }
            char[] separator = new char[] { ':' };
            return Encoding.GetEncoding("GBK").GetString(br.ReadBytes(Convert.ToInt32(type.Split(separator)[1]))).ToString().Split(new char[1])[0];
        }


        public class Desc
        {
            public int Id;
            public string Description;

            public Desc(int i, string d)
            {
                this.Id = i;
                this.Description = d;
            }
        }

        public class Elements
        {
            public List<List> ElementsLists = new List<List>();
            public List<Item> Items = new List<Item>();
            public int[] InListAmount;
            public int Version;
            public int ListsAmount;
            public int DialogsListPosition;
            public List NpcsList = new List();
            public Dictionary<int, int> NonObjectListBytesAmount;

            public class Item
            {
                public int Id;
                public string Name;
                public int MaxAmount;
                public string Icon;
                public List<object> Values = new List<object>();
                public Image IconImage = PWHelper.Small;
                public Image Standard_image = PWHelper.Big;
            }

            public class List
            {
                public int ItemsAmount;
                public List<PWHelper.Elements.Item> Items = new List<PWHelper.Elements.Item>();

                public string ListName { get; set; }

                public string ListType { get; set; }

                public List<string> TypesNames { get; set; }

                public List<string> Types { get; set; }
            }
        }

        public class Icon
        {
            public List<int> IDS;
            public string IconName;
            public Bitmap StandardImage;
            public Bitmap ResizedImage;
        }

        public class ShopIcon
        {
            public string Name;
            public Image Icon;

            public ShopIcon(string d, Image b)
            {
                this.Name = d;
                this.Icon = b;
            }
        }
    }
}

