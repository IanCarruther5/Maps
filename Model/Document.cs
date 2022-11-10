using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace RouteModel
{
    public partial class Document
    {
        public Document()
        {
            DocumentPlacemarks = new List<Placemark>();
            DocumentLineStyles = new List<LineStyle>();
            DocumentStyleMaps = new List<StyleMap>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<LineStyle> DocumentLineStyles { get; set; }
        public IList<StyleMap> DocumentStyleMaps { get; set; }
        public IList<Placemark> DocumentPlacemarks { get; set; }



        [NotMapped]
        [JsonIgnore]
        public int[] SelectedLineStyles { get; set; }= System.Array.Empty<int>();
        [NotMapped]
        [JsonIgnore]
        public int[] SelectedStyleMaps { get; set; } = System.Array.Empty<int>();
        [NotMapped]
        [JsonIgnore]
        public int[] SelectedPlacemarks { get; set; } = System.Array.Empty<int>();
        [NotMapped]
        [JsonIgnore]
        public string xml=> this.ToString();
        [NotMapped]
        [JsonIgnore]
        public string Output { get
            {
                ToString();
                string rtn = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?><kml xmlns=\"http://www.opengis.net/kml/2.2\"> <Document><name>{Name}</name><description>{Description}</description>";


                foreach (var item in DocumentLineStyles)
                {
                    rtn += item.ToString();
                }
                foreach (var item in DocumentStyleMaps)
                {
                    rtn += item.ToString();
                }
                foreach (var item in DocumentPlacemarks.OrderBy(x => x.PlacemarkName))
                {
                    rtn += item.ToString();
                }
                rtn += "</ Document >";

                return rtn;
            } }
        public override string ToString()
        {
            string rtn = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?><kml xmlns=\"http://www.opengis.net/kml/2.2\"> <Document><name>{Name}</name><description>{Description}</description>";


            foreach (var item in DocumentLineStyles)
            {
                rtn += item.ToString();
            }
            foreach (var item in DocumentStyleMaps)
            {
                rtn += item.ToString();
            }
            rtn += $"<Folder><name>{Name}</name>";
            foreach (var item in DocumentPlacemarks.OrderBy(x=>x.PlacemarkName))
            {
                rtn += item.ToString();
            }
            rtn += "</Folder></Document></kml>";

            return rtn;
        }
    }
}
