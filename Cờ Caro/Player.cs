using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cờ_Caro
{
    public class Player
    {
        private string name;
        private string sign;
        public string Name { get => name; set => name = value; }
        public string Sign { get => sign; set => sign = value; }
        public Player(string name,string sign)
        {
            this.Name = name;
            this.Sign = sign;
        }

       
    }
}
