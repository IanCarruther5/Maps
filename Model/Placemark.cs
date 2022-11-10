using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteModel
{
    public partial class Placemark
    {
        public Placemark()
        {
        }
        [Key]
        public int Id { get; set; }
        public string PlacemarkName { get; set; }
        public string Description { get; set; }

        public int StyleMapId { get; set; }
        public string Tessellate { get; set; } = "1";
        public string Coordinates { get; set; }
        public virtual StyleMap StyleMap { get; set; }

        public override string ToString()
        {
            return $"<Placemark><name>{PlacemarkName}</name><description>{Description}</description><styleUrl>#{StyleMap?.StyleMapName}</styleUrl><LineString><tessellate>{Tessellate}</tessellate><coordinates>{Coordinates}</coordinates></LineString></Placemark>";
        }

        public static implicit operator Placemark(Model.Placemark v)
        {
            return new Placemark
            {
                PlacemarkName = v.name,
                Description = v.description,
                 Tessellate=v.LineString.tessellate,
                 Coordinates=v.LineString.coordinates
            };
        }
    }
}
