using System.Xml.Serialization;
using WPBasic.Basissystem;
using WPBasic.Logging.Model;

namespace WPBasic.Helper.FileHelper
{
    public abstract class XmlFileHelper<T> : BaseFilehelper<T>
    {

        public override void Save(){

            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            try{
                using (StreamWriter writer = new StreamWriter(FileHandler)){
                    serializer.Serialize(writer, Storage);
                }
            }catch(Exception ex){
                WriteToLog(ex.Message);
            }
        }

        public override void Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            try{
                if(File.Exists(FileHandler)){
                    using (StreamReader reader = new StreamReader(FileHandler)){
                        Storage = (List<T>)serializer.Deserialize(reader);
                    }
                }else{
                    WriteToLog($"File nicht gefunden : {FileHandler}");
                }
            }catch(Exception ex){
                WriteToLog(ex.Message);
                Storage = new List<T>();
            }
        }

        public override void WriteToLog(string msg, ErrorLevel lvl = ErrorLevel.Error){
            Logging.Log.AddLog(msg, lvl);

        }
    }
}