/*
 * ##USAGE##
 * // Get setting value
 * string settingValue = Settings.GetSetting("MySetting");
 * Console.WriteLine($"Setting value: {settingValue}");
 * // Set setting value
 * Settings.SetSetting("MySetting", "New value");
 * // Save settings to file
 * Settings.SaveToFile("settings.xml");
 * // Load settings from file
 * Settings.LoadFromFile("settings.xml");
 */
#pragma warning disable CS8618, CS8603, CS8602
using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WPBasic{
    public static class Settings{
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
            _xmlDocument = XDocument.Load(FileName);
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

        var settingsElement = _xmlDocument.Root.Element("Settings");
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

    public static void SetSetting(string name, string value)
    {
        if (_xmlDocument == null)
        {
            CreateDefaultSettings();
        }

        var settingsElement = _xmlDocument.Root.Element("Settings");
        var settingElements = settingsElement.Elements("Setting");

        foreach (var settingElement in settingElements)
        {
            if (settingElement.Attribute("Name").Value == name)
            {
                settingElement.Attribute("Value").Value = value;
                return;
            }
        }

        // Add new setting element
        var newSettingElement = new XElement("Setting", new XAttribute("Name", name), new XAttribute("Value", value));
        settingsElement.Add(newSettingElement);

        _xmlDocument.Save(FileName);
    }

    public static void SaveToFile(string filePath)
    {
        _xmlDocument.Save(filePath);
    }

    public static void LoadFromFile(string filePath)
    {
        _xmlDocument = XDocument.Load(filePath);
    }
}
}
#pragma warning restore CS8618, CS8603, CS8602