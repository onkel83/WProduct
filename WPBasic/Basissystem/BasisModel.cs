using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPBasic.Interface;

namespace WPBasic.Basissystem
{
    public abstract class BasisModel : IModel
    {
        public int ID {get;set;}
        public override string ToString(){
            return ID.ToString();
        }
    }
}

    