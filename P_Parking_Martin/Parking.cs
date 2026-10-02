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
    public class Parking_place
    {
    }

    public class Ticket
    {
        public int place;
        public int heure_entree;
        public int tarif;

        public Ticket(int place, int heure_entree, int tarif)
        {
            this.place = place;
            this.heure_entree = heure_entree;
            this.tarif = tarif;
        }
    }

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

    public class Parking
    {
        static int TAILLE_TOTALE = 20;
        Vehicule[] Vehicules = new Vehicule[TAILLE_TOTALE];
        public bool[] AffichageTablo = new bool[TAILLE_TOTALE];
        public bool fif = true;
        static string ERROR1 = "Veuillez entrer un chiffre entre 1 et 5";

        public void Menu()
        {
            bool exit = true;
            while (exit)
            {
                Console.WriteLine("=== MENU PRINCIPAL ===");
                Console.WriteLine("1. Entrée d'un véhicule");
                Console.WriteLine("2. Sortie d'un véhicule");
                Console.WriteLine("3. Afficher l etat du parking");
                Console.WriteLine("4. Rechercher un véhicule");
                Console.WriteLine("5. Statistiques du jour");
                Console.WriteLine("0. Quitter");
                Console.Write("Votre choix : ");
                string choix = Console.ReadLine();
                {
                    switch (choix)
                    {
                        case "1":
                            Entree();
                            break;
                        case "2":
                            Sortie();
                            break;
                        case "3":
                            EtatParking();
                            break;
                        case "4":
                            Rechercher();
                            break;
                        case "5":
                            Statistiques();
                            break;
                        case "6":
                            Historique();
                            break;
                        case "0":
                            Console.WriteLine("");
                            exit = false;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine(ERROR1);
                            break;
                    }
                }
            }
        }

        int place = 0;
        public void Historique()
        {

        }
        public void Entree()
        {
            int plaque_nb_max = 8;
            Random rand = new Random();
            Console.WriteLine("Veuillez entrer la plaque d'immatriculation");
            Console.Write("Plaque : ");
            string plaque_entree = Console.ReadLine();
            int heure_entree = rand.Next(1, 24);
            int minute_entree = rand.Next(1, 60);
            int tarif = 1;
            foreach (Vehicule p in Vehicules)
            {
                if (p != null)
                {
                    if (p.plaque == plaque_entree)
                    {
                        Console.WriteLine($"le vehicule {plaque_entree} se trouve deja sur le parking");
                    }
                }
            }
            Ticket ticket = new Ticket(place, heure_entree, tarif);
            Vehicule vehicule = new Vehicule($"{plaque_entree}", ticket);
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
            Vehicules[place] = vehicule;
            AffichageTablo[place] = true;
            place++;
        }

        public void Statistiques()
        {
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
                        Console.WriteLine($"Heure d'entrée : {p.ticket.heure_entree}");
                        Console.WriteLine($"Durée :{duree}");
                        Console.WriteLine($"Prix actuel : {prix_actuel}");
                        Console.WriteLine($"Place: {p.ticket.place + 1}");
                    }
                    else if (p.ticket.place == choix_place)
                    {
                        Console.WriteLine($"le vehicule {choix_plaque} se trouve sur le parking et voici le ticket");
                        Console.WriteLine($"Plaque : {p.plaque}");
                        Console.WriteLine($"Heure d'entrée : {p.ticket.heure_entree}");
                        Console.WriteLine($"Durée :{duree}");
                        Console.WriteLine($"Prix actuel :{prix_actuel}");
                        Console.WriteLine($"Place: {p.ticket.place + 1}");
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
                    Console.WriteLine($"Place : {p.ticket.place + 1}: plaque : {p.plaque} (passé {p.ticket.heure_entree - heure_passee} heures sur le parking)");
                }
            }
            Console.WriteLine("");
        }

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
                        Console.WriteLine($"le montant a payer {p.ticket.heure_entree - 3 * 1}.- chf");
                        Console.WriteLine("voulez vous vraiment sortir ce vehicule? (Oui/non)");
                        string choix_sortie = Console.ReadLine();
                        //si oui supprime du tablo vehicules et X=>L
                        if (choix_sortie == "oui")
                        {
                            Vehicules[p.ticket.place] = null;
                            AffichageTablo[p.ticket.place] = false;
                            Console.WriteLine("le vehicule est bien sortie du parking");
                            Menu();
                        }
                        else if (choix_sortie == "")
                        {
                            Console.WriteLine("le vehicule n est pas sortie du parking");
                        }
                    }
                }
            }
        }

        public void Test()
        {
        }
    }
}