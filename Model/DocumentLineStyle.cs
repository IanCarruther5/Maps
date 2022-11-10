using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RouteModel
{
    public partial class DocumentLineStyle
    {
        public DocumentLineStyle()
        {
        }
        public DocumentLineStyle(int item)
        {
            LineStyleId = item;
        }

        [Key]
        public int Id { get; set; }
        public int LineStyleId { get; set; }
        public int DocumentId { get; set; }
        public virtual LineStyle LineStyle { get; set; }
        public virtual Document Document { get; set; }
    }

}
