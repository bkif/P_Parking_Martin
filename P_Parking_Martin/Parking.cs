using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
///ETML 
///Auteur : Martin Ivliev
///Date : 29.10.2026
///Description : Simulation d'un parking en cmd

//TODO remettre convontion de nommage de l'ETML
//TODO plus d'explication dans le rapport
//le word en pdf

namespace P_Parking_Martin
{
    /// <summary>
    /// Parking
    /// </summary>
    public class Parking
    {
        static string ERROR1 = "Veuillez entrer un chiffre entre 1 et 5";
        static readonly int TAILLE_TOTALE = 20; // taille du parking maximal
        static readonly int TARIF_PARKING = 1;
        static string MENU_TEXT =
            "=== MENU PRINCIPAL ===\r\n" +
            "1. Entrée d'un véhicule\r\n" +
            "2. Sortie d'un véhicule\r\n" +
            "3. Afficher l'état du parking\r\n" +
            "4. Rechercher un véhicule\r\n" +
            "5. Statistiques du jour\r\n" +
            "6. Historique des transactions\r\n" +
            "0. Quitter";
        public int place = 0;
        Vehicule[] Vehicules = new Vehicule[TAILLE_TOTALE];
        public bool[] AffichageTablo = new bool[TAILLE_TOTALE];
        public Random rand = new Random();
        public bool exit = true;

        /// <summary>
        /// Affiche les options possibles a choisir pour l'utilisateur
        /// </summary>
        public void Menu()
        {
            while (exit)
            {
                Console.WriteLine(MENU_TEXT);
                Console.Write("Votre choix : ");
                string choix = Console.ReadLine();
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
                        Console.WriteLine("Aurevoir!");
                        exit = false;
                        break;
                    default:
                        Console.WriteLine(ERROR1);
                        break;
                }
            }
        }
        public void Entree()
        {
            int heure_entree = rand.Next(1, 24);
            int minute_entree = rand.Next(1, 60);
            int heure_sortie = rand.Next(1, 24);
            int minute_sortie = rand.Next(1, 60);
            Console.WriteLine("Veuillez entrer la plaque d'immatriculation");
            Console.Write("Plaque : ");
            string plaque_entree = Console.ReadLine();
            //verification pas null
            if (plaque_entree is not null)
            {
                //pas moin que 3 ou plus que 8 longuer
                if (plaque_entree.Length < 3 || plaque_entree.Length > 8)
                {
                    return;
                }
                //2 premiers lettre
                if (!char.IsLetter(plaque_entree[0]) || !char.IsLetter(plaque_entree[1]))
                {
                    return;
                }
                //de 3 a 8 chiffres
                for (int a = 2; a < plaque_entree.Length; a++)
                {
                    if (!char.IsDigit(plaque_entree[a]))
                    {
                        return;
                    }
                }
                //si le vehicule est deja sur le parking c'est invalide
                foreach (Vehicule p in Vehicules)
                {
                    if (p != null)
                    {
                        if (plaque_entree == p.plaque)
                        {
                            Console.WriteLine("le vehicule se trouve deja sur le parking");
                            return;
                        }
                    }
                }
            }
            //si la personne a rien mis return
            else
            {
                return;
            }
            //la plaque est valide donc
            int place;
            do
            {
                place = rand.Next(0, TAILLE_TOTALE);
            }
            while (Vehicules[place] != null);

            Ticket ticket = new Ticket(place, heure_entree, heure_sortie, TARIF_PARKING);
            Vehicule vehicule = new Vehicule($"{plaque_entree}", ticket);
            Console.Clear();
            Console.WriteLine("Nouveau vehicule crée!\n");
            //plaque
            Console.WriteLine($"Plaque : {plaque_entree}");
            Console.WriteLine("Voici votre ticket : ");
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
            Console.WriteLine($"| Place : {vehicule.ticket.Parking_place.place + 1}");
            Console.WriteLine($"| Tarif horaire : {vehicule.ticket.Tarif}.- CHF");
            Console.WriteLine("----------------------------");
            Console.WriteLine("");
            Vehicules[place] = vehicule;
            AffichageTablo[place] = true;
            return;

        }

