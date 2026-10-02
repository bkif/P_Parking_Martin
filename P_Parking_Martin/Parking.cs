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
        public int place = 0;
            public Parking_place(int place)
        {
            this.place = place;
        }
    }

    public class Ticket
    {
        public int heure_entree;
        public int tarif;
        public Parking_place parking_place;

        public Ticket(int place, int heure_entree, int tarif)
        {
            this.parking_place.place = place;
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
        static string ERROR1 = "Veuillez entrer un chiffre entre 1 et 5";
        public static string MENU_TEXT =
            "=== MENU PRINCIPAL ===\r\n" +
            "1. Entrée d'un véhicule\r\n" +
            "2. Sortie d'un véhicule\r\n" +
            "3. Afficher l'état du parking\r\n" +
            "4. Rechercher un véhicule\r\n" +
            "5. Statistiques du jour\r\n" +
            "6. Historique des transactions\r\n" +
            "0. Quitter";
        static int TAILLE_TOTALE = 20;
        public int place = 0;
        Vehicule[] Vehicules = new Vehicule[TAILLE_TOTALE];
        Parking_place[] parking_places = new Parking_place[TAILLE_TOTALE];
        public bool[] AffichageTablo = new bool[TAILLE_TOTALE];
        public Random rand = new Random();

        public void Menu()
        {
            bool exit = true;
            while (exit)
            {
                Console.WriteLine($"{MENU_TEXT}");
                Console.Write("Votre choix : ");
                string choix = Console.ReadLine();
                {
                    Console.Clear();
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
                            Console.WriteLine("Goodbye!");
                            exit = false;
                            break;
                        default:
                            Console.WriteLine(ERROR1);
                            break;
                    }
                }
            }
        }

        public void Historique()
        {

        }
        public void Entree()
        {
            int heure_entree = rand.Next(1, 24);
            int minute_entree = rand.Next(1, 60);
            int tarif = 1;
            Console.WriteLine("Veuillez entrer la plaque d'immatriculation");
            Console.Write("Plaque : ");
            string plaque_entree = Console.ReadLine();
            place = rand.Next(0,20);
            foreach (Parking_place parking_place in parking_places)
            {
                if (parking_place == null)
                {

                    if (place = parking_place.place)
                    {
                        Console.WriteLine("cette place est deja prise, rerandom");
                        place = rand.Next(0, 20);
                    }
                    else
                    {
                        Console.WriteLine("cette place n'est pas prise, allez-y");
                    }
                }
                else
                {
                    return;
                }
            }
            //todo : verification de plaque 2 chiffre et 6 lettres /
            //si le parking est plein demande de sortir un vehicule et
            //parcour le tablo pour verifier la place disponible pour ensuite lui attribuer la place
            Ticket ticket = new Ticket(place, heure_entree, tarif);
            Vehicule vehicule = new Vehicule($"{plaque_entree}", ticket);
            Console.Clear();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Nouveau vehicule crée!");
            Console.ForegroundColor = ConsoleColor.White;
            //plaque
            Console.WriteLine($"Plaque : {plaque_entree}");
            Console.WriteLine("Et voici votre ticket : ");
            //ticket attribué au vehicule qui s'affiche
            Console.WriteLine("----------------------------");
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
            Console.WriteLine($"| Place : {vehicule.ticket.parking_place.place + 1}");
            Console.WriteLine($"| Tarif horaire : {vehicule.ticket.tarif}.- CHF");
            Console.WriteLine("----------------------------");
            Console.WriteLine("");
            Vehicules[place] = vehicule;
            AffichageTablo[place] = true;
            place++;
            return;
        }
        public void Sortie()
        {
            Console.WriteLine("Veuillez entrer la plaque d'immatriculation ou le numero de place du véhicule à sortir:");
            Console.Write("Plaque / numero de place : ");
            int heure_sortie = rand.Next(1, 24);
            string choix_plaque = Console.ReadLine();
            foreach (Vehicule p in Vehicules)
            {
                if (p != null)
                {
                    if (p.plaque == choix_plaque)
                    {
                        Console.WriteLine($"Le vehicule avec la plaque | {p.plaque} | se trouve sur le parking a la place {p.ticket.parking_place.place}");
                        Console.WriteLine($"Heure d'entrée : {p.ticket.heure_entree}");
                        Console.WriteLine($"Heure de sortie : {heure_sortie}");
                        Console.WriteLine($"Le montant a payer est de : {p.ticket.heure_entree - heure_sortie * p.ticket.tarif}.- CHF");
                        Console.WriteLine("Voulez vous vraiment sortir ce vehicule? (Oui/non)");
                        string choix_sortie = Console.ReadLine();
                        if (choix_sortie == "oui")
                        {
                            Vehicules[p.ticket.parking_place.place] = null;
                            AffichageTablo[p.ticket.parking_place.place] = false;
                            Console.WriteLine("Le vehicule est bien sortie du parking");
                        }
                        else
                        {
                            Console.WriteLine("Le vehicule n'est pas sortie du parking");
                        }
                        return;
                    }
                    else if (choix_plaque == (p.ticket.parking_place.place + 1).ToString())
                    {
                        Console.WriteLine($"Le vehicule avec la plaque | {p.plaque} | se trouve sur le parking a la place {p.ticket.parking_place.place}");
                        Console.WriteLine($"Heure d'entrée : {p.ticket.heure_entree}");
                        Console.WriteLine($"Heure de sortie : {heure_sortie}");
                        Console.WriteLine($"Le montant a payer est de : {p.ticket.heure_entree - heure_sortie * p.ticket.tarif}.- CHF");
                        Console.WriteLine("Voulez vous vraiment sortir ce vehicule? (Oui/non)");
                        string choix_sortie = Console.ReadLine();
                        if (choix_sortie == "oui")
                        {
                            Vehicules[p.ticket.parking_place.place] = null;
                            AffichageTablo[p.ticket.parking_place.place] = false;
                            Console.WriteLine("Le vehicule est bien sortie du parking");
                        }
                        else
                        {
                            Console.WriteLine("Le vehicule n'est pas sortie du parking");
                        }
                        return;
                    }
                }
            }
            return;
        }
        public void EtatParking()
        {
            //--- ia
            double occupe = Vehicules.Count(x => x != null);
            double libres = Vehicules.Length - Vehicules.Count(x => x != null);
            double taux_occupe = (occupe) / (Vehicules.Length) * (100);
            //---
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
                    Console.WriteLine($"Place : {p.ticket.parking_place.place + 1}: plaque : {p.plaque} (passé {p.ticket.heure_entree - heure_passee} heures sur le parking)");
                }
            }
            Console.WriteLine("");
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
                        Console.WriteLine($"Place: {p.ticket.parking_place.place + 1}");
                    }
                    else if (p.ticket.parking_place.place == choix_place)
                    {
                        Console.WriteLine($"le vehicule {choix_plaque} se trouve sur le parking et voici le ticket");
                        Console.WriteLine($"Plaque : {p.plaque}");
                        Console.WriteLine($"Heure d'entrée : {p.ticket.heure_entree}");
                        Console.WriteLine($"Durée :{duree}");
                        Console.WriteLine($"Prix actuel :{prix_actuel}");
                        Console.WriteLine($"Place: {p.ticket.parking_place.place + 1}");
                    }
                    else
                    {
                    }
                }
            }
            return;
        }

        public void Statistiques()
        {
            Console.WriteLine("=== STATISTIQUES DU JOUR ===");
            Console.WriteLine($"Montant total payé: ");
            return;
        }



        public void Test()
        {
            return;
        }
    }
}