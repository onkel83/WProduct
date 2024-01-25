// See https://aka.ms/new-console-template for more information
using WPBasic;
Settings.SetSetting("ErrorFile", AppDomain.CurrentDomain.BaseDirectory + "Error.xml");
Settings.SaveToFile(AppDomain.CurrentDomain.BaseDirectory + "settings.xml");
Console.WriteLine($"Key : ErrorFile ; Value : {Settings.GetSetting("ErrorFile")}");