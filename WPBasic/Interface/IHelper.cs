using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WPBasic.Interface
{
    public interface IHelper<T>
    {
        string Filepath{get;set;}

        List<T> Load();
        void Save(List<T> value);
    }
}