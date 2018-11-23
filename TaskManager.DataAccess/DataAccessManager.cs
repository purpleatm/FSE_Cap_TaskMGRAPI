using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TaskManager.DataAccess
{
    /// <summary>
    /// DataAccess class contains help methods 
    /// to store and reterive data from DB.
    /// </summary>
    public class DataAccessManager
    {
        /// <summary>
        /// GetParentTask
        /// </summary>
        /// <returns>List of ParentTask</returns>
        public static List<ParentTask> GetParentTasks()
        {
            List<ParentTask> parentTasks = null;
            using (var _dbContext = new TaskManagerEntities())
            {
                parentTasks = _dbContext.ParentTasks.ToList();
            }
            return parentTasks;
        }

        /// <summary>
        /// GetTask
        /// </summary>
        /// <returns>List of Task</returns>
        public static List<Task> GetTasks()
        {
            List<Task> parentTasks = null;
            using (var _dbContext = new TaskManagerEntities())
            {
                parentTasks = _dbContext.Tasks.ToList();
            }
            return parentTasks;
        }

        /// <summary>
        /// Add Update task
        /// </summary>
        /// <param name="task">Task</param>
        /// <returns>
        /// True - Update transaction done.
        /// False - No transaction.
        /// </returns>
        public static bool AddUpdateTask(Task task)
        {
            bool isAddorUpdateSuccess = false;
            using (var _dbContext = new TaskManagerEntities())
            {
                /// Check if task already exist
                var existingTask = _dbContext.Tasks
                    .Where(c => c.Task_ID == task.Task_ID)
                    .SingleOrDefault();

                if (existingTask != null)
                {
                    /// Update task
                    if (!(task.Parent_ID != null && task.Parent_ID != System.Guid.Empty))
                        task.Parent_ID = existingTask.Parent_ID;

                    if (!(task.Task_ID == null || task.Task_ID == System.Guid.Empty))
                        task.Task_ID = existingTask.Task_ID;

                    if (string.IsNullOrEmpty(task.Task1))
                        task.Task1 = existingTask.Task1;

                    if (task.Priority <= 0)
                        task.Priority = existingTask.Priority;

                    if (!(task.Start_Date != null && task.Start_Date != System.DateTime.MinValue))
                        task.Start_Date = existingTask.Start_Date;

                    if (!(task.End_Date != null))
                        task.End_Date = existingTask.End_Date;

                    _dbContext.Entry(existingTask).CurrentValues.SetValues(task);
                }
                else
                {
                    /// Insert task
                    _dbContext.Tasks.Add(task);
                }
                _dbContext.SaveChanges();
                isAddorUpdateSuccess = true;
            }
            return isAddorUpdateSuccess;
        }


        ///// <summary>
        ///// Add update parent task
        ///// </summary>
        ///// <param name="parentTask">parentTask</param>
        ///// <returns>
        ///// True - Update transaction done.
        ///// False - No transaction.
        ///// </returns>
        //public static bool AddUpdateParentTask(ParentTask model)
        //{
        //    bool isUpdateSuccess = false;
        //    using (var _dbContext = new TaskManagerEntities())
        //    {
        //        var existingParent = _dbContext.ParentTasks
        //                .Where(p => p.Parent_ID == model.Parent_ID)
        //                .Include(p => p.Tasks)
        //                .SingleOrDefault();

        //        if (existingParent != null)
        //        {
        //            // Update parent
        //            _dbContext.Entry(existingParent).CurrentValues.SetValues(model);

        //            // Delete children
        //            foreach (var existingChild in existingParent.Tasks.ToList())
        //            {
        //                if (!model.Tasks.Any(c => c.Task_ID == existingChild.Task_ID))
        //                    _dbContext.Tasks.Remove(existingChild);
        //            }

        //            // Update and Insert children
        //            foreach (var childModel in model.Tasks)
        //            {
        //                var existingChild = existingParent.Tasks
        //                    .Where(c => c.Task_ID == childModel.Task_ID)
        //                    .SingleOrDefault();

        //                if (existingChild != null)
        //                {
        //                    // Update child
        //                    childModel.Parent_ID = existingChild.Parent_ID;
        //                    _dbContext.Entry(existingChild).CurrentValues.SetValues(childModel);
        //                }
        //                else
        //                {
        //                    // Insert child
        //                    existingParent.Tasks.Add(childModel);
        //                }
        //            }
        //            _dbContext.SaveChanges();
        //            isUpdateSuccess = true;
        //        }
        //        //else
        //        //{
        //        //    ///Add parent and child task
        //        //    _dbContext.ParentTasks.Add(model);
        //        //}
        //    }
        //    return isUpdateSuccess;
        //}

        #region Not Required
        //public static APIResponse AddUpdateTask(TaskModel objTaskModel)
        //{
        //    APIResponse objAPIResponse = new APIResponse();
        //    Task objTask = new Task
        //    {
        //        Task_ID = objTaskModel.Task_ID,
        //        Parent_ID = objTaskModel.Parent_ID,
        //        Task1 = objTaskModel.Task,
        //        Start_Date = objTaskModel.Start_Date,
        //        End_Date = objTaskModel.End_Date,
        //        Priority = objTaskModel.Priority
        //    };

        //    using (TaskManagerEntities context = new TaskManagerEntities())
        //    {
        //        if (objTaskModel.Action == 0)
        //        {
        //            context.Tasks.Add(objTask);
        //            context.SaveChanges();
        //        }
        //        else
        //        {
        //            Task dbTask = context.Tasks.ToList().Find(x => x.Task_ID == objTaskModel.Task_ID);
        //            dbTask.Task1 = objTaskModel.Task;
        //            dbTask.Start_Date = objTaskModel.Start_Date;
        //            dbTask.End_Date = objTaskModel.End_Date;
        //            dbTask.Priority = objTaskModel.Priority;
        //        }
        //    }

        //    return objAPIResponse;
        //}

        ///// <summary>
        ///// Add parent task
        ///// </summary>
        ///// <param name="parentTask">parentTask</param>
        ///// <returns>
        ///// True - Add transaction done.
        ///// False - No transaction.
        ///// </returns>
        //public static bool AddParentTask(ParentTask parentTask)
        //{
        //    bool isAddSuccess = false;
        //    if (parentTask != null)
        //    {
        //        APIResponse objAPIResponse = new APIResponse();
        //        using (var context = new TaskManagerEntities())
        //        {
        //            context.ParentTasks.Add(parentTask);
        //            context.SaveChanges();
        //            isAddSuccess = true;
        //        }
        //    }
        //    return isAddSuccess;
        //}

        ///// <summary>
        ///// Update parent task
        ///// </summary>
        ///// <param name="parentTask">parentTask</param>
        ///// <returns>
        ///// True - Update transaction done.
        ///// False - No transaction.
        ///// </returns>
        //public static bool UpdateParentTask(ParentTask model)
        //{
        //    bool isUpdateSuccess = false;
        //    using (var _dbContext = new TaskManagerEntities())
        //    {
        //        var existingParent = _dbContext.ParentTasks
        //                .Where(p => p.Parent_ID == model.Parent_ID)
        //                .Include(p => p.Tasks)
        //                .SingleOrDefault();

        //        if (existingParent != null)
        //        {
        //            // Update parent
        //            _dbContext.Entry(existingParent).CurrentValues.SetValues(model);

        //            // Delete children
        //            foreach (var existingChild in existingParent.Tasks.ToList())
        //            {
        //                if (!model.Tasks.Any(c => c.Task_ID == existingChild.Task_ID))
        //                    _dbContext.Tasks.Remove(existingChild);
        //            }

        //            // Update and Insert children
        //            foreach (var childModel in model.Tasks)
        //            {
        //                var existingChild = existingParent.Tasks
        //                    .Where(c => c.Task_ID == childModel.Task_ID)
        //                    .SingleOrDefault();

        //                if (existingChild != null)
        //                {
        //                    // Update child
        //                    childModel.Parent_ID = existingChild.Parent_ID;
        //                    _dbContext.Entry(existingChild).CurrentValues.SetValues(childModel);
        //                }
        //                else
        //                {
        //                    // Insert child
        //                    existingParent.Tasks.Add(childModel);
        //                }
        //            }
        //            _dbContext.SaveChanges();
        //            isUpdateSuccess = true;
        //        }
        //    }
        //    return isUpdateSuccess;
        //}
        #endregion
    }
}