        /// <summary>
        /// Sort le vehicule du parking
        /// </summary>
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
                    if (choix_plaque == p.plaque)
                    {
                        Console.WriteLine($"Le vehicule avec la plaque | {p.plaque} | se trouve sur le parking a la place {p.ticket.Parking_place.place}");
                        Console.WriteLine($"Heure d'entrée : {p.ticket.Heure_entree}");
                        Console.WriteLine($"Heure de sortie : {heure_sortie}");
                        Console.WriteLine($"Le montant a payer est de : {p.ticket.Heure_entree - heure_sortie * p.ticket.Tarif}.- CHF");
                        Console.WriteLine("Voulez vous vraiment sortir ce vehicule? (Oui/non)");
                        string choix_sortie = Console.ReadLine();
                        if (choix_sortie == "oui")
                        {
                            Vehicules[p.ticket.Parking_place.place] = null;
                            AffichageTablo[p.ticket.Parking_place.place] = false;
                            Console.WriteLine("Le vehicule est bien sortie du parking");
                        }
                        else
                        {
                            Console.WriteLine("Le vehicule n'est pas sortie du parking");
                        }
                    }
                }
            }
            return;
        }
        /// <summary>
        /// Affiche l'etat du parking
        /// </summary>
        /// 
        public int Places_Occupees()
        {
            int Places_Occupees = 0;
            foreach (Vehicule p in Vehicules)

                if (p != null)
                {
                    Places_Occupees++;
                }
                else
                {
                }

            return Places_Occupees;
        }

        public void EtatParking()
        {
            decimal Places_Occupeess = Places_Occupees();
            decimal Places_Libres = Vehicules.Length - Places_Occupeess;
            decimal Taux_occupation = (decimal)Places_Occupeess / Vehicules.Length * 100;
            int places = 0;
            int placeaf = 1 + places;
            Random rand = new Random();
            int heure_passee = rand.Next(1, 3);
            int minute_passee = rand.Next(1, 24);
            Console.WriteLine("=== ETAT DU PARKING ===");
            Console.WriteLine($"Place totales: {Vehicules.Length}");
            Console.WriteLine($"Place occupees: {Places_Occupeess}");
            Console.WriteLine($"Place libres: {Places_Libres}");
            Console.WriteLine($"Taux d'occupation {Taux_occupation}%");
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
                    Console.WriteLine($"Place : {p.ticket.Parking_place.place + 1}: plaque : {p.plaque} (passé {p.ticket.Heure_entree - heure_passee} heures sur le parking)");
                }
            }
            Console.WriteLine("");
            return;
        }

        /// <summary>
        /// Recherche le vehicule en faisant une boucle sur le tableau des vehicules
        /// </summary>
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
                        Console.WriteLine($"Heure d'entrée : {p.ticket.Heure_entree}");
                        Console.WriteLine($"Durée :{duree}");
                        Console.WriteLine($"Prix actuel : {prix_actuel}");
                        Console.WriteLine($"Place: {p.ticket.Parking_place.place + 1}");
                    }
                    else if (p.ticket.Parking_place.place == choix_place)
                    {
                        Console.WriteLine($"le vehicule {choix_plaque} se trouve sur le parking et voici le ticket");
                        Console.WriteLine($"Plaque : {p.plaque}");
                        Console.WriteLine($"Heure d'entrée : {p.ticket.Heure_entree}");
                        Console.WriteLine($"Durée :{duree}");
                        Console.WriteLine($"Prix actuel :{prix_actuel}");
                        Console.WriteLine($"Place: {p.ticket.Parking_place.place + 1}");
                    }
                    else
                    {
                    }
                }
            }
            return;
        }
        /// <summary>
        /// Affiche les statistiques du jour
        /// </summary>
        decimal nb_place_occupees = 0;
        public int Count(Vehicule p)
        {
            foreach (Vehicule v in Vehicules)
            {
                if (p != null)
                {
                    nb_place_occupees++;
                }
            }
            return (int)nb_place_occupees;
        }
        public void Statistiques()
        {
            decimal nb_place_occupees_pourcent = nb_place_occupees * 100 / Vehicules.Length;
            Console.WriteLine("=== STATISTIQUES DU JOUR ===");
            Console.WriteLine($"Pourcentage des places occupéees : {nb_place_occupees_pourcent} %");
            return;
        }
        /// <summary>
        /// Affiche l'historique des transactions
        /// </summary>
        public void Historique()
        {
            Console.WriteLine("Affichage des transactions effectuées");
            foreach (Vehicule p in Vehicules)
            {
                if (p != null)
                {
                    Console.WriteLine($"ENTREE ({p.ticket.Heure_entree}h) - {p.plaque} n.place: {p.ticket.Parking_place}");
                    Console.WriteLine($"SORTIE ({p.ticket.Heure_sortie}h) - {p.plaque} n.place: {p.ticket.Parking_place} , Montant payé {p.ticket.Tarif * p.ticket.Heure_entree - p.ticket.Heure_sortie}\n");
                }
            }
            return;
        }
    }
}