using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public class Card
    {
        private int value;
        private bool flipped;

        public int Value
        {
            get { return value; }
        }

        public bool Flipped
        {
            get { return flipped; }
            set { flipped = value; }
        }

        public Card(int value)
        {
            this.value = value;
            flipped = false;
        }

    }
}




