using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeTest
{
    public class TempleModel
    {
        public TempleModel()
        {
            Subject = "StoreAudit";
            Status = "Closed";
            Category = "维护";
            Subcategory = "任务转交，退回或关闭";
            Admin = "alvin";
            TimeSpan = 1;
        }
        public string EmptyFirstColumn { get; set; }
        public string Subject { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string Category { get; set; }

        public string Subcategory { get; set; }
        public string UserName { get; set; }
        public string UserDate { get; set; }
        public string Admin { get; set; }
        public string FixDate { get; set; }
        public string ComfiDate { get; set; }
        public string Remark { get; set; }

        public string Fundation { get; set; }

        public int TimeSpan { get; set; }

        public string CaseOrigin { get; set; }

        public string IsSSC { get; set; }

        public string Month { get; set; }


    }
}
