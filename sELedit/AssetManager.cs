using sELedit.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Data;
using System.Drawing.Imaging;

namespace sELedit
{
    public class AssetManager
    {
        public int DDSFORMAT = 6;
        internal delegate void UpdateProgressDelegate(string value, int min, int max);
        private SortedList<int, int> item_color;
        private int rows;
        private Bitmap sourceBitmap;
        private CacheSave database = new CacheSave();
        private bool firstLoad = true;
        private int cols;
        private SortedList<int, string> imagesx;
        private SortedList<string, Point> imageposition;
        private SortedList<int, string> item_desc;
        public SortedList item_ext_desc;

        public static SortedList _wepon;
		public static SortedList _armor;
		public static SortedList _decoration;
		public static SortedList _suite;

		private List<string> arrTheme;
        PCKs pck;
        LoasdsProgreces LP = new LoasdsProgreces();
		public static object anydata;

		
		public SortedList<int, Image> ImageTask;

		public bool load(ColorProgressBar.ColorProgressBar cpb2, System.Windows.Forms.Control invokeControl)
        {
            // Initialize progress bar on UI thread
            if (invokeControl.IsHandleCreated)
            {
                invokeControl.Invoke((System.Windows.Forms.MethodInvoker)delegate {
                    cpb2.Maximum = 10;
                    cpb2.Minimum = 0;
                    cpb2.Value = 0;
                });
            }
            else
            {
                // If handle not created yet, just set values directly
                cpb2.Maximum = 10;
                cpb2.Minimum = 0;
                cpb2.Value = 0;
            }

            firstLoad = true;
            
            if (sourceBitmap == null)
            {
               
                imageposition = LoadSurfaces();
                UpdateProgress(cpb2, invokeControl);
                loadItem_color();
                UpdateProgress(cpb2, invokeControl);
            }

            if (firstLoad)
            {
				imagsTask();
                UpdateProgress(cpb2, invokeControl);
                //LoadTheme();
                UpdateProgress(cpb2, invokeControl);
                LoadLocalizationText();
                //this.LoadInstanceList();
                UpdateProgress(cpb2, invokeControl);
                LoadBuffList();
                UpdateProgress(cpb2, invokeControl);
                LoadItemExtDescList();
                UpdateProgress(cpb2, invokeControl);
                LoadSkillList();
                UpdateProgress(cpb2, invokeControl);
                LoadAddonList();
                UpdateProgress(cpb2, invokeControl);
                firstLoad = false;
				addons_wac();
                UpdateProgress(cpb2, invokeControl);


            }
            
            MainWindow.database = database;
            
            return true;
        }
        
        private void UpdateProgress(ColorProgressBar.ColorProgressBar cpb2, System.Windows.Forms.Control invokeControl)
        {
            if (invokeControl.IsHandleCreated)
            {
                invokeControl.Invoke((System.Windows.Forms.MethodInvoker)delegate {
                    cpb2.Value++;
                    System.Windows.Forms.Application.DoEvents();
                });
            }
            else
            {
                // If handle not created yet, just update value directly
                // This is safe because we're still in form initialization
                cpb2.Value++;
            }
        }

        public void imagsTask()
		{

			try
			{
				ImageList imageList1 = new ImageList();
				string[] arquivos = Directory.GetFiles(Application.StartupPath + @"\images", "*.png", SearchOption.TopDirectoryOnly);
				for (int fd = 0; fd < arquivos.Length; fd++)
				{
					imageList1.Images.Add(Image.FromFile(arquivos[fd]));

				}
				MainWindow.database.ImageTask = imageList1;
			}
			catch (Exception)
			{

			
			}
			
			

		}

