using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WPBasic.Interface;

namespace WPBasic.Basissystem{
    public class XmlHelper<T> : IHelper<T>{
        public string Filepath{
            get;
            set;
        }

        public XmlHelper(string filepath = ""){
            if(filepath != ""){
                Filepath = filepath;
            }else{
                Filepath = AppDomain.CurrentDomain.BaseDirectory + "test.xml";
            }
        }

        public void Save(List<T> list){
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (TextWriter writer = new StreamWriter(Filepath)){
                serializer.Serialize(writer, list);
            }
        }
        public List<T> Load(){
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (TextReader reader = new StreamReader(Filepath)){
                return (List<T>)serializer.Deserialize(reader);
            }
        }
    }
}