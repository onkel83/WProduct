namespace WPAZV.Interfaces
{
    public interface IEmployee
    {
        int ID { get; set; }
        string LastName { get; set; }
        string FirstName { get; set; }
        DateTime Birthday { get; set; }
        string PhoneNumber { get; set; }
        char Gender { get; set; }
    }
}