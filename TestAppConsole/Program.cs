// See https://aka.ms/new-console-template for more information
using WPAZV;
using WPBasic;

Settings.LoadFromFile("settings.xml");
Settings.SetSetting("ArbeitsZeit", "AVZ.xml");
Settings.SaveToFile("settings.xml");
ArbeitsZeitManager azv = new ArbeitsZeitManager();
azv.XmlFilePath = Settings.GetSetting("ArbeitsZeit");
 /*
azv.AddArbeitsZeit(new MArbeitsZeit{
    ID = azv._arbeitszeiten.Count + 1,
    EinsatzOrt = Console.In.ReadLine(),
    StartZeit = azv.StringToDateTime(Console.In.ReadLine()),
    EndZeit = azv.StringToDateTime(Console.In.ReadLine()),
    Pause = Convert.ToDouble(Console.In.ReadLine())
}); 
 */
foreach(MArbeitsZeit _avz in azv._arbeitszeiten){
    Console.WriteLine($"Einsatz Ort : {_avz.EinsatzOrt}");
    Console.WriteLine($"Beginn      : {azv.FormatDateTime(_avz.StartZeit)}");
    Console.WriteLine($"Ende        : {azv.FormatDateTime(_avz.EndZeit)}");
    Console.WriteLine($"Pausen in h : {_avz.Pause}");
    Console.WriteLine($"Gesamt Arbeitszeit : {_avz.ArbeitsZeit.ToString()}");
    Console.WriteLine("###################################################################");
}

Console.WriteLine($"Gesamt Stunden : {azv.GetTotalArbeitsZeit()}");
Console.WriteLine($"Gesamt Stunden im Januar : {azv.GetTotalArbeitsZeitForMonth(1)}");
