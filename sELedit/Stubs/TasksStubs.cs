using System;
using System.Collections.Generic;

namespace tasks
{
    public class CandItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public AwardItem[] m_AwardItems { get; set; }
        
        public CandItem()
        {
            m_AwardItems = new AwardItem[0];
        }
    }
    
    public class AwardItem
    {
        public int m_ulItemTemplId { get; set; }
        public int m_ulItemNum { get; set; }
        public float m_fProb { get; set; }
    }
    
    public class Award
    {
        public CandItem[] m_CandItems { get; set; }
        
        public Award()
        {
            m_CandItems = new CandItem[0];
        }
    }
    
    // Wrapper class to make List<ATaskTempl> behave like an array
    public class ATaskTemplArray
    {
        private List<ATaskTempl> _list;
        
        public ATaskTemplArray(List<ATaskTempl> list)
        {
            _list = list ?? new List<ATaskTempl>();
        }
        
        public int Length => _list.Count;
        
        public ATaskTempl this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }
        
        public void Add(ATaskTempl item)
        {
            _list.Add(item);
        }
        
        public List<ATaskTempl> ToList()
        {
            return _list;
        }
        
        public static implicit operator ATaskTempl[](ATaskTemplArray array)
        {
            return array._list.ToArray();
        }
        
        public static implicit operator ATaskTemplArray(List<ATaskTempl> list)
        {
            return new ATaskTemplArray(list);
        }
    }
    
    public class ATaskTempl
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool m_Award_F { get; set; }
        public Award m_Award_FPointer { get; set; }
        public Award m_Award_S { get; set; }
        public int m_ulType { get; set; }
        private List<ATaskTempl> _pSub;
        public ATaskTemplArray pSub 
        { 
            get { return new ATaskTemplArray(_pSub); }
            set { _pSub = value?.ToList() ?? new List<ATaskTempl>(); }
        }
        
        public ATaskTempl()
        {
            _pSub = new List<ATaskTempl>();
            m_Award_FPointer = new Award();
            m_Award_S = new Award();
        }
        
        public ATaskTempl(int id, string name)
        {
            ID = id;
            Name = name;
            _pSub = new List<ATaskTempl>();
            m_Award_FPointer = new Award();
            m_Award_S = new Award();
        }
        
        public ATaskTempl(int version, System.IO.BinaryReader reader)
        {
            _pSub = new List<ATaskTempl>();
            m_Award_FPointer = new Award();
            m_Award_S = new Award();
            // Stub implementation - would normally read from binary
        }
        
        public void AddNode(ATaskTempl node)
        {
            _pSub.Add(node);
        }
        
        public void AddNode(int index, ATaskTempl node)
        {
            if (index >= _pSub.Count)
                _pSub.Add(node);
            else
                _pSub.Insert(index, node);
        }
        
        public void AddNode(System.Windows.Forms.TreeNodeCollection nodes, int iconIndex)
        {
            // Stub implementation for TreeView integration
            var node = new System.Windows.Forms.TreeNode(this.Name ?? "Task " + this.ID);
            node.ImageIndex = iconIndex;
            node.SelectedImageIndex = iconIndex;
            nodes.Add(node);
        }
    }
    
    public class TASK_PACK_HEADER
    {
        public int magic { get; set; }
        public int version { get; set; }
        public int item_count { get; set; }
        
        public TASK_PACK_HEADER() { }
        
        public TASK_PACK_HEADER(System.IO.BinaryReader reader)
        {
            magic = reader.ReadInt32();
            version = reader.ReadInt32();
            item_count = reader.ReadInt32();
        }
    }
    
    public static class GlobalData
    {
        public static object Data { get; set; }
        public static int[] Versions { get; set; } = new int[100];
        public static int NewID { get; set; }
        public static int version { get; set; }
    }
    
    public class TaskManager
    {
        public List<ATaskTempl> Tasks { get; set; }
        
        public TaskManager()
        {
            Tasks = new List<ATaskTempl>();
        }
    }
    
    // Helper methods
    public static class TaskExtensions
    {
        public static ATaskTempl[] ToTaskArray(this List<ATaskTempl> list)
        {
            return list?.ToArray() ?? new ATaskTempl[0];
        }
    }
}