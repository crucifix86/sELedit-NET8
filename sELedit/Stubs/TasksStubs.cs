using System;
using System.Collections.Generic;

namespace tasks
{
    public class ATaskTempl
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool m_Award_F { get; set; }
        public object m_Award_FPointer { get; set; }
        public object m_Award_S { get; set; }
        public int m_ulType { get; set; }
        public List<ATaskTempl> pSub { get; set; }
        
        public ATaskTempl()
        {
            pSub = new List<ATaskTempl>();
        }
        
        public ATaskTempl(int id, string name)
        {
            ID = id;
            Name = name;
            pSub = new List<ATaskTempl>();
        }
        
        public void AddNode(ATaskTempl node)
        {
            pSub.Add(node);
        }
    }
    
    public class TASK_PACK_HEADER
    {
        public int Version { get; set; }
        public int TaskCount { get; set; }
    }
    
    public static class GlobalData
    {
        public static object Data { get; set; }
    }
    
    public class TaskManager
    {
        public List<ATaskTempl> Tasks { get; set; }
        
        public TaskManager()
        {
            Tasks = new List<ATaskTempl>();
        }
    }
}