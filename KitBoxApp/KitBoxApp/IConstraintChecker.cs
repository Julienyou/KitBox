using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    interface IConstraintChecker<T>
    {
        bool Check(T param);
    }
}
