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
        public int Heure_entree {  get; set; }
        public int Heure_sortie { get; set; }
        public int Tarif { get; set; }
        public Parking_place Parking_place { get; set; }

        public Ticket(int place, int heure_sortie, int heure_entree, int tarif)
        {
            this.Parking_place = new Parking_place(place);
            this.Heure_entree = heure_entree;
            this.Heure_sortie = heure_sortie;
            this.Tarif = tarif;
        }
    }
}