using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SOAP
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string InputtedIP(string ip)
        {
            WebClient client = new WebClient();
            string json = client.DownloadString("http://ipwho.is/" + ip);
            var geoIPInfo = JsonConvert.DeserializeObject<GeoIPInfo>(json);
            return geoIPInfo.Country_code;
        }


        public string MyCurrentIP()
        {
            WebClient client = new WebClient();
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string IPAddress = string.Empty;
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.Cluster)
                {
                    IPAddress = ip.ToString();
                    break;
                }
            }

            string json = client.DownloadString("http://ipwho.is/" + IPAddress);
            var geoIPInfo = JsonConvert.DeserializeObject<GeoIPInfo>(json);
            return geoIPInfo.Country_code;
        }
    }
}
