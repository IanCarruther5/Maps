using System;
using System.Collections.Generic;

namespace Model
{
    public class Import
    {
        public Import()
        {
        }
        public Document Document { get; set; }
    }

    public class Document
    {
        public Folder Folder { get; set; }
    }

    public class Folder
    {
        public List<Placemark> Placemark { get; set; }
    }

    public class Placemark
    {
        public string name { get; set; }
        public string description { get; set; }
        public string styleUrl { get; set; }
        public LineString LineString { get; set; }
    }

    public class LineString
    {
        public string tessellate { get; set;
        }
        public string coordinates { get; set; }
    }
}

