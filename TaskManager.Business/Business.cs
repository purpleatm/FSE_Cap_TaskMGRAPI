using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Model;
using TaskManager.DataAccess;

namespace TaskManager.Business
{
    public class Business
    {
        public APIResponse AddUpdateTask(TaskModel objTaskModel)
        {
            return TaskManager.DataAccess.DataAccess.AddUpdateTask(objTaskModel);
        }


        /// <summary>
        /// Search task
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        public List<TaskModel> SearchTask(TaskModel taskModel)
        {

            return TaskManager.DataAccess.DataAccess

            return new List<TaskModel>();
        }


    }
}
