using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HttpWebRequestHelper
{

    public class CustomData
    {
    }

    public class Headcount
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string number { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jobName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int needNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string departmentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string commitment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ownerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string managerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string creatorId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int hireMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createdAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string updatedAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string education { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string maxSalary { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string minSalary { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string locationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string completeDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> connectedJobIds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> connectedJobs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int usedNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int remainNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CustomData customData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> addressInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> jobPriority { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<JobRanksItem> jobRanks { get; set; }
    }

    public class JobRanksItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int hcId { get; set; }
    }
    public class GA_HCResponseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Headcount headcount { get; set; }
    }

}
