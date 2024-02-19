namespace WPBasic;

public static class DateTimeConverter
{
    public static DateTime ConvertStringToDateTime(string dateString)
    {
        // Definiere das erwartete Eingabeformat
        string format = "HH:mm dd.MM.yyyy";
        
        // Versuche, den Eingabestring in ein DateTime-Objekt zu konvertieren
        if (DateTime.TryParseExact(dateString, format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
        {
            return result;
        }
        else
        {
            // Wenn die Konvertierung fehlschlägt, wirf eine Ausnahme oder handle den Fehler entsprechend
            throw new ArgumentException("Ungültiges Datumsformat. Verwende das Format HH:mm dd.MM.YYYY");
        }
    }

    public static string ConvertDateTimeToString(DateTime dateTime)
    {
        // Definiere das gewünschte Ausgabeformat
        string format = "HH:mm dd.MM.YYYY";

        // Konvertiere das DateTime-Objekt in einen formatierten String
        string result = dateTime.ToString(format);

        return result;
    }
}
