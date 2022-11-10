using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RouteModel
{
    public partial class DocumentPlacemark
    {
        public DocumentPlacemark(int item)
        {
            PlacemarkId = item;
        }
        public DocumentPlacemark()
        {
        }
        [Key]
        public int Id { get; set; }
        public int PlacemarkId { get; set; }
        public int DocumentId { get; set; }
        public virtual Placemark Placemark { get; set; }
        public virtual Document Document { get; set; }
    }
}
