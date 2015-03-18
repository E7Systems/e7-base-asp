using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Rsff.BusinessLayer
{
    public class ProjectsDummyData
    {
        public List<Project> GetProjectsDummyDataList()
        {
            Random random = new Random();

            List<Project> projects = new List<Project>();

            DataTable tbl = new DataTable();
            
            for (int i = 0; i < 50; i++)
            {
                Project project = new Project();
                project.Address = string.Format("Project number {0} Address", i);
                project.APN = String.Format("{0}-{1}-{2}", random.Next(0, 999), random.Next(0, 999), random.Next(0, 99));
                project.Notes = string.Format("Random notes for project id # {0}.  {1}", i, Guid.NewGuid().ToString());
                project.PlanCheckNumber = random.Next(1,9999);
                project.ProjectID = i;
                project.ProjectName = string.Format("Project number {0}", i);
                projects.Add(project);
            }
            return projects;

            

        }
    }
}