		public void addons_wac()
		{
			string wp = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\opt\add_wepom.txt";
			string am = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\opt\add_armor.txt";
			string dec = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\opt\add_decoration.txt";
			string st = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\opt\add_suite.txt";
			string[] lines_w = File.Exists(wp) ? File.ReadAllLines(wp) : new string[0];
			string[] lines_a = File.Exists(am) ? File.ReadAllLines(am) : new string[0];
			string[] lines_d = File.Exists(dec) ? File.ReadAllLines(dec) : new string[0];
			string[] lines_e = File.Exists(st) ? File.ReadAllLines(st) : new string[0];
			_wepon = new SortedList(); _armor = new SortedList(); _decoration = new SortedList(); _suite = new SortedList();
			_wepon.Clear(); _armor.Clear(); _decoration.Clear(); _suite.Clear();
            
            for (int i = 0; i < lines_w.Length; i++)
			{
				if (lines_w[i] != "")
				{
					try
					{
						_wepon.Add(int.Parse(lines_w[i].Replace(" ", "")), EQUIPMENT_ADDON.GetAddon(lines_w[i].Replace(" ", "").ToString()));
					}
					catch (Exception)
					{

						
					}
					
				}
			}
			database._wepon = _wepon;
			for (int i = 0; i < lines_a.Length; i++)
			{
				if (lines_a[i] != "")
				{
					try
					{
						_armor.Add(int.Parse(lines_a[i].Replace(" ", "")), EQUIPMENT_ADDON.GetAddon(lines_a[i].Replace(" ", "").ToString()));
					}
					catch (Exception)
					{

						
					}
					
				}
			}
			database._armor = _armor;
			for (int i = 0; i < lines_d.Length; i++)
			{
				if (lines_d[i] != "")
				{
					try
					{
						_decoration.Add(int.Parse(lines_d[i].Replace(" ", "")), EQUIPMENT_ADDON.GetAddon(lines_d[i].Replace(" ", "").ToString()));
					}
					catch (Exception)
					{

						
					}
					
				}
			}

			database._decoration = _decoration;
			for (int i = 0; i < lines_e.Length; i++)
			{
				if (lines_e[i] != "")
				{
					try
					{
						_suite.Add(int.Parse(lines_e[i].Replace(" ", "").Split(new string[] { "\"" }, StringSplitOptions.None)[0]), lines_e[i]./*Replace(" ", "").*/Split(new string[] { "\"" }, StringSplitOptions.None)[1]);
					}
					catch (Exception)
					{


					}

				}
			}
			database._suite = _suite;

		}
        public void LoadTheme()
        {
            try
            {
                string line;
                arrTheme = new List<string>();
                string theme_list = Path.GetDirectoryName(Application.ExecutablePath) + "\\resources\\theme.txt";
                Encoding enc = Encoding.GetEncoding("GBK");
                int lines = File.ReadAllLines(theme_list).Length;
                StreamReader file = new StreamReader(theme_list, enc);
                int count = 0;

                while ((line = file.ReadLine()) != null)
                {
                    if (line != null && line.Length > 0 && !line.StartsWith("#") && !line.StartsWith("/"))
                    {
                        arrTheme.Add(line);
                    }
                    count++;
                }
                file.Close();
                database.arrTheme = arrTheme;
            }
            catch
            {
                database.arrTheme = null;
            }
        }

        static public Bitmap getSkillIcon(int skillid)
        {
            Bitmap img = Properties.Resources.ResourceManager.GetObject("_" + skillid) as Bitmap;
            return img != null ? img : new Bitmap(new Bitmap(Resources.blank));
        }
       
        



		public void loadItem_color()
		{
            try
            {
                if (MainWindow.XmlData != null && !string.IsNullOrEmpty(MainWindow.XmlData.ConfigsPckPath) && File.Exists(MainWindow.XmlData.ConfigsPckPath))
                {
                    item_color = new SortedList<int,int>();
                    pck = new PCKs(MainWindow.XmlData.ConfigsPckPath);
                    IEnumerable<PCKFileEntry> source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("configs\\item_color.txt")));
                    byte[] array = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source.ElementAt<PCKFileEntry>(0))).ToArray<byte>();

