using System;
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
        /// GetTask
        /// </summary>
        /// <returns>List of Task</returns>
        public static Task GetTask(Guid taskId)
        {
            Task task = null;
            using (var _dbContext = new TaskManagerEntities())
            {
                task = _dbContext.Tasks.SingleOrDefault(t => t.Task_ID == taskId);
            }
            return task;
        }


        /// <summary>
        /// Add Update task
        /// </summary>
        /// <param name="task">Task</param>
        /// <returns>
        /// True - Update transaction done.
        /// False - No transaction.
        /// </returns>
        public static bool AddTask(Task task)
        {
            bool isAddSuccess = false;
            using (var _dbContext = new TaskManagerEntities())
            {
                /// Check if task already exist
                var existingTask = _dbContext.Tasks
                    .Where(c => c.Task_ID == task.Task_ID)
                    .SingleOrDefault();

                if (existingTask == null)
                {
                    ///Add parent task if not exist
                    if (task.Parent_ID != null && task.Parent_ID != System.Guid.Empty)
                    {
                        var existingParent = _dbContext.ParentTasks
                        .Where(p => p.Parent_ID == task.Parent_ID)
                        .SingleOrDefault();
                        if (existingParent == null)
                        {
                            _dbContext.ParentTasks.Add(new ParentTask()
                            {
                                Parent_ID=(System.Guid)task.Parent_ID,
                                Parent_Task= _dbContext.Tasks.SingleOrDefault(s=>s.Task_ID==task.Parent_ID ).Task1
                            });
                        }
                    }
                    /// Add task
                    _dbContext.Tasks.Add(task);

                    _dbContext.SaveChanges();
                    isAddSuccess = true;
                }
            }
            return isAddSuccess;
        }

        /// <summary>
        /// Update task
        /// </summary>
        /// <param name="task">Task</param>
        /// <returns>
        /// True - Update transaction done.
        /// False - No transaction.
        /// </returns>
        public static bool UpdateTask(Task task)
        {
            bool isUpdateSuccess = false;
            using (var _dbContext = new TaskManagerEntities())
            {
                /// Check if task already exist
                var existingTask = _dbContext.Tasks
                    .Where(c => c.Task_ID == task.Task_ID)
                    .SingleOrDefault();

                if (existingTask != null)
                {
                    ///Add parent task if not exist
                    if (task.Parent_ID != null && task.Parent_ID != System.Guid.Empty)
                    {
                        ///Add parent task if not exist
                        var existingParent = _dbContext.ParentTasks
                        .Where(p => p.Parent_ID == task.Parent_ID)
                        .SingleOrDefault();
                        if (existingParent == null)
                        {
                            _dbContext.ParentTasks.Add(new ParentTask()
                            {
                                Parent_ID = (System.Guid)task.Parent_ID,
                                Parent_Task = task.Task1
                            });
                        }
                    }

                    ///Update task
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
                _dbContext.SaveChanges();
                isUpdateSuccess = true;
            }
            return isUpdateSuccess;
        }

        /// <summary>
        /// Update end task
        /// </summary>
        /// <param name="task">Task</param>
        /// <returns>
        /// True - Update transaction done.
        /// False - No transaction.
        /// </returns>
        public static bool UpdateEndTask(Task task)
        {
            bool isUpdateSuccess = false;
            using (var _dbContext = new TaskManagerEntities())
            {
                /// Check if task already exist
                var existingTask = _dbContext.Tasks
                    .Where(c => c.Task_ID == task.Task_ID)
                    .SingleOrDefault();

                if (existingTask != null)
                {
                    ///Update tas
                    existingTask.End_Date = DateTime.Now.AddDays(-1);
                    _dbContext.SaveChanges();
                    isUpdateSuccess = true;
                }
                
            }
            return isUpdateSuccess;
        }


    }
}
