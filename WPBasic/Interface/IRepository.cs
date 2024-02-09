using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WPBasic.Interface
{
    public interface IRepository<T>
    {
        string XmlFilePath{get;set;}
        void Add(T value);
        void Edit(T value);
        void Delete(string value);
        List<T>? Get();
    }
}