using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WPAZV.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T value);
        void Edit(T value);
        void Delete(int value);
        List<T>? Get(string id = "0");
    }
}