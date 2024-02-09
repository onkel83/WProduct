using System.Xml.Serialization;
using WPAZV.Interfaces;
using WPAZV.ViewModel;
using WPBasic.Basissystem;
using WPBasic.Logging;

namespace WPAZV.Repository
{
    public class WorktimeRepository : BasisRepository<WorktimeViewModel>
    {

        public WorktimeRepository(string xmlFilePath)
        {
            XmlFilePath = xmlFilePath;
        }
    }

}