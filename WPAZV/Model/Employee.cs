using WPAZV.Interfaces;

namespace WPAZV.Model
{
    [Serializable]
    public class Employee : IEmployee
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public char Gender { get; set; }

        public Employee(){
            LastName = "";
            FirstName = "";
            PhoneNumber = "+49";
        }
    }
}