using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WPBasic.Logging;
using WPBasic.Logging.Model;

namespace WPBasic.Helper.Security
{
    public class AES<T>
    {
        public T Data { get; set; }
        public string EncryptedMessage { get; set; }

        private byte[] _key;

        public AES(T data){
            _key = GenerateAesKey();
            Settings.SetSetting("Key", ConvertKeyToString(_key));
            _key = Convert.FromBase64String(Settings.GetSetting("Key"));
            Log.AddLog(ConvertKeyToString(_key), ErrorLevel.Info);
            Data = data;
            EncryptMessage();
        }

        private void EncryptMessage(){
            try{
                string jsonData = JsonConvert.SerializeObject(Data);
                using (Aes aes = Aes.Create()){
                    aes.IV = new byte[aes.BlockSize / 8];
                    aes.Key = _key;
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    byte[] dataBytes = Encoding.UTF8.GetBytes(jsonData);

                    byte[] encryptedData;
                    using (var ms = new System.IO.MemoryStream()){
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)){
                            cs.Write(dataBytes, 0, dataBytes.Length);
                        }
                        encryptedData = ms.ToArray();
                    }
                    EncryptedMessage = Convert.ToBase64String(encryptedData);
                }
            }
            catch (Exception ex){
                Log.AddLog("Error encrypting message: " + ex.Message, ErrorLevel.Error);
            }
        }

        public T DecryptMessage(){
            try{
                byte[] encryptedData = Convert.FromBase64String(EncryptedMessage);
                using (Aes aes = Aes.Create()){
                    aes.IV = new byte[aes.BlockSize / 8];
                    aes.Key = _key; // Entschlüsselungsschlüssel
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    string decryptedData;
                    using (var ms = new System.IO.MemoryStream(encryptedData)){
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read)){
                            using (var sr = new System.IO.StreamReader(cs)){
                                decryptedData = sr.ReadToEnd();
                            }
                        }
                    }
                    return JsonConvert.DeserializeObject<T>(decryptedData);
                }
            }catch (Exception ex){
                Log.AddLog("Error decrypting message: " + ex.Message, ErrorLevel.Error);
                return default(T);
            }
        }

        public byte[] GenerateAesKey(){
            using (var aes = Aes.Create()){
                aes.GenerateKey();
                return aes.Key;
            }
        }
        public string ConvertKeyToString(byte[] key){
            return Convert.ToBase64String(key);
        }
    }
}