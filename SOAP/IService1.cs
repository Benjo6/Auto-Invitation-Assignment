using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.Json.Serialization;

namespace SOAP
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string MyCurrentIP();

        [OperationContract]
        string InputtedIP(string ip);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class GeoIPInfo
    {
        [DataMember]
        [JsonPropertyName("ip")]
        public string IP { get; set; }

        [DataMember]
        [JsonPropertyName("country_code")]
        public string Country_code { get; set; }
        [DataMember]
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [DataMember]
        [JsonPropertyName("region")]
        public string Region { get; set; }

        [DataMember]
        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        [DataMember]
        [JsonPropertyName("city")]
        public string City { get; set; }

        [DataMember]
        [JsonPropertyName("postal")]
        public string Postal { get; set; }

        [DataMember]
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [DataMember]
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }

}
