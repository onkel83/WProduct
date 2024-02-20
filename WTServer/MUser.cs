using WPBasic.Basissystem;
namespace WTServer{
    [Serializable()]
    public class MUser : BasisModel{
        private string _UserName = string.Empty;
        private string _Key = string.Empty;

        public string UserName{
            get => (_UserName != string.Empty)?_UserName:"New User";
            set => _UserName = value;
        }
        public string Key{
            get {
                WPBasic.Helper.Security.AES<string> Aes = new WPBasic.Helper.Security.AES<string>(_Key);
                return Aes.DecryptMessage();
            }
            set {
                WPBasic.Helper.Security.AES<string> Aes = new WPBasic.Helper.Security.AES<string>(value);
                _Key = Aes.EncryptedMessage;
            }
        } 

        public MUser(string user = "", string key = ""){
            UserName = (user != "" || string.IsNullOrEmpty(user))?user:string.Empty;
            Key = (key != "" || string.IsNullOrEmpty(key))?key:string.Empty;
        }

        public override string ToString(){
            return $"ID: {ID}; User : {UserName}; Key : {Key}";
        }
    }
}