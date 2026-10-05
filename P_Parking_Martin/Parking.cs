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
///Date : 05.10.2016 
///Description : parking en ligne de console
///

namespace P_Parking_Martin
{
    public class ParkingSpot
    {
        /// <summary>
        /// Index de la place dans le tableau
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Crée une place de parking
        /// </summary>
        public ParkingSpot(int index)
        {
            this.Index = index;
        }
    }

    /// <summary>
    /// Ticket donne au véhicule à son entrée
    /// </summary>
    public class Ticket
    {
        public int EntryHour { get; set; }
        public int EntryMinute { get; set; }
        public int HourlyRate { get; set; }
        public ParkingSpot ParkingSpot { get; set; }

        /// <summary>
        /// Crée le ticket d'un véhicule
        /// </summary>
        public Ticket(int spotIndex, int entryHour, int entryMinute, int hourlyRate)
        {
            this.ParkingSpot = new ParkingSpot(spotIndex);
            this.EntryHour = entryHour;
            this.EntryMinute = entryMinute;
            this.HourlyRate = hourlyRate;
        }
    }

    /// <summary>
    /// Un véhicule garé dans le parking
    /// </summary>
    public class Vehicle
    {
        public string LicensePlate { get; set; }
        public Ticket Ticket { get; set; }

        /// <summary>
        /// Crée un véhicule
        /// </summary>
        /// <param name="licensePlate">plaque d'imatriculation</param>
        /// <param name="ticket">ticket recu à l'entrée</param>
        public Vehicle(string licensePlate, Ticket ticket)
        {
            this.LicensePlate = licensePlate;
            this.Ticket = ticket;
        }
    }

