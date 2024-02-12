using System.Xml.Serialization;
using WPBasic.Basissystem;
using WPBasic.Logging.Model;

namespace WPBasic.Helper.FileHelper
{
    public abstract class XmlFileHelper<T> : BaseFilehelper<T>
    {

        public override void Save(){

            XmlSerializer serializer = new (typeof(List<T>));
            try{
                using StreamWriter writer = new(FileHandler);
                serializer.Serialize(writer, Storage);
            }
            catch(Exception ex){
                WriteToLog(ex.Message);
            }
        }

        public override void Load()
        {
            XmlSerializer serializer = new(typeof(List<T>));
            try{
                if(File.Exists(FileHandler)){
                    using StreamReader reader = new(FileHandler);
                    Storage = (List<T>)serializer.Deserialize(reader);
                }
                else{
                    WriteToLog($"File nicht gefunden : {FileHandler}", ErrorLevel.Info);
                    Storage = new();
                }
            }catch(Exception ex){
                WriteToLog(ex.Message);
                Storage = new ();
            }
        }
    }
}