using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteModel
{
    public partial class LineStyle
    {
        public LineStyle()
        {
        }
        [Key]
        public int Id { get; set; }
        public string LineStyleName => $"line-{StyleMap.Color}-1000-{Style}";

        public int StyleMapId { get; set; }
        public string Width { get; set; }
        public Style Style { get; set; }
        public StyleMap StyleMap { get; set; }

        public override string ToString()
        {
            string rtn = $"<Style id=\"{LineStyleName}\"><LineStyle><color> {StyleMap.Color} </color><width>{Width}</width></LineStyle></Style>";

            return rtn;
        }
    }
}