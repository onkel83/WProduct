#pragma warning disable CS8618, CS8603, CS8602
using System.Xml.Linq;
using WPBasic.Logging;

namespace WPBasic
{
    public static class Settings
    {
        private const string FileName = "settings.xml";

        private static XDocument _xmlDocument;

        static Settings()
        {
            if (!File.Exists(FileName))
            {
                CreateDefaultSettings();
            }
            else
            {
                try
                {
                    _xmlDocument = XDocument.Load(FileName);
                }
                catch (Exception e)
                {
                    string errorMsg = $"Error loading settings file: {e.Message}";
                    Log.AddLog(errorMsg, ErrorLevel.Error);
                    Console.WriteLine(errorMsg);
                    CreateDefaultSettings();
                }
            }
        }

        private static void CreateDefaultSettings()
        {
            _xmlDocument = new XDocument(new XElement("Settings"));
            _xmlDocument.Save(FileName);
        }

        public static string GetSetting(string name)
        {
            if (_xmlDocument == null)
            {
                return null;
            }

            var settingsElement = _xmlDocument.Root;
            if (settingsElement == null)
            {
                return null;
            }

            var settingElements = settingsElement.Elements("Setting");

            foreach (var settingElement in settingElements)
            {
                if (settingElement.Attribute("Name").Value == name)
                {
                    return settingElement.Attribute("Value").Value;
                }
            }

            return null;
        }

        public static XElement SetSetting(string name, string value)
        {
            if (_xmlDocument == null)
            {
                CreateDefaultSettings();
            }

            var settingsElement = _xmlDocument.Root;
            if (settingsElement == null)
            {
                _xmlDocument.Add(new XElement("Settings"));
                settingsElement = _xmlDocument.Root;
            }

            var settingElements = settingsElement.Elements("Setting");
            foreach (var settingElement in settingElements)
            {
                if (settingElement.Attribute("Name").Value == name)
                {
                    settingElement.Attribute("Value").Value = value;
                    return settingElement;
                }
            }
            // Add new setting element
            var newSettingElement = new XElement("Setting", new XAttribute("Name", name), new XAttribute("Value", value));
            settingsElement.Add(newSettingElement);

            try
            {
                _xmlDocument.Save(FileName);
            }
            catch (Exception e)
            {
                string errorMsg = $"Error saving settings file: {e.Message}";
                Log.AddLog(errorMsg, ErrorLevel.Error);
                Console.WriteLine(errorMsg);
                return null;
            }

            return newSettingElement;
        }

        public static void SaveToFile(string filePath)
        {
            try
            {
                _xmlDocument.Save(filePath);
            }
            catch (Exception e)
            {
                string errorMsg = $"Error saving settings file: {e.Message}";
                Log.AddLog(errorMsg, ErrorLevel.Error);
                Console.WriteLine(errorMsg);
            }
        }

        public static void LoadFromFile(string filePath)
        {
            try
            {
                _xmlDocument = XDocument.Load(filePath);
            }
            catch (Exception e)
            {
                string errorMsg = $"Error loading settings file: {e.Message}";
                Log.AddLog(errorMsg, ErrorLevel.Error);
                Console.WriteLine(errorMsg);
                CreateDefaultSettings();
            }
        }
    }
}

#pragma warning restore CS8618, CS8603, CS8602