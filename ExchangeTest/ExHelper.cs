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
            SearchFilter timeLimit = new SearchFilter.IsGreaterThan(EmailMessageSchema.DateTimeSent, Convert.ToDateTime("2019/4/15 12:24"));
            //复合条件
            SearchFilter.SearchFilterCollection searchlist = new SearchFilter.SearchFilterCollection(LogicalOperator.And, isRead, timeLimit);
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

            List<TempleModel> list = new List<TempleModel>();
            if (findResults != null && findResults.Items != null && findResults.Items.Count > 0)
            {

                foreach (Item item in findResults.Items)
                {
                    EmailMessage email = EmailMessage.Bind(service, item.Id);
                    props.RequestedBodyType = BodyType.Text;
                    EmailMessage emailNoHtml = EmailMessage.Bind(service, item.Id, props);
                    string emailText = emailNoHtml.Body.Text;

                    TempleModel tm = new TempleModel();
                    tm.UserName = email.Sender.Name;
                    tm.Description = emailText;
                    tm.UserDate = email.DateTimeSent.ToShortDateString();
                    tm.FixDate = tm.UserDate;
                    tm.ComfiDate = tm.FixDate;
                    tm.Month = $"{email.DateTimeSent.Year}/{email.DateTimeSent.Month}";
                    list.Add(tm);
                    Console.WriteLine(emailText);
                    Console.WriteLine("-------------------------down!--------------------------------");

                }

            }
            DataTable dt = ToDataTable(list);

            string path = @"C:\Users\Administrator\Desktop\11.xlsx";

            ExcelHelper.DataTableToExcel(path, dt, "F3申报问题汇总", false);
            Console.ReadKey();
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
