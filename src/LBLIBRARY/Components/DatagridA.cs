namespace LBLIBRARY.Components
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;

    public class DatagridA : DataGrid
    {
        public List<int> SelectedRowsIndexes
        {
            get
            {
                List<int> list = new List<int>();
                foreach (object obj2 in base.SelectedItems)
                {
                    list.Add(base.Items.IndexOf(obj2));
                }
                return list;
            }
        }
    }
}

