using SpaceProgram.Application.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Test
{
    public class FlightTests : DatabaseTest
    {
        public FlightTests()
        {
            _db.Database.EnsureCreated();
            _db.Database.EnsureCreated();

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
            var flight1 = new Flight(departureTime: new DateTime(2023, 5, 15),
                                    arrivalTime: new DateTime(2023, 5, 17),
                                    destinationTime: new DateTime(2023, 5, 16),
                                    spaceship: spaceship1,
                                    organisation: organisation1,
                                    departureAddress: new StationAddress(42, "PegasusSystem"),
                                    arrivalAddress: new StationAddress(1, "ErdSystem"),
                                    isActive: true);
            flight1.ChangeStatus(false);
            _db.Flights.Add(flight1);
            _db.SaveChanges();
        }

        [Fact]
        public void ChangeStatus()
        {
            Assert.True(_db.Flights.First().IsActive == false);
        }
    }
}
