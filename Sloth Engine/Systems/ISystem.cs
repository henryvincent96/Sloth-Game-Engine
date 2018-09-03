using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sloth_Engine.Systems
{
    public interface ISystem
    {
        void onUpdate();

        string Name
        {
            get;
        }
    }
}
