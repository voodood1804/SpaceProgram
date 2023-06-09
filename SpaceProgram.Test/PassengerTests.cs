using SpaceProgram.Application.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Test
{
    public class PassengerTests : DatabaseTest
    {
        public PassengerTests() 
        {
            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();

            var person1 = new Passenger(new Person(firstName: "Lukas",
                                                   lastName: "Hammerschmid",
                                                   ssn: 23535423,
                                                   birthDate: new DateTime(2003, 4, 18),
                                                   address: new PlanetAddress("Nowhere", "Infinty", "Stone"),
                                                   tel: "066023135",
                                                   email: "lukas.testmail@gmail.com"));
            var person2 = new Passenger(new Person(firstName: "Lukas",
                                                   lastName: "Hammerschmid",
                                                   ssn: 23535423,
                                                   birthDate: new DateTime(2000, 5, 20),
                                                   address: new PlanetAddress("Nowhere", "Infinty", "Stone"),
                                                   tel: "238325545",
                                                   email: "HAM.testmail@gmail.com"));
            _db.Persons.Add(person1);
            _db.Persons.Add(person2);
            _db.SaveChanges();

            var producer1 = new Producer(name: "Atlas");
            _db.Producers.Add(producer1);
            _db.SaveChanges();
            var spaceship1 = new Spaceship(name: "Sternzerstörer",
                                          producer: producer1,
                                          seats: 10000,
                                          maxBaggageWeight: 200);
            _db.Spaceships.Add(spaceship1);
            _db.SaveChanges();
            var organisation1 = new Organisation(name: "Imperium");
            _db.Organisations.Add(organisation1);
            _db.SaveChanges();
            var solarsystem1 = new SolarSystem(dangerLevel: DangerLevel.Moderate,
                                               name: "EwokSystem");
            var solarsystem2 = new SolarSystem(dangerLevel: DangerLevel.Low,
                                               name: "NabooSystem");
            _db.SolarSystems.Add(solarsystem1);
            _db.SolarSystems.Add(solarsystem2);
            _db.SaveChanges();
            var spacestation1 = new SpaceStation(solarsystem: solarsystem1,
                                                 name: "EwokStation",
                                                 longitude: 1000000,
                                                 latitude: 53250,
                                                 height: 0,
                                                 neglongitude: 0,
                                                 neglatitude: 0,
                                                 negheight: -324698);

            var spacestation2 = new SpaceStation(solarsystem: solarsystem1,
                                                 name: "NabooStation",
                                                 longitude: 0,
                                                 latitude: 0,
                                                 height: 0,
                                                 neglongitude: -325436,
                                                 neglatitude: -435269,
                                                 negheight: -35893);
            _db.Spacestations.Add(spacestation1);
            _db.Spacestations.Add(spacestation2);
            _db.SaveChanges();
            var flight1 = new Flight(departureTime: new DateTime(2023, 5, 15),
                                    arrivalTime: new DateTime(2023, 5, 17),
                                    destinationTime: new DateTime(2023, 5, 16),
                                    spaceship: spaceship1,
                                    organisation: organisation1,
                                    spaceStation: spacestation1,
                                    arrivalAddress: spacestation2,
                                    isActive: true);
            var flight2 = new Flight(departureTime: new DateTime(2023, 5, 15),
                                    arrivalTime: new DateTime(2023, 5, 17),
                                    destinationTime: new DateTime(2023, 5, 16),
                                    spaceship: spaceship1,
                                    organisation: organisation1,
                                    spaceStation: spacestation1,
                                    arrivalAddress: spacestation2,
                                    isActive: false);

            _db.Flights.Add(flight1);
            _db.Flights.Add(flight2);
            _db.SaveChanges();

            var confiremedticket1 = new ConfirmedTicket(ticket: new Ticket(
                                                         flight: flight1,
                                                         passenger: person1,
                                                         seatNumber: "AB42",
                                                         PriorityLevel.BusinessPlus,
                                                         price: 999),
                                                        paymentDate: new DateTime(2022, 10, 5),
                                                        paymentmethod: Paymentmethod.PayPal);

            var confiremedticket2 = new ConfirmedTicket(ticket: new Ticket(
                                                         flight: flight2,
                                                         passenger: person2,
                                                         seatNumber: "AB42",
                                                         PriorityLevel.BusinessPlus,
                                                         price: 999),
                                                        paymentDate: new DateTime(2023, 10, 5),
                                                        paymentmethod: Paymentmethod.PayPal);

            _db.SaveChanges();
            person1.BuyTicket(confiremedticket1);
            person2.BuyTicket(confiremedticket2);
            _db.SaveChanges();
        }
        [Fact]
        public void SetDataSuccessTest()
        {
            Assert.True(_db.Persons.Count()  == 2);
            Assert.True(_db.Producers.Count() == 1);
            Assert.True(_db.Spaceships.Count() == 1);
            Assert.True(_db.Organisations.Count() == 1);
            Assert.True(_db.SolarSystems.Count() == 2);
            Assert.True(_db.Spacestations.Count() == 2);
            Assert.True(_db.Flights.Count() == 2);
        }

        [Fact]
        public void AddActiveFlight()
        {
            Assert.True(_db.Passengers.ToList().First().CountTickets() == 1);
        }

        [Fact]
        public void AddCanceledFlight()
        {
            Assert.True(_db.Passengers.ToList().Last().CountTickets() == 0);
        }

        [Fact]
        public void CancelFlightSuccessTest()
        {
            var pes1 = _db.Passengers.First();
            pes1.CancelTicket(_db.Passengers.ToList().First().Tickets.First());

            Assert.True(_db.Passengers.ToList().First().CountTickets() == 0);
        }

        [Fact]
        public void ConfirmTicketSuccessTest()
        {
            var pes1 = _db.Passengers.First();
            pes1.ConfirmTicket(pes1.Tickets.First(), DateTime.Now, Paymentmethod.PayPal);

            Assert.True(_db.Passengers.ToList().First().CountTickets() == 1);
        }
    }
}