                    string tempFileName = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\configs\item_color.txt";
                    string tempDir = Path.GetDirectoryName(tempFileName);
                    Directory.CreateDirectory(tempDir);
                    File.WriteAllBytes(tempFileName, array);
                    var item_color_Read = File.ReadAllLines(tempFileName, Encoding.GetEncoding("GBK"));


                    for (int i = 0; i < item_color_Read.Length; i++)
                    {
                        if (item_color_Read[i] != null)
                        {
                            string[] data = item_color_Read[i].Split(null);
                            string v1 = data[0].ToString();
                            string v2 = data[1].ToString();
                            if (v1.Length > 0 && v2.Length > 0)
                            {
                                item_color.Add(int.Parse(v1), int.Parse(v2));
                            }
                            else
                            {
                                if (v1.Length > 0)
                                {
                                    item_color.Add(int.Parse(v1), 0);
                                }
                                if (v2.Length > 0)
                                {
                                    item_color.Add(0, int.Parse(v2));
                                }
                            }
                        }
                        else
                        {

                        }
                    }
					database.item_color = item_color;

					loaditem_desc();

				}
                else
                {
                    // No configs.pck available - use empty data
                    item_color = new SortedList<int, int>();
                    database.item_color = item_color;
                    
                    // Still need to call loaditem_desc to continue the chain
                    loaditem_desc();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void loaditem_desc()
        {
            try
            {
                
                if (MainWindow.XmlData != null && !string.IsNullOrEmpty(MainWindow.XmlData.ConfigsPckPath) && File.Exists(MainWindow.XmlData.ConfigsPckPath))
                {
                    pck = new PCKs(MainWindow.XmlData.ConfigsPckPath);
                    item_desc = new SortedList<int, string>();
                    IEnumerable<PCKFileEntry> source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("configs\\item_desc.txt")));
                    byte[] array = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source.ElementAt<PCKFileEntry>(0))).ToArray<byte>();
                   // var sd = pck.
                    string tempFileName = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\configs\item_desc.txt";
                    string tempDir = Path.GetDirectoryName(tempFileName);
                    Directory.CreateDirectory(tempDir);
                    File.WriteAllBytes(tempFileName, array);
                    var item_desc_Read = File.ReadAllLines(tempFileName, Encoding.GetEncoding("GBK"));

                    for (int i = 0; i < item_desc_Read.Length; i++)
                    {
                        
                            if (item_desc_Read[i] != null && item_desc_Read[i].Length > 0 && !item_desc_Read[i].StartsWith("#") && !item_desc_Read[i].StartsWith("/"))
                            {
                                string[] data = item_desc_Read[i].Split('"');
                                data = data.Where(a => a != "").ToArray();
                                try
                                {
                                    item_desc.Add(i, data[0])/*, data[1].ToString().Replace('"', ' ')*/;
                                }
                            catch (Exception e) { /* Log error instead of showing MessageBox from background thread */ }
                            }
                         
                    }
                }
                else
                {
                    // No configs.pck available - use empty data
                    item_desc = new SortedList<int, string>();
                    database.item_desc = item_desc;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void LoadItemExtDescList()
        {
            try
            {
                if (database.item_ext_desc != null)
                {
                    
                    //MainWindow.item_ext_desc_TXT = database.item_ext_desc;
                    return;
                }

                if (MainWindow.XmlData != null && !string.IsNullOrEmpty(MainWindow.XmlData.ConfigsPckPath) && File.Exists(MainWindow.XmlData.ConfigsPckPath))
                {
                    pck = new PCKs(MainWindow.XmlData.ConfigsPckPath);
                    try
                    {
                        IEnumerable<PCKFileEntry> source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("configs\\item_ext_desc.txt")));
                        byte[] array = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source.ElementAt<PCKFileEntry>(0))).ToArray<byte>();

                        string tempFileName = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\configs\item_ext_desc.txt";
                        string tempDir = Path.GetDirectoryName(tempFileName);
                        Directory.CreateDirectory(tempDir);
                        File.WriteAllBytes(tempFileName, array);
                        var item_ext_desc_Read = File.ReadAllLines(tempFileName, Encoding.GetEncoding("GBK"));

                        string result = string.Join("\n", item_ext_desc_Read);
                        if (result != string.Empty)
                        {
                            try
                            {
                                item_ext_desc = new SortedList();
                                var arr_result = result.Split('\n');
                                int start_Arr = 0;

                                for (int i = 0; i < arr_result.Length; i++)
                                {
                                    if (!arr_result[i].StartsWith("/") && !arr_result[i].StartsWith("#") && arr_result[i] != string.Empty)
                                    {

                                        var desfragmento = arr_result[i].Split('\"');


                                        int num;
                                        bool res = int.TryParse(desfragmento[0].Replace(" ", "").Replace("\t", ""), out num);

                                        if (res)
                                        {
                                            if (!item_ext_desc.ContainsKey(desfragmento[0].Replace(" ", "").Replace("\t", "")))
                                            {
                                                item_ext_desc.Add(desfragmento[0].Replace(" ", "").Replace("\t", "").Trim(), desfragmento[1]);
                                            }

                                        }

                                    }

                                }



                                //MainWindow.item_ext_desc_TXT = result.Split(new char[] { '\"' });
                                //string[] temp = MainWindow.item_ext_desc_TXT[0].Split(new char[] { '\n' });
                                //MainWindow.item_ext_desc_TXT[0] = temp[temp.Length - 1];


                            }
                            catch (Exception e)
                            {
                                // Log error instead of showing MessageBox from background thread
                                System.Diagnostics.Debug.WriteLine("ERROR LOADING ITEM DESCRIPTION LIST: " + e.Message);
                            }
                        }
                        else
                        {
                            // Log error instead of showing MessageBox from background thread
                            System.Diagnostics.Debug.WriteLine("NOT FOUND item_ext_desc.txt!");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log error instead of showing MessageBox from background thread
                        System.Diagnostics.Debug.WriteLine("ERROR LOADING ITEM DESCRIPTION LIST: " + ex.Message);
                    }
                }
                else
                {
                    // No configs.pck available - use empty data
                    item_ext_desc = new SortedList();
                }



                GC.Collect();
                database.item_ext_desc = item_ext_desc;
            }
            catch (Exception)
            {

                throw;
            }
			
        }

		private SortedList<string, Point> LoadSurfaces()
        {
           if (MainWindow.XmlData != null && !string.IsNullOrEmpty(MainWindow.XmlData.SurfacesPckPath) && File.Exists(MainWindow.XmlData.SurfacesPckPath))
                {
                    pck = new PCKs(MainWindow.XmlData.SurfacesPckPath);
                    IEnumerable<PCKFileEntry> source_img = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("surfaces\\iconset\\iconlist_ivtrm.dds")));
                    byte[] array_IMG = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source_img.ElementAt<PCKFileEntry>(0))).ToArray<byte>();
                    
                    DDSReader.Utils.PixelFormat sti = (DDSReader.Utils.PixelFormat)DDSFORMAT;
                    Bitmap bmp2 = null;
                    
                    try
                    {
                        bmp2 = DDSReader.DDS.LoadImage(array_IMG, true, sti);
                    }
                    catch (Exception ex)
                    {
                        // Log DDS loading error
                        System.Diagnostics.Debug.WriteLine($"Failed to load DDS image: {ex.Message}");
                        // Continue without the image
                    }

                    if (bmp2 != null)
                    {

                        if (bmp2 != null)
                        {
                            sourceBitmap = bmp2;
                            string tempFileName_img = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\surfaces\iconset\iconlist_ivtrm.dds";
                            string tempDir_img = Path.GetDirectoryName(tempFileName_img);
                            Directory.CreateDirectory(tempDir_img);
                            if (File.Exists(tempFileName_img))
                            {
                                File.Delete(tempFileName_img);
                            }
                            bmp2.Save(tempFileName_img);
                            bmp2.Save(tempFileName_img.Replace(Path.GetExtension(tempFileName_img), ".png"), ImageFormat.Png);
                        }
                        else
                        {
                            // Log error instead of showing MessageBox from background thread
                            System.Diagnostics.Debug.WriteLine("Unable to load thumbnails...");
                            //sourceBitmap = (Bitmap)Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\resources\\surfaces\\iconset\\iconlist_ivtrm.png");
                        }
                    }

                    database.sourceBitmap = sourceBitmap;
                    SortedList<string, Bitmap> results = new SortedList<string, Bitmap>();
                    List<Bitmap> zxczxc = new List<Bitmap>();
                    List<string> fileNames = new List<string>();

                    imagesx = new SortedList<int, string>();
                    int w = 0; int h = 0;

                    IEnumerable<PCKFileEntry> source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("surfaces\\iconset\\iconlist_ivtrm.txt")));
                    byte[] array = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source.ElementAt<PCKFileEntry>(0))).ToArray<byte>();

                    string tempFileName = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\surfaces\iconset\iconlist_ivtrm.txt";
                    string tempDir = Path.GetDirectoryName(tempFileName);
                    Directory.CreateDirectory(tempDir);
                    File.WriteAllBytes(tempFileName, array);
                    var iconlist_ivtrm_Read = File.ReadAllLines(tempFileName, Encoding.GetEncoding("GBK"));

                    LP.preg_max = iconlist_ivtrm_Read.Length;

                    for (int i = 0; i < iconlist_ivtrm_Read.Length; i++)
                    {
                       
                        if (iconlist_ivtrm_Read[i] != null)
                        {
                            LP.preg = i;
                            switch (i)
                            {
                                case 0:
                                    w = int.Parse(iconlist_ivtrm_Read[i]);
                                    break;
                                case 1:
                                    h = int.Parse(iconlist_ivtrm_Read[i]);
                                    break;
                                case 2:
                                    rows = int.Parse(iconlist_ivtrm_Read[i]);
                                    database.rows = rows;
                                    break;
                                case 3:
                                    cols = int.Parse(iconlist_ivtrm_Read[i]);
                                    database.cols = cols;
                                    break;
                                default:
                                    fileNames.Add(iconlist_ivtrm_Read[i]);
                                    break;
                            }

                        }
                    }
                    imageposition = new SortedList<string, Point>();
                    int x, y = 0;

                    LP.preg_max = fileNames.Count;
                    for (int a = 0; a < fileNames.Count; a++)
                    {
                        y = a / cols;
                        x = a - y * cols;
                        x = x * w;
                        y = y * h;
                        try
                        {
                            LP.preg = a;
                            imagesx.Add(a, fileNames[a]);
                            imageposition.Add(fileNames[a], new Point(x, y));
                        }
                        catch (Exception) { }

                    }

                    database.imagesx = imagesx;
                    database.imageposition = imageposition;
                    return imageposition;
                }
                else
                {
                    // No surfaces.pck file available - return empty data
                    sourceBitmap = null;
                    database.sourceBitmap = sourceBitmap;
                    imagesx = new SortedList<int, string>();
                    imageposition = new SortedList<string, Point>();
                    database.imagesx = imagesx;
                    database.imageposition = imageposition;
                    database.rows = 0;
                    database.cols = 0;
                    
                    // Don't show message here as it may be called during initial load
                    // The user will configure paths through the settings dialog
                    return imageposition;
                }

			

            
        }

        public void LoadSkillList()
        {
            try
            {
                if (MainWindow.XmlData != null && !string.IsNullOrEmpty(MainWindow.XmlData.ConfigsPckPath) && File.Exists(MainWindow.XmlData.ConfigsPckPath))
                {
                    pck = new PCKs(MainWindow.XmlData.ConfigsPckPath);
                    IEnumerable<PCKFileEntry> source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("configs\\skillstr.txt")));
                    byte[] array = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source.ElementAt<PCKFileEntry>(0))).ToArray<byte>();

                    string tempFileName = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\configs\skillstr.txt";
                    string tempDir = Path.GetDirectoryName(tempFileName);
                    Directory.CreateDirectory(tempDir);
                    File.WriteAllBytes(tempFileName, array);
                    var skillstr_Read = File.ReadAllLines(tempFileName, Encoding.GetEncoding("GBK"));
                    string result = string.Join("\n", skillstr_Read);
                    
                    if (File.Exists(tempFileName))
                    {
                        try
                        {
                            MainWindow.skillstr = result.Split(new char[] { '\"' });
                            string[] temp = MainWindow.skillstr[0].Split(new char[] { '\n' });
                            MainWindow.skillstr[0] = temp[temp.Length - 1];
                            
                        }
                        catch (Exception e)
                        {
                            // Log error instead of showing MessageBox from background thread
                            System.Diagnostics.Debug.WriteLine("ERROR LOADING SKILL LIST: " + e.Message);
                        }
                    }
                    else
                    {
                        // Log error instead of showing MessageBox from background thread
                        System.Diagnostics.Debug.WriteLine("NOT FOUND localization\\skillstr.txt!");
                    }
                    database.skillstr = MainWindow.skillstr;
                }
                else
                {
                    if (database.skillstr != null)
                    {
                        MainWindow.skillstr = database.skillstr;
                        return;
                    }
                    // No configs.pck available - use empty data
                    MainWindow.skillstr = new string[0];
                    database.skillstr = MainWindow.skillstr;
                }

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void LoadAddonList()
        {
            if (database.addonslist != null)
            {
                MainWindow.addonslist = database.addonslist;
                return;
            }
            String path = Path.GetDirectoryName(Application.ExecutablePath) + "\\resources\\data\\addon_table.txt";
            MainWindow.addonslist = new SortedList();
            if (File.Exists(path))
            {
                try
                {
                    StreamReader sr = new StreamReader(path, Encoding.Unicode);

                    char[] seperator = new char[] { '\t' };
                    string line;
                    string[] split;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (line.Contains("\t") && line != "" && !line.StartsWith("/") && !line.StartsWith("#"))
                        {
                            split = line.Split(seperator);
                            MainWindow.addonslist.Add(split[0], split[1]);
                        }
                    }

                    sr.Close();
                }
                catch (Exception e)
                {
                    // Log error instead of showing MessageBox from background thread
                    System.Diagnostics.Debug.WriteLine("ERROR LOADING ADDON LIST: " + e.Message);
                }
            }
            // If file doesn't exist, just use empty list
            database.addonslist = MainWindow.addonslist;
        }

        public void LoadLocalizationText()
        {
            MainWindow.LocalizationText = new SortedList();
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\resources\\data\\language_en.txt";
            if (File.Exists(path))
            {
                try
                {
                    StreamReader sr = new StreamReader(path, Encoding.Unicode);

                    char[] seperator = new char[] { '"' };
                    string line;
                    string[] split;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (line != "" && !line.StartsWith("/") && !line.StartsWith("#"))
                        {
                            split = line.Split(seperator);
                            MainWindow.LocalizationText.Add(split[0].Trim(), split[1]);
                        }
                    }

                    sr.Close();
                }
                catch (Exception e)
                {
                    // Log error instead of showing MessageBox from background thread
                    System.Diagnostics.Debug.WriteLine("ERROR LOADING LOCALIZATION: " + e.Message);
                }
            }
            // If file doesn't exist, just use empty list
            database.LocalizationText = MainWindow.LocalizationText;
        }

        //public void LoadInstanceList()
        //{
        //    if (database.InstanceList != null)
        //    {
        //        MainWindow.InstanceList = database.InstanceList;
        //        return;
        //    }

        //    database.defaultMapsTemplate = new SortedList<int, Map>();
        //    MainWindow.InstanceList = new SortedList();
        //    String path = Path.GetDirectoryName(Application.ExecutablePath) + "\\configs\\instance_en.txt";
        //    if (File.Exists(path))
        //    {
        //        try
        //        {
        //            StreamReader sr = new StreamReader(path, Encoding.Unicode);

        //            char[] seperator = new char[] { '\t' };
        //            string line;
        //            string[] split;
        //            while (!sr.EndOfStream)
        //            {
        //                line = sr.ReadLine();
        //                if (line.Contains("\t") && line != "" && !line.StartsWith("/") && !line.StartsWith("#"))
        //                {
        //                    split = line.Split(seperator);
        //                    if (split.Length > 2)
        //                    {
        //                        MainWindow.InstanceList.Add(split[0], " [" + split[1] + "] [" + split[2] + "] " + split[3] + "");
        //                        Map map = new Map();
        //                        map.name = split[3];
        //                        map.realName = split[2];
        //                        database.defaultMapsTemplate.Add(Int32.Parse(split[0]), map);
        //                    }
        //                    else
        //                    {
        //                        MainWindow.InstanceList.Add(split[0], split[1]);
        //                    }
        //                }
        //            }

        //            sr.Close();
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBox.Show("ERROR LOADING INSTANCE LIST\n" + e.Message);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("NOT FOUND localization:" + path + "!");
        //    }
        //    database.InstanceList = MainWindow.InstanceList;
        //}

        public void LoadBuffList()
        {

            try
            {
                if (database.buff_str != null)
                {
                    MainWindow.buff_str = database.buff_str;
                    return;
                }

                if (MainWindow.XmlData != null && !string.IsNullOrEmpty(MainWindow.XmlData.ConfigsPckPath) && File.Exists(MainWindow.XmlData.ConfigsPckPath))
                {
                    pck = new PCKs(MainWindow.XmlData.ConfigsPckPath);
                    IEnumerable<PCKFileEntry> source = pck.Files.Where<PCKFileEntry>((Func<PCKFileEntry, bool>)(i => i.Path.StartsWith("configs\\buff_str.txt")));
                    byte[] array = ((IEnumerable<byte>)pck.ReadFile(pck.PckFile, source.ElementAt<PCKFileEntry>(0))).ToArray<byte>();

                    string tempFileName = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + @"resources\configs\buff_str.txt";
                    string tempDir = Path.GetDirectoryName(tempFileName);
                    Directory.CreateDirectory(tempDir);
                    File.WriteAllBytes(tempFileName, array);
                    var buff_str_Read = File.ReadAllLines(tempFileName, Encoding.GetEncoding("GBK"));
                    string result = string.Join("\n", buff_str_Read);

                    if (File.Exists(tempFileName))
                    {
                        try
                        {
                            MainWindow.buff_str = result.Split(new char[] { '\"' });
                            string[] temp = MainWindow.buff_str[0].Split(new char[] { '\n' });
                            MainWindow.buff_str[0] = temp[temp.Length - 1];
                        }
                        catch (Exception e)
                        {
                            // Log error instead of showing MessageBox from background thread
                            System.Diagnostics.Debug.WriteLine("ERROR LOADING BUFF LIST: " + e.Message);
                        }
                    }
                    else
                    {
                        // Log error instead of showing MessageBox from background thread
                        System.Diagnostics.Debug.WriteLine("NOT FOUND localization\\skillstr.txt!");
                    }
                    database.skillstr = MainWindow.skillstr;
                }
                else
                {
                    // No configs.pck available - use empty data
                    MainWindow.buff_str = new string[0];
                    database.buff_str = MainWindow.buff_str;
                }

            }
            catch (Exception)
            {

                throw;
            }
            
            
        }
    }
}
