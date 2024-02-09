using WPBasic.Interface;

namespace WPAZV.Interfaces;

public interface IWorktime : IModel
{
        public int UserID { get; set; }
        public string Einsatzort { get; set; }
        public DateTime Startzeit { get; set; }
        public DateTime Endzeit { get; set; }
        public decimal Pause { get; set; }
        public decimal Arbeitszeit { get => Convert.ToDecimal((Endzeit-Startzeit).TotalHours) - Pause;}
}
