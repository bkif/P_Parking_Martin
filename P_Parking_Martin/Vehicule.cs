using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_Parking_Martin
{
    /// <summary>
    /// vehicule avec une plaque et un ticket
    /// </summary>
    public class Vehicule
    {
        public string plaque;
        public Ticket ticket;

        public Vehicule(string plaque, Ticket ticket)
        {
            this.plaque = plaque;
            this.ticket = ticket;
        }
    }
}