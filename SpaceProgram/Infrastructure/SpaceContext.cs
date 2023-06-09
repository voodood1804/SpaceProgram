using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using SpaceProgram.Application.models;
using Bogus.DataSets;
using Microsoft.Extensions.Options;

namespace SpaceProgram.Application.infrastructure;
public class SpaceContext : DbContext
{
    public SpaceContext(DbContextOptions opt) : base(opt) { }

    public DbSet<Baggage> Baggeges => Set<Baggage>();
    public DbSet<ConfirmedTicket> confirmedTickets=> Set<ConfirmedTicket>();
    public DbSet<Crew> Crew => Set<Crew>();
    public DbSet<Flight> Flights => Set<Flight>();
    public DbSet<Organisation> Organisations => Set<Organisation>();
    public DbSet<Passenger> Passengers => Set<Passenger>();
    public DbSet<models.Person> Persons => Set<models.Person>();
    public DbSet<Producer> Producers => Set<Producer>();
    public DbSet<Spaceship> Spaceships => Set<Spaceship>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<SpaceStation> Spacestations => Set<SpaceStation>();
    public DbSet<SolarSystem> SolarSystems => Set<SolarSystem>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<models.Person>().OwnsOne(p => p.Address);

        modelBuilder.Entity<Ticket>().HasAlternateKey(t => t.Guid);
        modelBuilder.Entity<Ticket>().Property(t => t.Guid).ValueGeneratedOnAdd();

        modelBuilder.Entity<Ticket>().HasDiscriminator(t => t.BookingType);
        modelBuilder.Entity<models.Person>().HasDiscriminator(p => p.PersonType);

        modelBuilder.Entity<Flight>()
        .HasOne(f => f.SpaceStationDepature)
        .WithMany()
        .HasForeignKey(f => f.DepartureAddressId);

        modelBuilder.Entity<Flight>()
            .HasOne(f => f.SpaceStationArrival)
            .WithMany()
            .HasForeignKey(f => f.ArrivalAddressId);
    }

    public void Seed()
    {
        Randomizer.Seed = new Random(1337);

        var producer = new Faker<Producer>("en").CustomInstantiator(pd => new Producer(
            name: pd.Vehicle.Manufacturer()))
            .Generate(5)
            .ToList();
        Producers.AddRange(producer);
        SaveChanges();

        var passenger = new Faker<Passenger>("de").CustomInstantiator(p => new Passenger(new models.Person(
            firstName: p.Person.FirstName,
            lastName: p.Person.LastName,
            ssn: p.Random.Int(100000000, 99999999),
            birthDate: p.Person.DateOfBirth,
            address: new PlanetAddress(p.Address.Country(), p.Address.Country(), p.Address.City()),
            tel: p.Phone.PhoneNumber(),
            email: p.Internet.Email())))
            .Generate(15)
            .ToList();
        Persons.AddRange(passenger);
        SaveChanges();

        var organisation = new Faker<Organisation>("de").CustomInstantiator(o => new Organisation(
            name: o.Company.CompanyName()))
            .Generate(15)
            .ToList();
        Organisations.AddRange(organisation);
        SaveChanges();

        var spaceship = new Faker<Spaceship>("de").CustomInstantiator(s => new Spaceship(
            name: s.Vehicle.Model(),
            producer: s.Random.ListItem(producer),
            seats: s.Random.Int(100000, 199999),
            maxBaggageWeight: s.Random.Double(100, 1000)))
            .Generate(15)
            .ToList();
        Spaceships.AddRange(spaceship);
        SaveChanges();

        var solarsystem = new Faker<SolarSystem>("de").CustomInstantiator(sy => new SolarSystem(
            name: sy.Address.Country(),
            dangerLevel: sy.PickRandom<DangerLevel>()
            ))
            .Generate(20)
            .ToList();
        SolarSystems.AddRange(solarsystem);
        SaveChanges();

        var spacestation = new Faker<SpaceStation>("de").CustomInstantiator(sp => new SpaceStation(
            solarsystem: sp.Random.ListItem(solarsystem),
            name: sp.Company.CompanyName(),
            longitude: sp.Random.Int(100000000, 999999999),
            latitude: sp.Random.Int(100000000, 999999999),
            height: sp.Random.Int(100000000, 999999999),
            neglongitude: sp.Random.Int(-100000000, -9999999),
            neglatitude: sp.Random.Int(-100000000, -999999),
            negheight: sp.Random.Int(-100000000, -999999)
            ))
            .Generate(20)
            .ToList();
        Spacestations.AddRange(spacestation);
        SaveChanges();

        var flight = new Faker<Flight>("de").CustomInstantiator(f => new Flight(
            departureTime: f.Person.DateOfBirth,
            arrivalTime: f.Person.DateOfBirth,
            destinationTime: f.Person.DateOfBirth,
            spaceship: f.Random.ListItem(spaceship),
            organisation: f.Random.ListItem(organisation),
            spaceStation: f.Random.ListItem(spacestation),
            arrivalAddress: f.Random.ListItem(spacestation),
            isActive: f.Random.Bool()))
            .Generate(10)
            .ToList();
        Flights.AddRange(flight);
        SaveChanges();

        var crew = new Faker<Crew>("de").CustomInstantiator(c => new Crew(new models.Person(
            firstName: c.Person.FirstName,
            lastName: c.Person.LastName,
            ssn: c.Random.Int(100000000, 99999999),
            birthDate: c.Person.DateOfBirth,
            address: new PlanetAddress(c.Address.Country(), c.Address.Country(), c.Address.City()),
            tel: c.Phone.PhoneNumber(),
            email: c.Internet.Email()),
            organisation: c.Random.ListItem(organisation),
            position: c.Name.JobTitle()))
            .Generate(20)
            .ToList();
        Persons.AddRange(crew);
        SaveChanges();

        //var baggage = new Faker<Baggage>("de").CustomInstantiator(b => new Baggage(
        //    name: b.Person.FirstName,
        //    weight: b.Random.Int(0, 70),
        //    price: b.Random.Int(50, 100))).Generate(2)
    }

}