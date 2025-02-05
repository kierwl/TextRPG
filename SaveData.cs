using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textrpg
{
    [Serializable]
    class SaveData
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public int AttackPower { get; set; }
        public int Defense { get; set; }
    }

}
