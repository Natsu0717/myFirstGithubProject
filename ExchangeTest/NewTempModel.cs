using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeTest
{
    public class NewTempModel
    {
        public NewTempModel()
        {
            this.Incident_Ref = 1;
        }
        public int Incident_Ref { get; set; }

        public string Application { get; set; }

        public string Request_Date_Time { get; set; }

        public string Severity_Level { get; set; }

        public string Feedback_Source { get; set; }

        public string Requestor_Email { get; set; }

        public string Impacted_BU { get; set; }

        public string Category { get; set; }

        public string Sub_Category { get; set; }

        public string Issue_Description { get; set; }

        public string Caused_By { get; set; }

        public string Resolution_Detail { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string Resolver { get; set; }

        public string Closure_Date_Time { get; set; }

        public string Duration { get; set; }

        public string SLA_Meet { get; set; }

    }
}
