using System;

namespace TaskManager.Model
{
    public class TASK_DETAILS
    {
        public string Task_ID { get; set; }
        public string Parent_ID { get; set; }
        public string Task { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public int Priority { get; set; }
        public int IsActive { get; set; }
    }

    public class PARENT_TASK
    {
        public string Parent_ID { get; set; }
        public string Parent_Task { get; set; }
    }

    //public class TaskModel
    //{
    //    public System.Guid Task_ID { get; set; }
    //    public System.Guid Parent_ID { get; set; }
    //    public string Task { get; set; }
    //    public System.DateTime Start_Date { get; set; }
    //    public System.DateTime End_Date { get; set; }
    //    public int Priority { get; set; }
    //    public int Action { get; set; }

    //    public ParentTaskModel Parent { get; set; }
    //}

    //public class ParentTaskModel
    //{
    //    public System.Guid Parent_ID { get; set; }
    //    public string Parent_Task { get; set; }
    //}

    //public class APIResponse
    //{
    //    public string errorInfo { get; set; }
    //    public bool status { get; set; }
    //}
}
