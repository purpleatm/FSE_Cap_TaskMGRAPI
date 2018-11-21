using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Model;


namespace TaskManager.DataAccess
{
    public static class DataAccess
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
    }
}
