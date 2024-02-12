using Newtonsoft.Json;
using WPBasic.Basissystem;
using WPBasic.Logging.Model;

namespace WPBasic.Helper.FileHelper
{
    public abstract class JsonFileHelper<T> : BaseFilehelper<T>
    {
        public override void Save(){
            try{
                string json = JsonConvert.SerializeObject(Storage);
                using StreamWriter writer = new (FileHandler);
                writer.Write(json);
            }
            catch(Exception ex) {
                WriteToLog(ex.Message);
            }
        }
        public override void Load(){
            try{
                if (File.Exists(FileHandler)){
                    using StreamReader reader = new(FileHandler);
                    string json = reader.ReadToEnd();
                    Storage = JsonConvert.DeserializeObject<List<T>>(json);
                }
                else { 
                    Storage = new List<T>();
                    WriteToLog($"File nicht gefunden : {FileHandler}", ErrorLevel.Info);
                }
            }catch(Exception ex) { 
            WriteToLog(ex.Message); 
            }
        }
    }
}
