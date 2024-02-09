using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WPBasic.Interface
{
    public interface IModel
    {
        int ID { get; set; }
        string? ToString();
    }
}