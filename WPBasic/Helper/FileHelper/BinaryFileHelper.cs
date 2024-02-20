using WPBasic.Basissystem;
using WPBasic.Logging.Model;

namespace WPBasic.Helper.FileHelper
{
    public class BinaryFileHelper<T> : BaseFilehelper<T>
    {
        public override void Save()
        {
            try {
                using FileStream fileStream = new(FileHandler, FileMode.Create);
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
#pragma warning disable SYSLIB0011
                binaryFormatter.Serialize(fileStream, Storage);
#pragma warning restore SYSLIB0011
            }
            catch(Exception ex)
            {
                WriteToLog(ex.Message);
            }
        }

        public override void Load()
        {
            try
            {
                if (File.Exists(FileHandler))
                {
                    using FileStream fileStream = new(FileHandler, FileMode.Open);
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
#pragma warning disable SYSLIB0011
                    Storage = (List<T>)binaryFormatter.Deserialize(fileStream);
#pragma warning restore SYSLIB0011
                }
                else
                {
                    Storage = new List<T>();
                    WriteToLog($"File nicht gefunden : {FileHandler}", ErrorLevel.Info);
                }
            }catch(Exception ex)
            {
                WriteToLog(ex.Message);
            }
        }
    }
}
