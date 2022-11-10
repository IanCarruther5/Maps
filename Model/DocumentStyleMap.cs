using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RouteModel
{
    public partial class DocumentStyleMap
    {
        public DocumentStyleMap( )
        {
        }
        public DocumentStyleMap(int item)
        {
            StyleMapId = item;
        }

        [Key]
        public int Id { get; set; }
        public int StyleMapId { get; set; }
        public int DocumentId { get; set; }
        public virtual StyleMap StyleMap { get; set; }
        public virtual Document Document { get; set; }
    }
}
