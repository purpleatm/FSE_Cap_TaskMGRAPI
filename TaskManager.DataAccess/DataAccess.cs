using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Model;


namespace TaskManager.DataAccess
{
    public class DataAccess
    {
        public static APIResponse AddUpdateTask(TaskModel objTaskModel)
        {
            APIResponse objAPIResponse = new APIResponse();
            Task objTask = new Task
            {
                Task_ID = objTaskModel.Task_ID,
                Parent_ID = objTaskModel.Parent_ID,
                Task1 = objTaskModel.Task,
                Start_Date = objTaskModel.Start_Date,
                End_Date = objTaskModel.End_Date,
                Priority = objTaskModel.Priority
            };

            using (TaskManagerEntities context = new TaskManagerEntities())
            {
                if (objTaskModel.Action == 0)
                {
                    context.Tasks.Add(objTask);
                    context.SaveChanges();
                }
                else
                {
                    Task dbTask = context.Tasks.ToList().Find(x => x.Task_ID == objTaskModel.Task_ID);
                    dbTask.Task1 = objTaskModel.Task;
                    dbTask.Start_Date = objTaskModel.Start_Date;
                    dbTask.End_Date = objTaskModel.End_Date;
                    dbTask.Priority = objTaskModel.Priority;
                }
            }

            return objAPIResponse;
        }

        /// <summary>
        /// Add parent task
        /// </summary>
        /// <param name="parentTask">parentTask</param>
        /// <returns>
        /// True - Add transaction done.
        /// False - No transaction.
        /// </returns>
        public static bool AddParentTask(ParentTask parentTask)
        {
            bool isAddSuccess = false;
            if (parentTask != null)
            {
                APIResponse objAPIResponse = new APIResponse();
                using (var context = new TaskManagerEntities())
                {
                    context.ParentTasks.Add(parentTask);
                    context.SaveChanges();
                    isAddSuccess = true;
                }
            }
            return isAddSuccess;
        }

        /// <summary>
        /// Update parent task
        /// </summary>
        /// <param name="parentTask">parentTask</param>
        /// <returns>
        /// True - Update transaction done.
        /// False - No transaction.
        /// </returns>
        public static bool UpdateParentTask(ParentTask model)
        {
            bool isUpdateSuccess = false;
            using (var _dbContext = new TaskManagerEntities())
            {
                var existingParent = _dbContext.ParentTasks
                        .Where(p => p.Parent_ID == model.Parent_ID)
                        .Include(p => p.Tasks)
                        .SingleOrDefault();

                if (existingParent != null)
                {
                    // Update parent
                    _dbContext.Entry(existingParent).CurrentValues.SetValues(model);

                    // Delete children
                    foreach (var existingChild in existingParent.Tasks.ToList())
                    {
                        if (!model.Tasks.Any(c => c.Task_ID == existingChild.Task_ID))
                            _dbContext.Tasks.Remove(existingChild);
                    }

                    // Update and Insert children
                    foreach (var childModel in model.Tasks)
                    {
                        var existingChild = existingParent.Tasks
                            .Where(c => c.Task_ID == childModel.Task_ID)
                            .SingleOrDefault();

                        if (existingChild != null)
                        {
                            // Update child
                            childModel.Parent_ID = existingChild.Parent_ID;
                            _dbContext.Entry(existingChild).CurrentValues.SetValues(childModel);
                        }
                        else
                        {
                            // Insert child
                            existingParent.Tasks.Add(childModel);
                        }
                    }
                    _dbContext.SaveChanges();
                    isUpdateSuccess = true;
                }
            }
            return isUpdateSuccess;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public List<Task> SearchTask(Task task)
        {
            return new List<Task>();
        }

    }
}
