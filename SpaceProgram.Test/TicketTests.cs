using Microsoft.EntityFrameworkCore;
using SpaceProgram.Application.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Test
{
    public class TicketTests : DatabaseTest
    {
        public TicketTests() 
        {
            _db.Database.EnsureCreated();
            _db.Database.EnsureDeleted();
            var person1 = new Passenger(new Person(firstName: "Lukas",
                                                   lastName: "Hammerschmid",
                                                   ssn: 23535423,
                                                   birthDate: new DateTime(2003, 4, 18),
                                                   address: new PlanetAddress("Nowhere", "Infinty", "Stone"),
                                                   tel: "066023135",
                                                   email: "lukas.testmail@gmail.com"));
            _db.Persons.Add(person1);
            _db.SaveChanges();
            var producer1 = new Producer(name: "Omega");
            _db.Producers.Add(producer1);
            _db.SaveChanges();
            var spaceship1 = new Spaceship(name: "X-wing",
                                          producer: producer1,
                                          seats: 100,
                                          maxBaggageWeight: 100);
            _db.Spaceships.Add(spaceship1);
            _db.SaveChanges();
            var organisation1 = new Organisation(name: "Widerstand");
            _db.Organisations.Add(organisation1);
            _db.SaveChanges();
            var solarsystem1 = new SolarSystem(dangerLevel: DangerLevel.Moderate,
                                               name: "VulcanerSystem");
            var solarsystem2 = new SolarSystem(dangerLevel: DangerLevel.Low,
                                               name: "ErdSystem");
            _db.SolarSystems.Add(solarsystem1);
            _db.SolarSystems.Add(solarsystem2);
            _db.SaveChanges();
            var spacestation1 = new SpaceStation(solarsystem: solarsystem1,
                                                 name: "VulcanStation",
                                                 longitude: 1000,
                                                 latitude: 50,
                                                 height: 80000000,
                                                 neglongitude: 0,
                                                 neglatitude: 0,
                                                 negheight: 0);

            var spacestation2 = new SpaceStation(solarsystem: solarsystem1,
                                                 name: "ErdStation",
                                                 longitude: 0,
                                                 latitude: 0,
                                                 height: 5,
                                                 neglongitude: 0,
                                                 neglatitude: 0,
                                                 negheight: 0);
            _db.Spacestations.Add(spacestation1);
            _db.Spacestations.Add(spacestation2);
            _db.SaveChanges();
            var flight1 = new Flight(departureTime: new DateTime(2023, 5, 10),
                                    arrivalTime: new DateTime(2023, 5, 12),
                                    destinationTime: new DateTime(2023, 5, 11),
                                    spaceship: spaceship1,
                                    organisation: organisation1,
                                    spaceStation: spacestation1,
                                    arrivalAddress: spacestation2,
                                    isActive: true);
            _db.Flights.Add(flight1);
            _db.SaveChanges();
            var ticket1 = new Ticket(flight: flight1,
                                     seatNumber: "AB42",
                                     PriorityLevel.BusinessPlus,
                                     price: 999);
            _db.Tickets.Add(ticket1);
            _db.SaveChanges();
            var baggage1 = new Baggage(person1.LastName, 50.21, 40);
            _db.Baggeges.Add(baggage1);
            _db.SaveChanges();
            var confiremedticket1 = new ConfirmedTicket(ticket: ticket1, paymentDate: new DateTime(2023, 10, 5), paymentmethod: Paymentmethod.PayPal);
            confiremedticket1.AddBaggage(baggage1);
            person1.BuyTicket(confiremedticket1);
            _db.SaveChanges();
        }
        [Fact]
        public void AddToHeavyBaggage()
        {
            var bg1 = _db.Tickets.First();
            var p1 = _db.Persons.First();
            var b1 = new Baggage("School Books", 1000, 500);

            Action testAddAction = () => bg1.AddBaggage(b1);
            Assert.Throws<ArgumentException>(testAddAction);
        }
        [Fact]
        public void CountBaggagesSuccessTest()
        {
            Assert.True(_db.Tickets.ToList().First().CountBaggages() == 2);
        }
        [Fact]
        public void CalcTotalPriceSuccess()
        {
            Assert.Equal(1039, _db.confirmedTickets.First().CalculateTotalPrice());
        }
    }
}
