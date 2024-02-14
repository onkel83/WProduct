using WPBasic.Basissystem;
using WPWorker.Interfaces;

namespace WPWorker
{
    [Serializable]
    public class MWorker :BasisModel, IMWorker
    {
        #region Private Member
        private string _NName;
        private string _VName;
        private string _Phone;
        private string _Gender;
        #endregion
        #region Public Member
        public string NName{
            get => _NName;
            set => _NName = value;
        }
        public string VName{
            get => _VName;
            set => _VName = value;
        }
        public string Phone{
            get => _Phone;
            set => _Phone = value;
        }
        public string Gender{
            get => _Gender;
            set => _Gender = value;
        }
        #endregion
        #region Constructor
        public MWorker(){
            _NName = string.Empty;
            _VName = string.Empty;
            _Phone = string.Empty;
            _Gender = string.Empty;
        }
        #endregion
    }
}