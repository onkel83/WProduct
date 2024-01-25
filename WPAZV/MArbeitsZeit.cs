using System;
using System.Xml.Serialization;


namespace WPAZV
{
    [XmlRoot("MArbeitsZeit")]
    public class MArbeitsZeit
    {
        [XmlAttribute("ID")]
        public int ID { get; set; }

        [XmlElement("EinsatzOrt")]
        public required string EinsatzOrt { get; set; }

        [XmlElement("StartZeit")]
        public DateTime StartZeit { get; set; }

        [XmlElement("EndZeit")]
        public DateTime EndZeit { get; set; }

        [XmlElement("Pause")]
        public double Pause { get; set; }

        [XmlIgnore]
        public double ArbeitsZeit{
            get => (EndZeit - StartZeit).TotalHours - Pause;
        }
    }
}