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
        public string[] Parking_Affichee = { };
        public bool[] Parking_Vrai = { };
        public bool fif = true;

        public void Menu()
        {
            
            Console.WriteLine("=== MENU PRINCIPAL ===");
            Console.WriteLine("1. Entrée d'un véhicule");
            Console.Write("Votre choix: ");
            int choix = int.Parse(Console.ReadLine());
            if (choix == 1)
            {
                Console.Clear();
                entree();
            }
            if (choix == 2)
            {

            }
            if (choix == 3)
            {

            }
            /*if (choix != 1 || choix != 2 || choix != 3)
                {
                Console.WriteLine("no");
                Menu();
            }*/
        }
 
        public void entree()
        {
            Random rand = new Random();
            Console.WriteLine("Veuillez entrer la plaque d'immatriculation");
            string plaque_entree = Console.ReadLine();
            int heure_entree = rand.Next(1,24);
            int minute_entree = rand.Next(1, 60);
            Vehicule car = new Vehicule($"{plaque_entree}", heure_entree, 1, 1);
            Console.Clear();
            Console.WriteLine("Nouveau vehicule crée!");
            Console.WriteLine($"Plaque : {plaque_entree}");
            Console.WriteLine($"Heure d'entrée: {heure_entree}:{minute_entree}");
            Parking_Affichee[1] = "X";

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
