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
        #region Task data access methods
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
                parentTasks = _dbContext.Tasks.Include(t=>t.Project).ToList();
            }
            return parentTasks;
        }

        /// <summary>
        /// GetTask
        /// </summary>
        /// <returns>List of Task</returns>
        public static Task GetTask(int taskId)
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
                    if (task.Parent_ID != null && task.Parent_ID != default(int))
                    {
                        var existingParent = _dbContext.ParentTasks
                        .Where(p => p.Parent_ID == task.Parent_ID)
                        .SingleOrDefault();
                        if (existingParent == null)
                        {
                            _dbContext.ParentTasks.Add(new ParentTask()
                            {
                                Parent_ID=(int)task.Parent_ID,
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
                    if (task.Parent_ID != null && task.Parent_ID != default(int))
                    {
                        ///Add parent task if not exist
                        var existingParent = _dbContext.ParentTasks
                        .Where(p => p.Parent_ID == task.Parent_ID)
                        .SingleOrDefault();
                        if (existingParent == null)
                        {
                            _dbContext.ParentTasks.Add(new ParentTask()
                            {
                                Parent_ID = (int)task.Parent_ID,
                                Parent_Task = task.Task1
                            });
                        }
                    }

                    ///Update task
                    if (!(task.Task_ID == null || task.Task_ID == default(int)))
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

        /// <summary>
        /// Get next task ID
        /// </summary>
        /// <returns></returns>
        public static int GetNextTaskID()
        {
            int lastId;
            using (var _dbContext = new TaskManagerEntities())
            {
                if (!(_dbContext.Tasks != null && _dbContext.Tasks.Any()))
                    return 1;
                lastId =_dbContext.Tasks.Select(x => x.Task_ID).Max();
            }
            return lastId+1;
        }

        #endregion;

        #region User data access methods
        /// <summary>
        /// GetNextUserID
        /// </summary>
        /// <returns></returns>
        public static int GetNextUserID()
        {
            int lastId;
            using (var _dbContext = new TaskManagerEntities())
            {
                if (!(_dbContext.Users != null && _dbContext.Users.Any()))
                    return 1;
                lastId = _dbContext.Users.Select(x => x.User_ID).Max();
            }
            return lastId+1;
        }

        /// <summary>
        /// GetUsers
        /// </summary>
        /// <returns></returns>
        public static List<User> GetUsers()
        {
            List<User> users = null;
            using (var _dbContext = new TaskManagerEntities())
            {
                users = _dbContext.Users.ToList();
            }
            return users;
        }
        
        /// <summary>
        /// AddUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool AddUser(User user)
        {
            bool isAddSuccess = false;
            using (var _dbContext = new TaskManagerEntities())
            {
                    /// Add user
                    _dbContext.Users.Add(user);
                    _dbContext.SaveChanges();
                    isAddSuccess = true;
            }
            return isAddSuccess;
        }

        /// <summary>
        /// UpdateUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool UpdateUser(User user)
        {
            bool isUpdateSuccess = false;
            using (var _dbContext = new TaskManagerEntities())
            {
                /// Check if task already exist
                var existingUser = _dbContext.Users
                    .Where(c => c.User_ID == user.User_ID)
                    .SingleOrDefault();

                if (existingUser != null)
                {
                    existingUser.FirstName = user.FirstName;
                    existingUser.LastName = user.LastName;
                    existingUser.Employee_ID = user.Employee_ID;
                    _dbContext.SaveChanges();
                    isUpdateSuccess = true;
                }
            }
            return isUpdateSuccess;
        }

        /// <summary>
        /// Delet User
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>
        /// True - Update transaction done.
        /// False - No transaction.
        /// </returns>
        public static bool DeleteUser(User user)
        {
            bool isUpdateSuccess = false;
            using (var _dbContext = new TaskManagerEntities())
            {
                /// Check if task already exist
                var existingUser = _dbContext.Users
                    .Where(c => c.User_ID == user.User_ID)
                    .SingleOrDefault();
                if (existingUser != null)
                {
                    _dbContext.Users.Remove(existingUser);
                    _dbContext.SaveChanges();
                    isUpdateSuccess = true;
                }
            }
            return isUpdateSuccess;
        }
        #endregion;

        #region Project data access methods

        /// <summary>
        /// GetNextProjectID
        /// </summary>
        /// <returns></returns>
        public static int GetNextProjectID()
        {
            int lastId;
            using (var _dbContext = new TaskManagerEntities())
            {
                if (!(_dbContext.Projects != null && _dbContext.Projects.Any()))
                    return 1;
                lastId = _dbContext.Projects.Select(x => x.Project_ID).Max();
            }
            return lastId+1;
        }

        /// <summary>
        /// Get projects
        /// </summary>
        /// <returns></returns>
        public static List<Project> GetProjects()
        {
            List<Project> projects = null;
            using (var _dbContext = new TaskManagerEntities())
            {
                projects = _dbContext.Projects.Include(p => p.Tasks).ToList();
            }
            return projects;
        }

        /// <summary>
        /// Get project id
        /// </summary>
        /// <returns></returns>
        public static Project GetProject(int projectId)
        {
            Project project = null;
            using (var _dbContext = new TaskManagerEntities())
            {
                project = _dbContext.Projects.SingleOrDefault(p => p.Project_ID == projectId);
            }
            return project;
        }

        /// <summary>
        /// Get manager details
        /// </summary>
        /// <returns></returns>
        public static List<Project> GetManagerDetails()
        {
            List<Project> projects = null;
            using (var _dbContext = new TaskManagerEntities())
            {
                projects = _dbContext.Projects.ToList();
            }
            return projects;
        }

        /// <summary>
        /// Add project
        /// </summary>
        /// <param name="project">Project</param>
        /// <returns></returns>
        public static bool AddProject(Project project)
        {
            bool isAddSuccess = false;
            using (var _dbContext = new TaskManagerEntities())
            {
                /// Add user
                _dbContext.Projects.Add(project);
                _dbContext.SaveChanges();
                isAddSuccess = true;
            }
            return isAddSuccess;
        }

        /// <summary>
        /// Update project
        /// </summary>
        /// <param name="project">Project</param>
        /// <returns></returns>
        public static bool UpdateProject(Project project)
        {
            bool isUpdateSuccess = false;
            using (var _dbContext = new TaskManagerEntities())
            {
                /// Check if task already exist
                var existingProject = _dbContext.Projects
                    .Where(c => c.Project_ID == project.Project_ID)
                    .SingleOrDefault();

                if (existingProject != null)
                {
                    existingProject.Project1 = project.Project1;
                    existingProject.Start_Date = project.Start_Date;
                    existingProject.End_Date = project.End_Date;
                    existingProject.Priority = project.Priority;
                    _dbContext.SaveChanges();
                    isUpdateSuccess = true;
                }
            }
            return isUpdateSuccess;
        }

        /// <summary>
        /// Update end project
        /// </summary>
        /// <param name="project">Project</param>
        /// <returns></returns>
        public static bool UpdateEndProject(Project project)
        {
            bool isUpdateSuccess = false;
            using (var _dbContext = new TaskManagerEntities())
            {
                /// Check if task already exist
                var existingProject = _dbContext.Projects
                    .Where(c => c.Project_ID == project.Project_ID)
                    .SingleOrDefault();

                if (existingProject != null)
                {
                    existingProject.End_Date = project.End_Date;   
                    _dbContext.SaveChanges();
                    isUpdateSuccess = true;
                }
            }
            return isUpdateSuccess;
        }
        #endregion
    }
}
