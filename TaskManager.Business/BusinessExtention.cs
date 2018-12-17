using System;
using System.Globalization;
using TaskManager.Model;

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
        public static string ToCustomDate(this Nullable<DateTime> datetime)
        {
            string pattern = "MM/dd/yyyy";
            return datetime?.ToString(pattern, CultureInfo.InvariantCulture);
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
        public static int IsActive(this Nullable<DateTime> dateTime)
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
                if(task.Parent_ID <= 0)
                    return false;

            if (string.IsNullOrEmpty(task.Task))
                return false;

            if (task.Priority <= 0)
                return false;

            if (!(task.Start_Date != null && task.Start_Date != System.DateTime.MinValue))
                return false;

            if (!(task.End_Date != null && task.End_Date != System.DateTime.MinValue))
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
                if (task.Parent_ID <= 0)
                    return false;

            if (!(task.Task_ID != null && task.Task_ID > 0))
                return false;

            if (string.IsNullOrEmpty(task.Task))
                return false;

            if (task.Priority <= 0)
                return false;

            if (!(task.Start_Date != null && task.Start_Date != System.DateTime.MinValue))
                return false;

            if (!(task.End_Date != null && task.End_Date != System.DateTime.MinValue))
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