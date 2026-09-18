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
        static int taille_totale = 20;
        Vehicule[] Vehicules = new Vehicule[taille_totale];
        public bool[] AffichageTablo = new bool[taille_totale];
        public bool fif = true;

        public void Menu()
        {
            Console.WriteLine("=== MENU PRINCIPAL ===");
            Console.WriteLine("1. Entrée d'un véhicule");
            Console.WriteLine("2. Sortie d'un véhicule");
            Console.WriteLine("3. Afficher l etat du parking");
            Console.Write("Votre choix: ");
            string choix = Console.ReadLine();
            // int choix = int.Parse(Console.ReadLine());
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
            /*if (choix != 1 || choix != 2 || choix != 3)
                {
                Console.WriteLine("no");
                Menu();
            }*/
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
            Console.WriteLine("Plan du parking (L=Libre, X=Occupé):");

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
                Console.Write($"|{ecriture}:{lettre}| ");
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
            Random rand = new Random();
            Console.WriteLine("Veuillez entrer la plaque d'immatriculation");
            string plaque_entree = Console.ReadLine();
            int heure_entree = rand.Next(1, 24);
            int minute_entree = rand.Next(1, 60);
            int tarif = 1;
            Vehicule car = new Vehicule($"{plaque_entree}", place, heure_entree, tarif);
            Console.Clear();
            Console.WriteLine("Nouveau vehicule crée!");
            Console.WriteLine($"Plaque : {plaque_entree}");
            Console.WriteLine($"Heure d'entrée: {heure_entree}:{minute_entree}");
            Vehicules[place] = car;
            AffichageTablo[place] = true;
            place++;
            Menu();
        }
        public void Sortie()
        {
            Console.WriteLine();
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
                            Console.WriteLine("le vehicule est bien sortie du parking");
                        }
                        else if (choix_sortie == "")
                        {
                            Console.WriteLine("le vehicule n est pas sortie du parking");
                        }
                    }
                    else
                    {
                        Console.WriteLine("malheuresement non");
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
