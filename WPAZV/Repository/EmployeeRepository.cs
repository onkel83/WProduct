using System.Xml.Linq;
using System.Xml.Serialization;
using WPAZV.Interfaces;
using WPAZV.Model;
using WPAZV.ViewModel;
using WPBasic.Basissystem;
using WPBasic.Interface;

namespace WPAZV.Repository
{
    public class EmployeeRepository : BasisRepository<EmployeeViewModel>
    {
        public EmployeeRepository(string xmlFilePath)
        {
            base.XmlFilePath = xmlFilePath;
        }

    }
}