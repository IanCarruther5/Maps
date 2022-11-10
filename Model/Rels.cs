using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
namespace RouteModel
{
    public class Routes
    {
        public Routes()
        { Open(); }
        public Routes(string location)
        {
            path = location;
            Open();
        }
        private string path =@"test/", ext=".json";

        public IList<Document> Documents { get; set; }

        public IList<LineStyle> Styles { get; set; }
        public IList<Placemark> Placemarks { get; set; }
        public IList<StyleMap> StyleMaps { get; set; }

        public void SaveChanges()
        {
            writeFile("Documents", Documents);
            writeFile("Styles", Styles);
            writeFile("Placemarks", Placemarks);
            writeFile("StyleMaps", StyleMaps);
            SaveKML();
        }
        public void Open()
        {
            if (fileExists( "Documents"))
            {
                string json = readFile("Documents");
                Documents = JsonSerializer.Deserialize<IList<Document>>(json);
            }
            else
            {
                Documents = new List<Document>();
            }
            if(fileExists( "Styles"))
            {
                string json = readFile("Styles");
                Styles = JsonSerializer.Deserialize<IList<LineStyle>>(json);
            }
            else
            {
                Styles = new List<LineStyle>();
            }
            if (fileExists("StyleMaps"))
            {
                string json = readFile("StyleMaps");
                StyleMaps = JsonSerializer.Deserialize<IList<StyleMap>>(json);
            }
            else
            {
                StyleMaps = new List<StyleMap>();
            }
            if (fileExists( "Placemarks"))
            {
                string json = readFile("Placemarks");
                Placemarks = JsonSerializer.Deserialize<IList<Placemark>>(json);
            }
            else
            {
                Placemarks = new List<Placemark>();
            }
        }



        private bool fileExists( string filename)
        {
            return File.Exists($"{path}/{filename}{ext}");
        }
        private bool writeFile(string filename, object obj)
        {
            if (obj != null)
            {
                File.WriteAllText($"{path}/{filename}{ext}", JsonSerializer.Serialize(obj));
                return true;
            }
            return false;
        }
        private string readFile(string filename)
        {
            return File.ReadAllText($"{path}/{filename}{ext}"); 
        }
        private void SaveKML()
        {
            foreach (var item in Documents)
            {
                File.WriteAllText($"{path}/{item.Name}.kml", item.ToString());
            }
        }
        public void Update(Document document)
        {
            var x = Documents.FirstOrDefault(x => x.Id == document.Id);
            Documents.Remove(x);
            Documents.Add(document);
        }

        public void Update(StyleMap styleMap)
        {
            var x = StyleMaps.FirstOrDefault(x => x.Id == styleMap.Id);
            StyleMaps.Remove(x);
            StyleMaps.Add(styleMap);
        }

        public void Update(LineStyle lineStyle)
        {
            var x = Styles.FirstOrDefault(x => x.Id == lineStyle.Id);
            Styles.Remove(x);
            Styles.Add(lineStyle);
        }

        public void Update(Placemark placemark)
        {
            var x = Placemarks.FirstOrDefault(x => x.Id == placemark.Id);
           Placemarks.Remove( x);
            Placemarks.Add(    placemark);
        }
        
    }
}
