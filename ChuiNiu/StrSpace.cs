using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ChuiNiu
{
    public class StrSpace
    {
        public string Fun()
        {
            string url = "https://www.vansfans.cn/wallpaper190112/";
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultCredentials;//获取或设置请求凭据
            Byte[] pageData = client.DownloadData(url); //下载数据
            string pageHtml = System.Text.Encoding.UTF8.GetString(pageData);

            Regex re = new Regex("<img\\s.*?\\s?src\\s*=\\s*['|\"]?([^\\s'\"]+).*?>");
            var listsp = re.Split(pageHtml);
            var listreg = re.Matches(pageHtml);
            foreach (var item in listreg)
            {
                var ssssss = item.ToString();
            }

            
            return pageHtml;
        }
    }
}
