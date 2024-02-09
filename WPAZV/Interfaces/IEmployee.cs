using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPBasic.Interface;

namespace WPAZV.Interfaces
{
    public interface IEmployee : IModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public char Gender { get; set; }    
    }
}