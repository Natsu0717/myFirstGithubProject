using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Reflection;

namespace ExchangeTest
{


    public class ExHelper
    {
        string UserName = "auto.man@cn.mcd.com";//"Keyruscorp\\dylan.ding";
        string Password = "Password1234";//"DGK1024...";
        // string Domain = "";
        string ServerUrl = "https://mymail.mcd.com/EWS/Exchange.asmx";
        string Email = "auto.man@cn.mcd.com";//"dylan.ding@keyrus.com";
        int ReadNumber = 3;

        public static bool RedirectionCallback(string url)
        {
            // Return true if the URL is an HTTPS URL.
            return url.ToLower().StartsWith("https://");
        }
        public void ExChangeMail()
        {
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010);
            service.Credentials = new WebCredentials(UserName, Password);
            //给出Exchange Server的URL
            service.Url = new Uri(ServerUrl);
            //你自己的邮件地址 xxx@xxx.xxx 
            service.AutodiscoverUrl(Email, RedirectionCallback);
            //已读条件
            SearchFilter isRead = new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, true);
            //日期条件
            SearchFilter timeLimit = new SearchFilter.IsGreaterThan(EmailMessageSchema.DateTimeSent, Convert.ToDateTime("2019/9/26 00:00"));
            SearchFilter timeLimitMaxTime = new SearchFilter.IsLessThan(EmailMessageSchema.DateTimeSent, Convert.ToDateTime("2019/10/25 23:59"));

            //复合条件
            SearchFilter.SearchFilterCollection searchlist = new SearchFilter.SearchFilterCollection(LogicalOperator.And, isRead, timeLimit, timeLimitMaxTime);
            //查找Inbox,加入过滤器条件,结果10条 
            var IV = new ItemView(int.MaxValue);


            //MsgFolderRoot 所有邮件文件夹 ！ 从所有文件夹中 找到 test文件夹
            //FolderId folder = FindFolderIdByDisplayName(service, "test", WellKnownFolderName.MsgFolderRoot);
            //DeletedItems:已删除邮件   InBox：收件箱
            //FindItemsResults<Item> findResults = service.FindItems(folder, sf, IV);

            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.DeletedItems, searchlist, IV);
            PropertySet props = new PropertySet(BasePropertySet.IdOnly);

            props.Add(ItemSchema.Subject);
            props.Add(ItemSchema.Body);

            List<NewTempModel> list = new List<NewTempModel>();
            if (findResults != null && findResults.Items != null && findResults.Items.Count > 0)
            {
                var i = 1;

                foreach (Item item in findResults.Items)
                {

                    var total = findResults.Items.Count();

                    EmailMessage email = EmailMessage.Bind(service, item.Id);
                    props.RequestedBodyType = BodyType.Text;
                    EmailMessage emailNoHtml = EmailMessage.Bind(service, item.Id, props);
                    string emailText = emailNoHtml.Body.Text;

                    

                    //TempleModel tm = new TempleModel();
                    //tm.UserName = email.Sender == null ? "" : email.Sender.Name;
                    //tm.Description = emailText;
                    //tm.UserDate = email.DateTimeSent.ToShortDateString();
                    //tm.FixDate = tm.UserDate;
                    //tm.ComfiDate = tm.FixDate;
                    //tm.Month = $"{email.DateTimeSent.Year}/{email.DateTimeSent.Month}";
                    //tm = SwithCategory(tm);

                    NewTempModel tm = new NewTempModel();
                    tm.Requestor_Email = email.Sender == null ? "" : email.Sender.Address;
                    tm.Request_Date_Time = email.DateTimeCreated.ToString();
                    tm.Issue_Description = emailText;
                    tm.Closure_Date_Time = email.LastModifiedTime.ToString();
                    tm.Application = "F3";
                    tm.Severity_Level = "P1";
                    tm.Feedback_Source = "邮箱";
                    tm.Impacted_BU = "F3";
                    tm.Category = "资讯类";
                    tm.Sub_Category = "后台处理";
                    tm.Caused_By = "F3";
                    tm.Resolution_Detail = "后台处理数据";
                    tm.Description = "转交任务";
                    tm.Status = "Closed";
                    tm.Resolver = "alvin";
                    tm.SLA_Meet = "Yes";
                    //tm.Closure_Date_Time=email.to

                    if (tm != null)
                    {
                        list.Add(tm);
                        Console.WriteLine(emailText);
                        Console.WriteLine("-------------------------" + i + "/" + total + "down!--------------------------------");
                    }
                    //list.Add(tm);
                    i++;
                }

            }
            DataTable dt = ToDataTable(list);

            string path = @"C:\Users\Administrator\Desktop\wow1\1.xlsx";

            ExcelHelper.DataTableToExcel(path, dt, "Incident Pattern", false);
            Console.ReadKey();
        }

        private TempleModel SwithCategory(TempleModel tm)
        {
            var text = tm.Description;
            if (text == null)
            {
                return null;
            }
            if (text.Contains("假") || text.Contains("level"))
            {

                return null;
            }
            if (text.Contains("SA"))
            {
                tm.Subject = "StoreAudit";
                tm.TimeSpan = 1;
            }
            if (text.Contains("AS"))
            {
                tm.Subject = "报废";
                tm.TimeSpan = 1;

            }
            if (text.Contains("ST"))
            {
                tm.Subject = "调拨";
                tm.TimeSpan = 1;

            }
            if (text.Contains("转") || text.Contains("退"))
            {
                tm.Subcategory = "任务转交，退回或关闭";
                tm.TimeSpan = 1;
            }
            if (text.Contains("无法"))
            {
                tm.Subcategory = "刷新数据";
                tm.TimeSpan = new Random().Next(1, 5);
            }
            if (text.Contains("咨询"))
            {
                tm.Category = "咨询";
                tm.TimeSpan = new Random().Next(1, 3);
            }
            if (text.Contains("EPS"))
            {
                tm.Subcategory = "任务转交，退回或关闭";
            }
            if (text.Contains("移交"))
            {
                tm.Category = "权限";
            }
            return tm;
        }


        public DataTable ConverToTable(List<TempleModel> list)
        {
            return ToDataTable<TempleModel>(list);
        }

        public static DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }


        //https://www.cnblogs.com/tuenbo/p/9870396.html 
        public FolderId FindFolderIdByDisplayName(ExchangeService service, WellKnownFolderName SearchFolder)
        {
            // Specify the root folder to be searched.
            Folder rootFolder = Folder.Bind(service, SearchFolder);
            return rootFolder.Id;
        }
        public FolderId FindFolderIdByDisplayName(ExchangeService service, string DisplayName, WellKnownFolderName SearchFolder)
        {
            // Specify the root folder to be searched.
            Folder rootFolder = Folder.Bind(service, SearchFolder);

            // Loop through the child folders of the folder being searched.
            foreach (Folder folder in rootFolder.FindFolders(new FolderView(100)))
            {
                // If the display name of the current folder matches the specified display name, return the folder’s unique identifier.
                if (folder.DisplayName == DisplayName)
                {
                    return folder.Id;
                }
            }
            return null;
        }

    }
}
