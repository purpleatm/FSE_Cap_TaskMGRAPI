using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Business.Extenstion;
using TaskManager.DataAccess;
using TaskManager.Model;

namespace TaskManager.Business
{
    public class UserApi
    {
        public IEnumerable<USER_DETAILS> GetUsers()
        {
            var users = DataAccessManager.GetUsers();
            if (users != null && users.Any())
                return users.Select(p => new USER_DETAILS()
                {
                    User_ID= p.User_ID,
                    Employee_ID= Convert.ToInt32(p.Employee_ID),
                    First_Name=p.FirstName,
                    Last_Name=p.LastName,
                    Project_ID=p.Project_ID,
                    Task_ID=p.Task_ID
                }).ToList();
            return null;
        }
        
        public bool AddUsers(USER_DETAILS userDetail)
        {
            var user = new User();
            user.User_ID = DataAccessManager.GetNextUserID();
            user.FirstName = userDetail.First_Name;
            user.LastName = userDetail.Last_Name;
            user.Project_ID = userDetail.Project_ID;
            user.Employee_ID = Convert.ToString(userDetail.Employee_ID);
            return DataAccessManager.AddUser(user);
        }
        
        public bool UpdateUsers(USER_DETAILS userDetail)
        {
            var user = new User();
            user.User_ID = Convert.ToInt32(userDetail.User_ID);
            user.FirstName = userDetail.First_Name;
            user.LastName = userDetail.Last_Name;
            user.Project_ID = userDetail.Project_ID;
            user.Employee_ID = Convert.ToString(userDetail.Employee_ID);
            return DataAccessManager.UpdateUser(user);
        }
        
        public bool DeleteUser(USER_DETAILS userDetail)
        {
            var user = new User();
            user.User_ID = Convert.ToInt32(userDetail.User_ID);
            return DataAccessManager.DeleteUser(user);
        }
    }
}

