using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_Parking_Martin
{
    /// <summary>
    /// Ticket attribué au vehicule qui indique la place actuelle avec l'heure d'entrée et le tarif
    /// </summary>
    public class Ticket
    {
        public int heure_entree;
        public int tarif;
        public Parking_place parking_place;

        public Ticket(int place, int heure_entree, int tarif)
        {
            this.parking_place = new Parking_place(place);
            this.heure_entree = heure_entree;
            this.tarif = tarif;
        }
    }
}