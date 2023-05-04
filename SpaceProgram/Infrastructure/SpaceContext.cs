using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using SpaceProgram.Application.models;
using Bogus.DataSets;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<models.Person>().OwnsOne(p => p.Address);
        modelBuilder.Entity<models.Flight>().OwnsOne(f2 => f2.ArrivalAddress);
        modelBuilder.Entity<models.Flight>().OwnsOne(f2 => f2.DepartureAddress);

        modelBuilder.Entity<Ticket>().HasAlternateKey(t => t.Guid);
        modelBuilder.Entity<Ticket>().Property(t => t.Guid).ValueGeneratedOnAdd();

        modelBuilder.Entity<Ticket>().HasDiscriminator(t => t.BookingType);
        modelBuilder.Entity<models.Person>().HasDiscriminator(p => p.PersonType);
    }

    public void Seed()
    {
        Randomizer.Seed = new Random(1337);

        var producer = new Faker<Producer>("AUT").CustomInstantiator(pd => new Producer(
            name: pd.Vehicle.Manufacturer()))
            .Generate(15)
            .ToList();
        Producers.AddRange(producer);
        SaveChanges();

        var passenger = new Faker<Passenger>("AUT").CustomInstantiator(p => new Passenger(new models.Person(
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

        var organisation = new Faker<Organisation>("AUT").CustomInstantiator(o => new Organisation(
            name: o.Company.CompanyName()))
            .Generate(15)
            .ToList();
        Organisations.AddRange(organisation);
        SaveChanges();

        var spaceship = new Faker<Spaceship>("AUT").CustomInstantiator(s => new Spaceship(
            name: s.Vehicle.Model(),
            producer: s.Random.ListItem(producer),
            seats: s.Random.Int(100000, 199999),
            maxBaggageWeight: s.Random.Double(100, 1000)))
            .Generate(15)
            .ToList();
        Spaceships.AddRange(spaceship);
        SaveChanges();

        var flight = new Faker<Flight>("AUT").CustomInstantiator(f => new Flight(
            departureTime: f.Person.DateOfBirth,
            arrivalTime: f.Person.DateOfBirth,
            destinationTime: f.Person.DateOfBirth,
            spaceship: f.Random.ListItem(spaceship),
            organisation: f.Random.ListItem(organisation),
            departureAddress: new StationAddress(f.Random.Int(100000, 999999), f.Address.City()),
            arrivalAddress: new StationAddress(f.Random.Int(100000, 999999), f.Address.City()),
            isActive: f.Random.Bool()))
            .Generate(10)
            .ToList();
        Flights.AddRange(flight);
        SaveChanges();

        var crew = new Faker<Crew>("AUT").CustomInstantiator(c => new Crew(new models.Person(
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
    }

}