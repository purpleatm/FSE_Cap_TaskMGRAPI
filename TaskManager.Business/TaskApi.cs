using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TaskManager.Business.Extenstion;
using TaskManager.DataAccess;
using TaskManager.Model;

namespace TaskManager.Business
{
    public class TaskApi
    {
        /// <summary>
        /// Get parent task details
        /// </summary>
        /// <returns>List of parent task</returns>
        public IEnumerable<PARENT_TASK> GetParentTask()
        {
            var parentTasks = DataAccessManager.GetParentTasks();
            if (parentTasks != null && parentTasks.Any())
                return parentTasks.Select(p => new PARENT_TASK()
                {
                    Parent_ID= p.Parent_ID.ToString(),
                    Parent_Task= p.Parent_Task
                }).ToList();
            return null;
        }
        
        /// <summary>
        /// Get task details
        /// </summary>
        /// <returns>List of Task</returns>
        public IEnumerable<TASK_DETAILS> GetTaskDetails()
        {
            var tasks = DataAccessManager.GetTasks();
            if (tasks != null && tasks.Any())
                return tasks.Select(t => new TASK_DETAILS()
                {
                    Parent_ID = t.Parent_ID.ToString(),
                    Task_ID= t.Task_ID.ToString(),
                    Task=t.Task1,
                    Start_Date=t.Start_Date.ToCustomDate(),
                    End_Date=t.End_Date.ToCustomDate(),
                    IsActive=t.End_Date.IsActiveTask(),
                    Priority=t.Priority
                }).ToList();
            return null;
        }

        /// <summary>
        /// Add task detail
        /// </summary>
        /// <param name="taskDetail"></param>
        /// <returns>Transcation status</returns>
        public bool AddTaskDetail(TASK_DETAILS taskDetail)
        {
            Task task = new Task();
            if (taskDetail.Parent_ID != null)
            {
                task.Parent_ID = taskDetail.Parent_ID.ToGuid();
                var parentTask = DataAccessManager.GetTask(taskDetail.Parent_ID.ToGuid());
                if (parentTask == null)
                {
                    //Return invalid parent task
                }
            }
            task.Task_ID = Guid.NewGuid();
            task.Task1 = taskDetail.Task;
            task.Start_Date = DateTime.Parse(taskDetail.Start_Date);
            task.End_Date =DateTime.Parse(taskDetail.End_Date);
            task.Priority = taskDetail.Priority;
            return DataAccessManager.AddTask(task);
        }


        /// <summary>
        /// Include task detail
        /// </summary>
        /// <param name="taskDetail"></param>
        /// <returns>Transcation status</returns>
        public bool UpdateTaskDetail(TASK_DETAILS taskDetail)
        {
            Task task = new Task();
            if (taskDetail.Parent_ID != null)
            {
                task.Parent_ID = taskDetail.Parent_ID.ToGuid();
                var parentTask = DataAccessManager.GetTask(taskDetail.Parent_ID.ToGuid());
                if (parentTask == null)
                {
                    //Return invalid parent task
                }
            }
            task.Task_ID = taskDetail.Task_ID.ToGuid();            
            task.Task1 = taskDetail.Task;
            task.Start_Date = taskDetail.Start_Date.ToDateTime();
            task.End_Date = taskDetail.End_Date.ToDateTime();
            task.Priority = taskDetail.Priority;
            return DataAccessManager.UpdateTask(task);
        }


        /// <summary>
        /// Set end task status
        /// </summary>
        /// <param name="taskDetail"></param>
        /// <returns></returns>
        public bool UpdateEndTask(TASK_DETAILS taskDetail)
        {
            Task task = new Task();
            task.Task_ID = taskDetail.Task_ID.ToGuid();
            return DataAccessManager.UpdateEndTask(task);
        }
    }
}

namespace TaskManager.Business.Extenstion
{
    /// <summary>
    /// Business extention
    /// </summary>
    public static class BusinessExtention
    {

        /// <summary>
        /// ToCustomDate
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string ToCustomDate(this DateTime datetime)
        {
            string pattern = "MM/dd/yyyy";
            return datetime.ToString(pattern, CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// ToDateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value)
        {
            DateTime dateTime;
            DateTime.TryParse(value, out dateTime);
            return dateTime;
        }

        /// <summary>
        /// ToGuid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string value)
        {
            Guid guid;
            Guid.TryParse(value, out guid);
            return guid;
        }

        /// <summary>
        /// Check task is active
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int IsActiveTask(this DateTime dateTime)
        {
            return Convert.ToInt32(dateTime >= System.DateTime.Now);
        }

        /// IsAddTaskModelValid
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static bool IsAddTaskModelValid(this TASK_DETAILS task)
        {
            if (task.Parent_ID != null)
                if (!task.Parent_ID.ToGuid().IsValidGUID())
                    return false;

            if (string.IsNullOrEmpty(task.Task))
                return false;

            if (task.Priority <= 0)
                return false;

            if (!(task.Start_Date != null && task.Start_Date.ToDateTime() != System.DateTime.MinValue))
                return false;

            if (!(task.End_Date != null && task.End_Date.ToDateTime() != System.DateTime.MinValue))
                return false;

            return true;
        }

        /// <summary>
        /// IsUpdateTaskModelValid
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static bool IsUpdateTaskModelValid(this TASK_DETAILS task)
        {
            if (task.Parent_ID != null)
                if (!task.Parent_ID.ToGuid().IsValidGUID())
                    return false;

            if (!(task.Task_ID != null && task.Task_ID.ToGuid() != Guid.Empty && task.Task_ID.ToGuid().IsValidGUID()))
                return false;

                if (string.IsNullOrEmpty(task.Task))
                return false;

            if (task.Priority <= 0)
                return false;

            if (!(task.Start_Date != null && task.Start_Date.ToDateTime() != System.DateTime.MinValue))
                return false;

            if (!(task.End_Date != null && task.End_Date.ToDateTime() != System.DateTime.MinValue))
                return false;

            return true;
        }

        /// <summary>
        /// IsValidGUID
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>True/False</returns>
        public static bool IsValidGUID(this Guid guid)
        {
            Guid defaultGuid;
            Guid.TryParse(guid.ToString(), out defaultGuid);
            return defaultGuid != Guid.Empty;
        }
    }
}