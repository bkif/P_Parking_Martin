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
///Date : 05.10.2026
///Description : Simulation d'un parking en cmd
///



//TODO remettre convontion de nommage de l'ETML et anglais manuellement
//TODO plus d'explication dans le rapport

namespace P_Parking_Martin
{
    /// <summary>
    /// Parking
    /// </summary>
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
        static int TARIF_PARKING = 1;
        public int place = 0;
        Vehicule[] Vehicules = new Vehicule[TAILLE_TOTALE];
        public bool[] AffichageTablo = new bool[TAILLE_TOTALE];
        public Random rand = new Random();


        /// <summary>
        /// Affiche les options possibles a choisir pour l'utilisateur
        /// </summary>
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
                            Console.WriteLine("Aurevoir!");
                            exit = false;
                            break;
                        default:
                            Console.WriteLine(ERROR1);
                            break;
                    }
                }
            }
        }

        public void Entree()
        {
            int heure_entree = rand.Next(1, 24);
            int minute_entree = rand.Next(1, 60);
            int tarif = TARIF_PARKING;
            Console.WriteLine("Veuillez entrer la plaque d'immatriculation");
            Console.Write("Plaque : ");
            string plaque_entree = Console.ReadLine();
            //TODO ajout du placement aleatoire des vehicules
            place = rand.Next(0, TAILLE_TOTALE);
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
                if (int.TryParse(choix_plaque, out int nb))
                {
                    if (p != null)
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
                //
                else if (string.IsNullOrWhiteSpace(choix_plaque))
                {
                    if (p != null)
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
        }

        /// <summary>
        /// Affiche l'etat du parking
        /// </summary>
        public void EtatParking()
        {

            int Places_Occupees = 0;
            foreach (Vehicule p in Vehicules)
            {
                if (p != null)
                {
                    Places_Occupees++;
                }
                else
                {
                }
            }
            int Places_Libres = Vehicules.Length - Places_Occupees;
            decimal Taux_occupation = (decimal)Places_Occupees / (decimal)Vehicules.Length * 100;
            int places = 0;
            int placeaf = 1 + places;
            Random rand = new Random();
            int heure_passee = rand.Next(1, 3);
            int minute_passee = rand.Next(1, 24);
            Console.WriteLine("=== ETAT DU PARKING ===");
            Console.WriteLine($"Place totales: {Vehicules.Length}");
            Console.WriteLine($"Place occupees: {Places_Occupees}");
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
                    Console.WriteLine($"Place : {p.ticket.parking_place.place + 1}: plaque : {p.plaque} (passé {p.ticket.heure_entree - heure_passee} heures sur le parking)");
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
        /// <summary>
        /// Affiche les statistiques du jour
        /// </summary>
        public void Statistiques()
        {
            decimal nb_place_occupees = 0;
            foreach (Vehicule v in Vehicules)
            {
                if (v != null)
                {
                    nb_place_occupees++;
                }
            }
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
            return;
        }
    }
}