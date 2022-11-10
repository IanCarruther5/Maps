using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteModel
{
    public partial class StyleMap
    {
        public StyleMap()
        {
            LineStyles = new List<LineStyle>();
        }
        [Key]
        public int Id { get; set; }
        public string StyleMapName => $"line-{Color}-1000";
        public string Color { get; set; }
        public string Name { get;set;}
        public IList<LineStyle> LineStyles { get; set; }

        public override string ToString()
        {
            string rtn =  $"<StyleMap id=\"{StyleMapName}\">";
            foreach (var item in Enum.GetValues(typeof(Style)))
            {
                rtn += $"<Pair><key>{item}</key><styleUrl>#{StyleMapName}-{item}</styleUrl></Pair> ";
            }
            rtn += "</StyleMap>";

            return rtn;
        }
    }
}