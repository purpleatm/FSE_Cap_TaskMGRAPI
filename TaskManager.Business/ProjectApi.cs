using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Business.Extenstion;
using TaskManager.DataAccess;
using TaskManager.Model;

namespace TaskManager.Business
{
    public class ProjectApi
    {
        public IEnumerable<PROJECT_DETAILS> GetProjects()
        {
            var projects = DataAccessManager.GetProjects();
            if (projects != null && projects.Any())
                return projects.Select(p => new PROJECT_DETAILS()
                {
                    Project_ID= p.Project_ID,
                     End_Date= p.End_Date,
                     Start_Date=p.Start_Date,
                     Project=p.Project1,
                     Priority=p.Priority,
                     TaskCount=p.Tasks.Count,
                     Is_Active= p.End_Date.IsActive(),
                     ProjStatus = p.End_Date.IsActive() == 1 ? "Pending" : "Completed",
                     Active_Progress= Convert.ToString((p.Priority * 100) / 30)
                }).ToList();
            return null;
        }

        public IEnumerable<PROJECT_DETAILS> GetManagers()
        {
            var users = DataAccessManager.GetUsers();
            if (users != null && users.Any())
                return users.Select(p => new PROJECT_DETAILS()
                {
                   Manager_ID=Convert.ToInt32(p.Employee_ID)
                }).Distinct().ToList();
            return null;
        }

        public IEnumerable<PROJECT_DETAILS> GetProjectName()
        {
            var projects = DataAccessManager.GetProjects();
            if (projects != null && projects.Any())
                return projects.Select(p => new PROJECT_DETAILS()
                {
                    Project_ID = p.Project_ID,
                    Project = p.Project1
                }).ToList();
            return null;
        }

        public bool AddProject(PROJECT_DETAILS projectDetail)
        {
            Project project = new Project();
            project.Project_ID = DataAccessManager.GetNextProjectID();
            project.Project1 = projectDetail.Project;
            project.Start_Date = projectDetail.Start_Date;
            project.End_Date = projectDetail.End_Date;
            project.Priority = projectDetail.Priority;
            return DataAccessManager.AddProject(project);
        }
        
        public bool UpdateProject(PROJECT_DETAILS projectDetail)
        {
            Project project = new Project();
            project.Project_ID =(int)projectDetail.Project_ID;
            project.Project1 = projectDetail.Project;
            project.Start_Date = projectDetail.Start_Date;
            project.End_Date = projectDetail.End_Date;
            project.Priority = projectDetail.Priority;
            return DataAccessManager.UpdateProject(project);
        }
        
        public bool EndProejct(PROJECT_DETAILS projectDetail)
        {
            Project project = new Project();
            project.Project_ID = Convert.ToInt32(projectDetail.Project_ID);
            project.End_Date = DateTime.Now.AddDays(-1);
            return DataAccessManager.UpdateEndProject(project);
        }
    }
}

