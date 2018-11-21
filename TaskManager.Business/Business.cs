using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Model;
using TaskManager.DataAccess;

namespace TaskManager.Business
{
    public static class Business
    {
        public static APIResponse AddUpdateTask(TaskModel objTaskModel)
        {
            return TaskManager.DataAccess.DataAccess.AddUpdateTask(objTaskModel);
        }
    }
}
