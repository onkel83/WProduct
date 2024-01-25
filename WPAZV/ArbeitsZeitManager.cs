using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;
using WPBasic;
using WPBasic.Logging;

namespace WPAZV
{
    public class ArbeitsZeitManager
    {
        public string XmlFilePath = "AVZ.xml";

        public List<MArbeitsZeit> _arbeitszeiten;

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
            var filteredArbeitszeiten = _arbeitszeiten.Where(arbeit => arbeit.StartZeit.Month == month && arbeit.StartZeit.Year == DateTime.Now.Year);

            // Calculate the total working time for the filtered entries
            double totalArbeitsZeit = filteredArbeitszeiten.Sum(arbeit => arbeit.ArbeitsZeit);

            return totalArbeitsZeit;
        }

        public double GetTotalArbeitsZeitForYear(int year){
            // Filter the list to only include entries for the specified month
            var filteredArbeitszeiten = _arbeitszeiten.Where(arbeit => arbeit.StartZeit.Year == year);

            // Calculate the total working time for the filtered entries
            double totalArbeitsZeit = filteredArbeitszeiten.Sum(arbeit => arbeit.ArbeitsZeit);

            return totalArbeitsZeit;
        }

        public double GetTotalArbeitsZeitForMonthAndYears(int month, int year){
            var filteredArbeitszeiten = _arbeitszeiten.Where(arbeit => arbeit.StartZeit.Month == month && arbeit.StartZeit.Year == year);
            double totalArbeitsZeit = filteredArbeitszeiten.Sum(arbeit => arbeit.ArbeitsZeit);

            return totalArbeitsZeit;
        }

        public DateTime StringToDateTime(string value){
            string input = value;
            DateTime datetime;

            if (DateTime.TryParseExact(input, "HH:mm dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out datetime)){
                return datetime;
            } else {
                string msg = "Invalid input string";
                Log.AddLog(msg, ErrorLevel.Error);
                Console.WriteLine(msg);
            }
            return DateTime.Now;
        }

        public string FormatDateTime(DateTime datetime){
            return datetime.ToString("HH:mm dd.MM.yyyy", CultureInfo.InvariantCulture);
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
