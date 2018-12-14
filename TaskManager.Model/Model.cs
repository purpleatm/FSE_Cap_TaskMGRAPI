using System;

namespace TaskManager.Model
{
    /// <summary>
    /// TASK_DETAILS
    /// </summary>
    public class TASK_DETAILS
    {
        public Nullable<int> Task_ID { get; set; }
        public Nullable<int> Parent_ID { get; set; }
        public string Parent_Task { get; set; }
        public string Task { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<int> Project_ID { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> User_ID { get; set; }
        public string Action { get; set; }
        public Nullable<int> Is_Active { get; set; }
        public string Project_Name { get; set; }
    }

    /// <summary>
    /// PARENT_TASK
    /// </summary>
    public class PARENT_TASK
    {
        public Nullable<int> Parent_ID { get; set; }
        public string Parent_Task { get; set; }
    }

    public class USER_DETAILS
    {
        public Nullable<int> User_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public Nullable<int> Employee_ID { get; set; }
        public Nullable<int> Project_ID { get; set; }
        public Nullable<int> Task_ID { get; set; }
    }

    public class PROJECT_DETAILS
    {
        public Nullable<int> Project_ID { get; set; }
        public string Project { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<int> Manager_ID { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> TaskCount { get; set; }
        public string ProjStatus { get; set; }
        public string Action { get; set; }
        public Nullable<int> Is_Active { get; set; }
        public string Active_Progress { get; set; }
    }


}
