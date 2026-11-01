using Microsoft.VisualStudio.TestTools.UnitTesting;
using P_Parking_Martin;

namespace P_Parking_Martin.Tests
{
    [TestClass]
    public class ParkingTests
    {
        [TestMethod]
        public void TestCalculMontantTicket()
        {
            Ticket ticket = new Ticket(0, 10, 13, 1);
            int montant = ticket.CalculMontant();
            Assert.AreEqual(3, montant);
        }

        [TestMethod]
        public void TestCreationVehicule()
        {
            Ticket ticket = new Ticket(1, 8, 10, 1);
            Vehicule vehicule = new Vehicule("VD1234", ticket);
            Assert.AreEqual("VD1234", vehicule.plaque);
            Assert.IsNotNull(vehicule.ticket);
        }

        [TestMethod]
        public void TestPlacesOccupeesInitiales()
        {
            Parking parking = new Parking();
            int places = parking.Places_Occupees();
            Assert.AreEqual(0, places);
        }
    }
}