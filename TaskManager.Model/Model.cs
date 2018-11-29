using System;

namespace TaskManager.Model
{
    /// <summary>
    /// TASK_DETAILS
    /// </summary>
    public class TASK_DETAILS
    {
        public string Task_ID { get; set; }
        public string Parent_ID { get; set; }
        public string Task { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public int Priority { get; set; }
        public int IsActive { get; set; }
    }

    /// <summary>
    /// PARENT_TASK
    /// </summary>
    public class PARENT_TASK
    {
        public string Parent_ID { get; set; }
        public string Parent_Task { get; set; }
    }
}
