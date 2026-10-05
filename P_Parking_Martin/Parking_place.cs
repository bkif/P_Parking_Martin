using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_Parking_Martin
{
    /// <summary>
    /// Represente la place du vehicule dans le tableau (vrai) qui est assigné au ticket
    /// </summary>
    public class Parking_place
    {
        public int place = 0;
        public Parking_place(int place)
        {
            this.place = place;
        }
    }
}