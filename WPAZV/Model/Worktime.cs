namespace WPAZV.Model{
    [Serializable]
    public class Worktime{

        public int ID { get; set; }
        public int UserID { get; set; }
        public string Einsatzort { get; set; }
        public DateTime Startzeit { get; set; }
        public DateTime Endzeit { get; set; }
        public decimal Pause { get; set; }
        public decimal Arbeitszeit { get => Convert.ToDecimal((Endzeit-Startzeit).TotalHours) - Pause;}

        public Worktime(){
        }
    }
}