// See https://aka.ms/new-console-template for more information
using P_Parking_Martin;
using System.ComponentModel.Design;
using System.Numerics;
Parking_class class1 = new Parking_class();



if (class1.fif is true)
    class1.Menu();
else
{
    Console.WriteLine("reesayez de rentrer un nombre entre 1 et 3");
    Thread.Sleep(500);
    Console.Clear();
    class1.Menu();
}
Console.WriteLine($"{class1.Parking_Affichee[1]}");
