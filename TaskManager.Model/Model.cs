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
}
