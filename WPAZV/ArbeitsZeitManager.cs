using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace WPAZV
{
    public class ArbeitsZeitManager
    {
        private const string XmlFilePath = "arbeitszeiten.xml";

        private List<MArbeitsZeit> _arbeitszeiten;

        public ArbeitsZeitManager()
        {
            if (File.Exists(XmlFilePath))
            {
                // Load data from XML file
                XmlSerializer serializer = new XmlSerializer(typeof(List<MArbeitsZeit>));
                using (FileStream stream = new FileStream(XmlFilePath, FileMode.Open))
                {
                    _arbeitszeiten = (List<MArbeitsZeit>)serializer.Deserialize(stream);
                }
            }
            else
            {
                // Create a new list if the file does not exist
                _arbeitszeiten = new List<MArbeitsZeit>();
            }
        }

        public void AddArbeitsZeit(MArbeitsZeit arbeit)
        {
            // Add the new entry to the list
            _arbeitszeiten.Add(arbeit);

            // Save the changes to the XML file
            SaveToXmlFile();
        }

        public void RemoveArbeitsZeit(int id)
        {
            // Find the entry with the specified ID
            MArbeitsZeit entry = _arbeitszeiten.Find(arbeit => arbeit.ID == id);

            // Remove the entry from the list
            _arbeitszeiten.Remove(entry);

            // Save the changes to the XML file
            SaveToXmlFile();
        }

        public double GetTotalArbeitsZeit()
        {
            // Calculate the total working time
            double totalArbeitsZeit = _arbeitszeiten.Sum(arbeit => arbeit.ArbeitsZeit);

            return totalArbeitsZeit;
        }

        public double GetTotalArbeitsZeitForMonth(int month)
        {
            // Filter the list to only include entries for the specified month
            var filteredArbeitszeiten = _arbeitszeiten.Where(arbeit => arbeit.StartZeit.Month == month);

            // Calculate the total working time for the filtered entries
            double totalArbeitsZeit = filteredArbeitszeiten.Sum(arbeit => arbeit.ArbeitsZeit);

            return totalArbeitsZeit;
        }


        private void SaveToXmlFile()
        {
            // Serialize the list to XML
            XmlSerializer serializer = new XmlSerializer(typeof(List<MArbeitsZeit>));
            using (FileStream stream = new FileStream(XmlFilePath, FileMode.Create))
            {
                serializer.Serialize(stream, _arbeitszeiten);
            }
        }
    }
}
