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
        static int taille_totale = 20;
        Vehicule[] Vehicules = new Vehicule[taille_totale];
        public bool[] AffichageTablo = new bool[taille_totale];
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
            string choix = Console.ReadLine();
            if (choix != "1" && choix != "2" && choix != "3" && choix != "4" && choix != "5")
            {
                Console.Clear();
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERREUR(1) {error1}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("");
                Console.WriteLine("Exemples corrects: 1 ou 2 ou 3 ou 4 ou 5");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
                Menu();
            }
            if (choix == "1")
            {
                Console.Clear();
                Entree();
            }
            if (choix == "2")
            {
                Console.Clear();
                Sortie();

            }
            if (choix == "3")
            {
                Console.Clear();
                EtatParking();
            }
            if (choix == "4")
            {
                Console.Clear();
                Rechercher();
            }
            if (choix == "5")
            {
                Console.Clear();
                Statistiques();
            }
            // int choix = int.Parse(Console.ReadLine());
            /*if (choix != 1 || choix != 2 || choix != 3)
               {
               Console.WriteLine("no");
               Menu();
           }*/
        }

        public void Statistiques()
        {
            Console.WriteLine("=== STATISTIQUES DU JOUR ===");
            Console.WriteLine($"Nombre de place occupées: {Vehicules.Count(x => x != null)}");
            Console.WriteLine($"Pourcentage du parking occupé total: {(Vehicules.Count(x => x != null) / (double)taille_totale * 100)}%");
            Console.WriteLine($"Montant total payé: ");
            Menu();
        }

        public void Rechercher()
        {
            Console.WriteLine("Veuillez entrer la plaque d'immatriculation du véhicule à rechercher:");
            string choix_plaque = Console.ReadLine();
            bool trouve = false;
            Random rand = new Random();
            int duree = rand.Next(1, 24);
            int prix_actuel = rand.Next(1, 100);
            foreach (Vehicule p in Vehicules)
            {
                if (p != null)
                {

                    if (p.plaque == choix_plaque)
                    {
                        Console.WriteLine($"le vehicule {choix_plaque} se trouve sur le parking et voici le ticket");
                        Console.WriteLine($"Plaque {p.plaque}");
                        Console.WriteLine($"Heure d'entrée {p.heure_entree}");
                        Console.WriteLine($"Durée {duree}");
                        Console.WriteLine($"Prix actuel {prix_actuel}");
                        Console.WriteLine($"Place: {p.place+1}");
                    }
                    else
                    {

                    }
            }
        }
        Menu();
        }
        public void EtatParking()
        {
            double occupe = Vehicules.Count(x => x != null);
            double libres = Vehicules.Length - Vehicules.Count(x => x != null);
            double taux_occupe = (occupe) / (Vehicules.Length) * (100);
            int places = 0;
            int placeaf = 1 + places;
            Random rand = new Random();
            int heure_passee = rand.Next(1, 24);
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
                    Console.WriteLine($"Place {p.place + 1}: {p.plaque} (depuis {p.heure_entree - heure_passee})");
                }
            }
            Console.WriteLine("");
            Menu();
        }

        int place = 0;
        public void Entree()
        {
            int plaque_nb_max = 8;
            Random rand = new Random();
            Console.WriteLine("Veuillez entrer la plaque d'immatriculation");
            Console.Write("Plaque : ");
            string plaque_entree = Console.ReadLine();
            if (plaque_entree.Length <= plaque_nb_max)
            {
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
                Console.WriteLine($"| Place : {place+1}");
                Console.WriteLine($"| Tarif : 30 minutes = +1.-");
                if (heure_entree < 10)
                {
                    Console.WriteLine($"| Heure d'entrée: 0{heure_entree}:{minute_entree}");
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
            }
            else
            {
                Console.WriteLine("ya plus de 8");
            }
            Menu();
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
                        Console.WriteLine("voulez vous vraiment sortir ce vehicule?");
                        string choix_sortie = Console.ReadLine();
                        if (choix_sortie == "oui")
                        {
                            AffichageTablo[p.place] = false;
                            place--;
                            Console.WriteLine("le vehicule est bien sortie du parking");
                        }
                        else if (choix_sortie == "")
                        {
                            Console.WriteLine("le vehicule n est pas sortie du parking");
                            Menu();
                        }
                    }
                    else
                    {

                    }

                }
                Console.WriteLine("Ce vehicule ne se trouve pas sur le parking");
                Console.WriteLine("Toutefois, voici tout les vehicule sur le parking presents : ");
                Console.WriteLine("");
                foreach (Vehicule c in Vehicules)
                {
                    if (c != null)
                    {
                        Console.WriteLine($"Place {c.place + 1}: {c.plaque}");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("Est ce que l'un de ses vehicules vous appartien?");
                Console.WriteLine("Si oui, veuillez entrer la plaque d'immatriculation du vehicule, sinon ecrivez rien et appuyez entrer");
                Console.Write("Plaque : ");
                string reesaye = Console.ReadLine();
                foreach (Vehicule h in Vehicules)
                {
                    if (h != null)
                    {

                        if (h.plaque == reesaye)
                        {
                            Console.WriteLine($"le vehicule {reesaye} se trouve sur le parking");
                            Console.WriteLine("voulez vous vraiment sortir ce vehicule?");
                            Console.Write("Oui / Non : ");
                            string choix_sortie = Console.ReadLine();
                            if (choix_sortie == "oui")
                            {
                                AffichageTablo[p.place] = false;
                                place--;
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
                if (reesaye == "")
                {
                    Menu();
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



/*if (x == "1")
{
    Console.Write("Veuillez entrer votre plaque d'immatriculaltion: ");
    string d = Console.ReadLine();
    if (d == "VD123456")
    {
        class1.parking[1] = "X";
        for (int i = 0; i < class1.parking.Length; i++)
        {
            Console.WriteLine($"{class1.parking[i]}");
            classy = "VD123456";
        }
    }
    else
    {

    }
}*/
/*Console.WriteLine("=== ETAT DU PARKING ===");
            Console.WriteLine("Places totales: 20");
            Console.WriteLine("Places occupées:");
            Console.WriteLine("Taux d'occupation:");
            Console.WriteLine("");
            Console.WriteLine("Plan du parking (L=Libre, X=Occupé");
            for (int i = 1; i < 6; i++)
            {
                Console.Write($"|0{i}:{parking[i]}| ");
            }
            Console.WriteLine("");
            for (int i = 6; i < 10; i++)
            {
                Console.Write($"|0{i}:{parking[i]}| ");
            }
            Console.Write($"|{10}:{parking[10]}| ");
            Console.WriteLine("");
            for (int i = 11; i < 16; i++)
            {
                Console.Write($"|{i}:{parking[i]}| ");
            }
            Console.WriteLine("");
            for (int i = 16; i < 21; i++)
            {
                Console.Write($"|{i}:{parking[i]}| ");
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"Véhicules présents: {y}");
            Console.WriteLine("");
*/
