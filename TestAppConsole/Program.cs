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
 MainMenue();

static void MainMenue(){
    string _menue = "";
    while(_menue != "exit"){
        LogikMainMenue(_menue);
        _menue = Console.In.ReadLine();
    }
}

static void LogikMainMenue(string value){
    switch(value.ToLower()){
        case "new" : GetNewEntry();break;
        case "list -a": ArbeitsZeitManager avz = new ArbeitsZeitManager(); ShowAllArbeitzeiten(avz);break;
        default : ShowMainMenue();break; 
    }
}

static void ShowMainMenue(){
    Console.WriteLine($"Willkommen bei der Stundenverwaltung !");
    Console.WriteLine($"Da Die Anwendung Konsolen gesteuert ist benötigst du folgende befehle :");
    Console.WriteLine($"[new] Fügt einen neuen Stunden Eintrag hinzu");
    Console.WriteLine($"[list -a] Wird alle vorhanden Einträge ausgeben");
    Console.WriteLine($"Platzhalter");
    Console.WriteLine($"Platzhalter");
    Console.WriteLine($"Platzhalter");
    Console.WriteLine($"[exit] beendet die Anwendung");
}
static void GetNewEntry(){
    ArbeitsZeitManager azv = new ArbeitsZeitManager();
    azv.XmlFilePath = Settings.GetSetting("ArbeitsZeit");
    string _ort;
    string _start;
    string _ende;
    string _pause;
    Console.WriteLine("Neu Eingabe von Arbeitszeit :");
    Console.WriteLine("Bitte geben sie den Einsatz Ort ein : ");
    _ort = Console.In.ReadLine();
    Console.WriteLine("Bitte geben sie die Start Zeit/Datum ein (Format: hh:mm dd.MM.yyyy) :");
    _start = Console.In.ReadLine();
    Console.WriteLine("Bitte geben sie die End Zeit/Datum ein (Format: hh:mm dd.MM.yyyy) :");
    _ende = Console.In.ReadLine();
    Console.WriteLine("Bitte geben sie die Pausen in decimal/Stunden ein:");
    _pause = Console.In.ReadLine();
    azv.AddArbeitsZeit(new MArbeitsZeit{
        ID = azv._arbeitszeiten.Count + 1,
        EinsatzOrt = _ort,
        StartZeit = azv.StringToDateTime(_start),
        EndZeit = azv.StringToDateTime(_ende),
        Pause = Convert.ToDouble(_pause)
    });
}

static void ShowAllArbeitzeiten(ArbeitsZeitManager azv){
    foreach(MArbeitsZeit _avz in azv._arbeitszeiten){
        Console.WriteLine($"Einsatz Ort : {_avz.EinsatzOrt}");
        Console.WriteLine($"Beginn      : {azv.FormatDateTime(_avz.StartZeit)}");
        Console.WriteLine($"Ende        : {azv.FormatDateTime(_avz.EndZeit)}");
        Console.WriteLine($"Pausen in h : {_avz.Pause}");
        Console.WriteLine($"Gesamt Arbeitszeit : {_avz.ArbeitsZeit.ToString()}");
        Console.WriteLine("###################################################################");
    }
}
