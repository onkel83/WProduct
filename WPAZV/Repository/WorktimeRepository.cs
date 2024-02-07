using System.Xml.Serialization;
using WPAZV.Interfaces;
using WPAZV.ViewModel;
using WPBasic.Logging;

namespace WPAZV.Repository
{
    public class WorktimeRepository : BRepository<WorktimeViewModel>
    {

        public WorktimeRepository(string xmlFilePath)
        {
            XmlFilePath = xmlFilePath;
        }
    }

}