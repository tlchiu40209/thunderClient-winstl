using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Policy;

namespace thunderClient
{
    internal class thunderDetector
    {
        public string ThunderUrl { get; set; }
        public double AlarmRangeKm { get; set; }
        public double ThisLon { get; set; }
        public double ThisLat { get; set; }
        public double AlarmTimeRangeSec { get; set; }

        private readonly HttpClient http = new HttpClient();

        public thunderDetector(string thunderUrl, double alarmRangeKm, double thisLon, double thisLat, double alarmTimeRangeSec)
        {
            ThunderUrl = thunderUrl;
            AlarmRangeKm = alarmRangeKm;
            ThisLon = thisLon;
            ThisLat = thisLat;
            AlarmTimeRangeSec = alarmTimeRangeSec;
        }

        public async Task<bool> CheckThunderAsync()
        {
            try
            {
                string kml = "";
                byte[] data = await http.GetByteArrayAsync(ThunderUrl);
                bool isKmz = data.Length > 4 &&
                    data[0] == 0x50 &&
                    data[1] == 0x4B &&
                    data[2] == 0x03 &&
                    data[3] == 0x04;
                if (!isKmz)
                {
                    kml = System.Text.Encoding.UTF8.GetString(data);
                }
                else
                {
                    using (var ms = new MemoryStream(data))
                    using (var zip = new System.IO.Compression.ZipArchive(ms))
                    {
                        var entry = zip.GetEntry("doc.kml");
                        if (entry == null)
                        {
                            return false;
                        }
                        using (var reader = new StreamReader(entry.Open()))
                        {
                            kml = await reader.ReadToEndAsync();
                        }
                    }
                }

                var strikes = ParseKml(kml);

                foreach (var strike in strikes)
                {
                    double distance = CalcDistance(ThisLon, ThisLat, strike.lon, strike.lat);
                    if (distance <= AlarmRangeKm)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private List<(double lon, double lat)> ParseKml(string kml)
        {
            var list = new List<(double lon, double lat)>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(kml);

            var placemakrs = doc.GetElementsByTagName("Placemark");
            foreach (XmlNode pm in placemakrs)
            {
                string coord = pm["Point"]?["coordinates"]?.InnerText?.Trim();
                string when = pm["TimeStamp"]?["when"]?.InnerText?.Trim();
                if (coord == null || when == null)
                {
                    continue;
                }
                var parts = coord.Split(','); 
                double lon = double.Parse(parts[0]);
                double lat = double.Parse(parts[1]);

                DateTime strikeTime = DateTime.Parse(when).AddHours(8);
                double deltaSec = (DateTime.Now - strikeTime).TotalSeconds;

                if (deltaSec < AlarmTimeRangeSec)
                {
                    list.Add((lon, lat));
                }
            }
            return list;
        }

        private double CalcDistance(double lon1, double lat1, double lon2, double lat2)
        {
            double R = 6371; // Radius of the earth in km
            double dLat = (lat2 - lat1) * (Math.PI / 180);
            double dLon = (lon2 - lon1) * (Math.PI / 180);
            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(lat1 * (Math.PI / 180)) * Math.Cos(lat2 * (Math.PI / 180)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c; // Distance in km
            return distance;
        }
    }
}
