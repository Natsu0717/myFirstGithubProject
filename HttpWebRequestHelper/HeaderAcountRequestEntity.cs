using System;

namespace HttpWebRequestHelper
{
    internal class HeaderAcountRequestEntity
    {
        public string Number { get; set; }
        public string JobName { get; set; }
        public int NeedNumber { get; set; }
        public DateTime StartDate { get; set; }
        public string DepartmentCode { get; set; }
        //public string LocationId { get; set; }
        public int[] JobRankIds { get; set; }
    }
}