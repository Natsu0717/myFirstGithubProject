using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace HttpWebRequestHelper
{
    class Program
    {
        private static string URL = "url";
        private static string API_KEY = "X70s5Z9RlB7ox7UH5X0CZGvv7IabNEMV";
        private static string SECRET_KEY = "secret";
        static void Main(string[] args)
        {
            GA_RecruitEntity entity = new GA_RecruitEntity();
            entity.NeedNumber = 2;
            entity.ID = Guid.NewGuid().ToString();
            entity.Job = "automan51";
            entity.JobRankID = "1201,1202";
            HttpRequest Request = null;
            HttpResponse Response = null;


            string url = "https://api-staging003.mokahr.com/v1/headcount?currentHireMode=1";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            string authValue = GetHeader();
            request.Headers.Add("Authorization", authValue);
            request.Timeout = 5000;

            HeaderAcountRequestEntity hcEntity = new HeaderAcountRequestEntity();

            //hcEntity.DepartmentCode = entity.Department;
            hcEntity.JobName = entity.Job;
            var temp = entity.JobRankID.Split(',').ToArray();
            List<int> rankIds = new List<int>();
            foreach (var item in temp)
            {
                rankIds.Add(int.Parse(item));
            }
            hcEntity.JobRankIds = rankIds.ToArray();
            hcEntity.NeedNumber = entity.NeedNumber;
            hcEntity.Number = entity.ID;
            var serializerSettings = new JsonSerializerSettings
            {
                // 设置为驼峰命名
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(hcEntity, Formatting.None, serializerSettings);

            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = "";
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                content = sr.ReadToEnd();
            }


            GA_HCResponseEntity resEntity = JsonConvert.DeserializeObject<GA_HCResponseEntity>(content);
            Console.WriteLine(resEntity.headcount.id);
            Console.ReadKey();

        }

        private static String GetHeader()
        {
            String auth = API_KEY + ":" + "";
            string encodedAuth = Convert.ToBase64String(Encoding.Default.GetBytes(auth));
            String authHeader = "Basic " + encodedAuth;
            return authHeader;
        }
    }
}
