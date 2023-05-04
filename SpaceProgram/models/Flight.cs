using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Application.models
{
    public record class StationAddress(int SolarSystem, string name);
    [Table("Flight")]
    public class Flight
    {
        public Flight(DateTime departureTime, DateTime arrivalTime, DateTime destinationTime, Spaceship spaceship, Organisation organisation, StationAddress departureAddress, StationAddress arrivalAddress, bool isActive) 
        {
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            DestinationTime = destinationTime;
            Spaceship = spaceship;
            SpaceshipId = spaceship.Id;
            Organisation = organisation;
            OrganisationName = organisation.Name;
            DepartureAddress = departureAddress;
            ArrivalAddress = arrivalAddress;
            IsActive = isActive;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Flight() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        public int Id { get; private set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DestinationTime { get; set; }
        public virtual Spaceship Spaceship { get; set; }
        public int SpaceshipId { get; set; }
        public virtual Organisation Organisation { get; set; }
        public string OrganisationName { get; set; }
        public StationAddress DepartureAddress { get; set; }
        public StationAddress ArrivalAddress { get; set; }
        public bool IsActive { get; set; }
        public void ChangeStatus(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
