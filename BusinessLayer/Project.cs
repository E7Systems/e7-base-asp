using System.Text;

namespace Rsff.BusinessLayer
{
    //Project Entity
    public class Project
    {
        public int ProjectID { get; set; }
        public string Address { get; set; }
        public string APN { get; set; }
        public string ProjectName { get; set; }
        public int PlanCheckNumber { get; set; }
        public string Notes { get; set; }

        #region ToString() override
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("ProjectID = {0}", this.ProjectID);
            sb.AppendFormat("Address = {0}", this.Address);
            sb.AppendFormat("APN = {0}", this.APN);
            sb.AppendFormat("ProjectName = {0}", this.ProjectName);
            sb.AppendFormat("PlanCheckNumber = {0}", this.PlanCheckNumber);
            sb.AppendFormat("ProjectID = {0}", this.Notes);
            return sb.ToString(); 
        #endregion
        }
    }
}
