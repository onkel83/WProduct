using System.Xml.Linq;
using System.Xml.Serialization;
using WPAZV.Interfaces;
using WPAZV.Model;
using WPAZV.ViewModel;

namespace WPAZV.Repository
{
    public class EmployeeRepository : BRepository<EmployeeViewModel>
    {
        public EmployeeRepository(string xmlFilePath)
        {
            base.XmlFilePath = xmlFilePath;
        }

    }
}