using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace P_Parking_Martin
{
    public class Parking_class
    {
        //les tablos
        static int TAILLE_TOTALE = 20;
        Vehicule[] Vehicules = new Vehicule[TAILLE_TOTALE];
        public bool[] AffichageTablo = new bool[TAILLE_TOTALE];
        public bool fif = true;



        //les erreurs
        static string error1 = "Veuillez entrer un chiffre entre 1 et 5";

        //menu principal
        public void Menu()
        {
            Console.WriteLine("=== MENU PRINCIPAL ===");
            Console.WriteLine("1. Entrée d'un véhicule");
            Console.WriteLine("2. Sortie d'un véhicule");
            Console.WriteLine("3. Afficher l etat du parking");
            Console.WriteLine("4. Rechercher un véhicule");
            Console.WriteLine("5. Statistiques du jour");
            Console.Write("Votre choix : ");
            int choix = Int32.Parse(Console.ReadLine());
            bool exit = true;
            while (exit)
            {
                switch (choix)
                {
                    case 1:
                        Entree();
                        break;
                    case 2:
                        Sortie();
                        break;
                    case 3:
                        Statistiques();
                        break;
                    case 0:
                        Console.WriteLine("");
                        break;
                }
            }
        }
        public void Statistiques()
        {
            //todo
            Console.WriteLine("=== STATISTIQUES DU JOUR ===");
            Console.WriteLine($"Montant total payé: ");
            return;
        }

        public void Rechercher()
        {
            Console.WriteLine("Veuillez entrer la plaque d'immatriculation / place du véhicule à rechercher:");
            string choix_plaque = Console.ReadLine();
            int choix_place = 1;
            bool trouve = false;
            Random rand = new Random();
            int heure_passee = rand.Next(1, 3);
            int duree = rand.Next(1, 24);
            int prix_actuel = rand.Next(1, 100);
            foreach (Vehicule p in Vehicules)
            {
                if (p != null)
                {

                    if (p.plaque == choix_plaque)
                    {
                        Console.WriteLine($"le vehicule {choix_plaque} se trouve sur le parking et voici le ticket");
                        Console.WriteLine($"Plaque : {p.plaque}");
                        Console.WriteLine($"Heure d'entrée : {p.heure_entree}");
                        Console.WriteLine($"Durée :{duree}");
                        Console.WriteLine($"Prix actuel : {prix_actuel}");
                        Console.WriteLine($"Place: {p.place + 1}");
                    }
                    else if (p.place == choix_place)
                    {
                        Console.WriteLine($"le vehicule {choix_plaque} se trouve sur le parking et voici le ticket");
                        Console.WriteLine($"Plaque : {p.plaque}");
                        Console.WriteLine($"Heure d'entrée : {p.heure_entree}");
                        Console.WriteLine($"Durée :{duree}");
                        Console.WriteLine($"Prix actuel :{prix_actuel}");
                        Console.WriteLine($"Place: {p.place + 1}");
                    }
                    else
                    {
                    }
                }
            }
            return;
        }


        public void EtatParking()
        {
            double occupe = Vehicules.Count(x => x != null);
            double libres = Vehicules.Length - Vehicules.Count(x => x != null);
            double taux_occupe = (occupe) / (Vehicules.Length) * (100);
            int places = 0;
            int placeaf = 1 + places;
            Random rand = new Random();
            int heure_passee = rand.Next(1, 3);
            int minute_passee = rand.Next(1, 24);
            Console.WriteLine("=== ETAT DU PARKING ===");
            Console.WriteLine($"Place totales: {Vehicules.Length}");
            Console.WriteLine($"Place occupees: {occupe}");
            Console.WriteLine($"Place libres: {libres}");
            Console.WriteLine($"Taux d'occupation {taux_occupe}.0%");
            Console.WriteLine("");
            Console.Write("Plan du parking (");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("L");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("=Libre, ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("X");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("=Occupé");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("):");
            Console.WriteLine("");
            Console.WriteLine("");

            while (places < Vehicules.Length)
            {
                string ecriture = $"0{placeaf}";
                string lettre = "";
                if (placeaf > 9)
                {
                    ecriture = $"{placeaf}";
                }
                if (AffichageTablo[places] is false)
                {
                    lettre = "L";
                }
                if (AffichageTablo[places] is true)
                {
                    lettre = "X";
                }
                if (lettre == "X")
                {

                    Console.Write($"|{ecriture}:");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{lettre}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("| ");
                }
                else
                {
                    Console.Write($"|{ecriture}:");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{lettre}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("| ");
                }
                Console.ForegroundColor = ConsoleColor.White;
                places++;
                placeaf++;
                if (places % 5 == 0)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Vehicules presents:");

            foreach (Vehicule p in Vehicules)
            {
                if (p != null)
                {
                    Console.WriteLine($"Place : {p.place + 1}: plaque : {p.plaque} (passé {p.heure_entree - heure_passee} heures sur le parking)");
                }
            }
            Console.WriteLine("");
            Menu();
        }
        int place = 0;
        /// <summary>
        /// entre le vehicule dans le parking en le creant et affiche dans le tablo d affichage il passe en occupé
        /// </summary>
        public void Entree()
        {
            int plaque_nb_max = 8;
            Random rand = new Random();
            Console.WriteLine("Veuillez entrer la plaque d'immatriculation");
            Console.Write("Plaque : ");
            string plaque_entree = Console.ReadLine();
            int vint = 0;
            while (20 > vint)
            {
                foreach (Vehicule p in Vehicules)
                {
                    if (p != null)
                    {
                        if (p.plaque == plaque_entree)
                        {
                            Console.WriteLine($"le vehicule {plaque_entree} se trouve deja sur le parking");
                            Menu();
                        }
                    }
                }
                vint++;
            }
            int heure_entree = rand.Next(1, 24);
            int minute_entree = rand.Next(1, 60);
            int tarif = 1;
            Vehicule car = new Vehicule($"{plaque_entree}", place, heure_entree, tarif);
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("----------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("| Nouveau vehicule crée!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"| Plaque : {plaque_entree}");
            Console.WriteLine($"| Place : {place + 1}");
            Console.WriteLine($"| Tarif : 30 minutes = +1.-");

            if (heure_entree < 10 && minute_entree < 10)
            {
                Console.WriteLine($"| Heure d'entrée: 0{heure_entree}:0{minute_entree}");
            }
            else if (heure_entree < 10)
            {
                Console.WriteLine($"| Heure d'entrée: 0{heure_entree}:{minute_entree}");
            }
            else if (minute_entree < 10)
            {
                Console.WriteLine($"| Heure d'entrée: {heure_entree}:0{minute_entree}");
            }
            else
            {
                Console.WriteLine($"| Heure d'entrée: {heure_entree}:{minute_entree}");
            }
            Console.WriteLine("----------------------------");
            Console.WriteLine("");
            Vehicules[place] = car;
            AffichageTablo[place] = true;
            place++;

            Menu();
        }
        /// <summary>
        /// sort le vehicule du parking en mettant le tablo d'affichage en question libre et en le supprimant du tablo vehicules
        /// </summary>
        public void Sortie()
        {

            Console.WriteLine("Veuillez entrer la plaque d'immatriculation du véhicule à sortir:");
            Console.Write("Plaque : ");
            string choix_plaque = Console.ReadLine();
            foreach (Vehicule p in Vehicules)
            {
                if (p != null)
                {

                    if (p.plaque == choix_plaque)
                    {
                        Console.WriteLine($"le vehicule {choix_plaque} se trouve sur le parking");
                        Console.WriteLine($"le montant a payer {p.heure_entree - 3 * 1}.- chf");
                        Console.WriteLine("voulez vous vraiment sortir ce vehicule? (Oui/non)");
                        string choix_sortie = Console.ReadLine();
                        //si oui supprime du tablo vehicules et X=>L
                        if (choix_sortie == "oui")
                        {
                            Vehicules[p.place] = null;
                            AffichageTablo[p.place] = false;
                            Console.WriteLine("le vehicule est bien sortie du parking");
                            Menu();
                        }
                        else if (choix_sortie == "")
                        {
                            Console.WriteLine("le vehicule n est pas sortie du parking");
                            Menu();
                        }
                    }
                }
            }
        }
        public void Test()
        {

        }
    }
    public class Vehicule
    {
        public string plaque;
        public int place;
        public int heure_entree;
        public int tarif;

        public Vehicule(string plaque, int place, int heure_entree, int tarif)
        {
            this.plaque = plaque;
            this.place = place;
            this.heure_entree = heure_entree;
            this.tarif = tarif;
        }
    }
}