    /// <summary>
    /// Le parking : gere les places, les véhicules et le menu
    /// </summary>
    public class Parking
    {
        private Vehicle[] _vehicles = new Vehicle[TOTAL_SPOTS];
        private bool[] _occupiedSpots = new bool[TOTAL_SPOTS];
        private const int MIN_ENTRY_HOUR = 1;
        private const int CURRENT_HOUR = 20;
        private const int CURRENT_MINUTE = 50;
        private const int TOTAL_SPOTS = 20;
        private const int SPOTS_PER_ROW = 5;
        private const int HOURLY_RATE = 1;
        private const int MINUTES_IN_HOUR = 60;
        private const string ERROR1 = "Veuillez entrer un chiffre entre 0 et 6";
        private Random _random = new Random();
        private int _nextSpotIndex = 0;
        private const string MENU_TEXT =
      "=== MENU PRINCIPAL ===\r\n" +
      "1. Entrée d'un véhicule\r\n" +
      "2. Sortie d'un véhicule\r\n" +
      "3. Afficher l'état du parking\r\n" +
      "4. Rechercher un véhicule\r\n" +
      "5. Statistiques du jour\r\n" +
      "6. Historique des transactions\r\n" +
      "0. Quitter";
        /// <summary>
        /// Affiche le menu et lance l option choisie jusqu'a ce que l'utilisateur quitte en entrant 0
        /// </summary>
        public void Menu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine($"{MENU_TEXT}");
                Console.Write("Votre choix : ");
                string choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        VehicleEntry();
                        break;
                    case "2":
                        VehicleExit();
                        break;
                    case "3":
                        ShowParkingState();
                        break;
                    case "4":
                        SearchVehicle();
                        break;
                    /*case "5":
                        //todo ShowStatistics();
                        break;*/
                    /*case "6":
                        //todo - affichage de l'historique des transactions
                        break;*/
                    case "0":
                        Console.WriteLine("Aurevoir!");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine(ERROR1);
                        break;
                }
            }
        }

        /// <summary>
        /// Teste la plaque, 2 lettre au debut et au max 6 chiffres
        /// </summary>
        public int NumberCheck(int number)
        {
            number = 6;

            return (number);
        }
        public string LettterCheck(string lettter)
        {
            lettter = Console.ReadLine();

            return (lettter);
        }
        /// <summary>
        /// Demande la plaque, donne la première place libre et affiche le ticket
        /// </summary>
        public void VehicleEntry()
        {

            Console.WriteLine("Veuillez entrer la plaque d'immatriculation");
            Console.Write("Plaque : ");
            string licensePlate = Console.ReadLine();
            //TODO vérifier que la plaque est cohérente 2 lettre et 6 chiffres
            //TODO réutiliser les places libérées par les sorties
            int spotIndex = _nextSpotIndex;
            if (spotIndex >= TOTAL_SPOTS)
            {
                Console.WriteLine("Le parking est plein, veuillez d'abord sortir un vehicule");
                return;
            }

            // l'heure d'entrée est simulée
            int entryHour = _random.Next(MIN_ENTRY_HOUR, CURRENT_HOUR);
            int entryMinute = _random.Next(0, MINUTES_IN_HOUR);
            string entryTime = $"{entryHour:00}:{entryMinute:00}";

            Ticket ticket = new Ticket(spotIndex, entryHour, entryMinute, HOURLY_RATE);
            Vehicle newVehicle = new Vehicle(licensePlate, ticket);
            Console.Clear();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Nouveau vehicule crée!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Plaque : {licensePlate}");
            Console.WriteLine("Et voici votre ticket : ");
            Console.WriteLine("----------------------------");
            Console.WriteLine($"| Heure d'entrée: {entryTime}");
            Console.WriteLine($"| Place : {spotIndex + 1}");
            Console.WriteLine($"| Tarif horaire : {HOURLY_RATE}.- CHF");
            Console.WriteLine("----------------------------");
            Console.WriteLine("");
            _vehicles[spotIndex] = newVehicle;
            _occupiedSpots[spotIndex] = true;
            _nextSpotIndex++;
        }
        

        /// <summary>
        /// Calcule le montant à payer et fait sortir le vehicule si l'utilisateur confirme
        /// </summary>


        public void VehicleExit()
        {

            Console.WriteLine("Veuillez entrer la plaque d'immatriculation ou le numero de place du vehicule à sortir:");
            Console.Write("Plaque / numero de place : ");
            string choice = Console.ReadLine();
            foreach (Vehicle parkedVehicle in _vehicles)
            {
                if (parkedVehicle != null)
                {
                    if (int.TryParse(choice, out int nomb))
                    {
                        int amount = parkedVehicle.Ticket.EntryHour - CURRENT_HOUR - parkedVehicle.Ticket.HourlyRate;
                        int exitHour = CURRENT_HOUR;
                        int spot = nomb;
                        int duration = exitHour - parkedVehicle.Ticket.EntryHour;
                        Console.WriteLine($"Le vehicule avec la plaque | {parkedVehicle.LicensePlate} | se trouve sur le parking a la place {parkedVehicle.Ticket.ParkingSpot.Index + 1}");
                        Console.WriteLine($"Heure d'entrée : {parkedVehicle.Ticket.EntryHour}h");
                        Console.WriteLine($"Heure de sortie : {exitHour}h");
                        Console.WriteLine($"Durée : {duration}h");
                        Console.WriteLine($"Le montant a payer est de : {amount}.- CHF");
                        Console.WriteLine("Voulez vous vraiment sortir ce vehicule? (oui/non)");
                        string confirmation = Console.ReadLine();
                        if (confirmation == "oui")
                        {
                            _vehicles[spot] = null;
                            _occupiedSpots[spot] = false;
                            Console.WriteLine("Le vehicule est bien sortie du parking");
                        }
                        else
                        {
                            Console.WriteLine("Le vehicule n'est pas sortie du parking");
                        }
                        return;
                    }
                    else if (!string.IsNullOrWhiteSpace(choice))
                    {
                        int amount = parkedVehicle.Ticket.EntryHour - CURRENT_HOUR - parkedVehicle.Ticket.HourlyRate;
                        int exitHour = CURRENT_HOUR;
                        int duration = exitHour - parkedVehicle.Ticket.EntryHour;
                        Console.WriteLine($"Le vehicule avec la plaque | {parkedVehicle.LicensePlate} | se trouve sur le parking a la place {parkedVehicle.Ticket.ParkingSpot.Index + 1}");
                        Console.WriteLine($"Heure d'entrée : {parkedVehicle.Ticket.EntryHour}h");
                        Console.WriteLine($"Heure de sortie : {exitHour}h");
                        Console.WriteLine($"Durée : {duration}h");
                        Console.WriteLine($"Le montant a payer est de : {amount}.- CHF");
                        Console.WriteLine("Voulez vous vraiment sortir ce vehicule? (oui/non)");
                        string confirmation = Console.ReadLine();
                        if (confirmation == "oui")
                        {
                            _vehicles[parkedVehicle.Ticket.ParkingSpot.Index] = null;
                            _occupiedSpots[parkedVehicle.Ticket.ParkingSpot.Index] = false;
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
            Console.WriteLine("Aucun vehicule trouvé");
        }

        /// <summary>
        /// Affiche le taux d'occupation, le plan du parking et les véhicules présents
        /// </summary>
        public void ShowParkingState()
        {
            /* a refaire avec une boucle*/
            int occupiedCount = _vehicles.Count(parkedVehicle => parkedVehicle != null);
            int freeCount = _vehicles.Length - occupiedCount;
            double occupancyRate = occupiedCount / _vehicles.Length * 100;
            Console.WriteLine("=== ETAT DU PARKING ===");
            Console.WriteLine($"Place totales: {_vehicles.Length}");
            Console.WriteLine($"Place occupees: {occupiedCount}");
            Console.WriteLine($"Place libres: {freeCount}");
            Console.WriteLine($"Taux d'occupation {occupancyRate:0.0}%");
            Console.WriteLine("");
            Console.Write("Plan du parking (");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("L");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("=Libre, ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("X");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=Occupé):");
            Console.WriteLine("");

            int spots = 0;
            int spotNumber = 1 + spots;
            while (spots < _vehicles.Length)
            {
                string label = $"0{spotNumber}";
                string letter = "";
                if (spotNumber > 9)
                {
                    label = $"{spotNumber}";
                }
                if (_occupiedSpots[spots] is false)
                {
                    letter = "L";
                }
                if (_occupiedSpots[spots] is true)
                {
                    letter = "X";
                }
                if (letter == "X")
                {
                    Console.Write($"|{label}:");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{letter}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("| ");
                }
                else
                {
                    Console.Write($"|{label}:");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{letter}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("| ");
                }
                Console.ForegroundColor = ConsoleColor.White;
                spots++;
                spotNumber++;
                if (spots % SPOTS_PER_ROW == 0)
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Vehicules presents:");
            foreach (Vehicle parkedVehicle in _vehicles)
            {
                if (parkedVehicle != null)
                {
                    int duration_hours = CURRENT_HOUR - parkedVehicle.Ticket.EntryHour;
                    int duration_minutes = CURRENT_MINUTE - parkedVehicle.Ticket.EntryMinute;
                    Console.WriteLine($"Place : {parkedVehicle.Ticket.ParkingSpot.Index + 1}: plaque : {parkedVehicle.LicensePlate} (depuis {duration_hours}:{duration_minutes} sur le parking)");
                }
            }
            Console.WriteLine("");
        }

        /// <summary>
        /// Recherche un véhicule par plaque ou numéro de place et affiche son ticket
        /// </summary>
        public void SearchVehicle()
        {
            Console.WriteLine("Veuillez entrer la plaque d'immatriculation / place du véhicule à rechercher:");
            string choice = Console.ReadLine();
            foreach (Vehicle parkedVehicle in _vehicles)
            {
                if (parkedVehicle != null)
                {
                    int duration = CURRENT_HOUR - parkedVehicle.Ticket.EntryHour;
                    Console.WriteLine($"le vehicule {parkedVehicle.LicensePlate} se trouve sur le parking et voici le ticket");
                    Console.WriteLine($"Plaque : {parkedVehicle.LicensePlate}");
                    Console.WriteLine($"Heure d'entrée : {parkedVehicle.Ticket.EntryHour}h");
                    Console.WriteLine($"Durée : {duration}h");
                    Console.WriteLine($"Prix actuel : {duration * parkedVehicle.Ticket.HourlyRate}.- CHF");
                    Console.WriteLine($"Place: {parkedVehicle.Ticket.ParkingSpot.Index + 1}");
                }
            }
        }
        public void ShowStatistics()
        {
            //TODO ajouter la durée moyenne et le nombre d'entrées / sorties
        }
        public void ShowTransacitons()
        {
            //TODO ajouter l'historique des transactions
        }
    }
}