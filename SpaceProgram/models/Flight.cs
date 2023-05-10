using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Application.models
{
    [Table("Flight")]
    public class Flight
    {
        public Flight(DateTime departureTime, DateTime arrivalTime, DateTime destinationTime, Spaceship spaceship, Organisation organisation, SpaceStation spaceStation, SpaceStation arrivalAddress, bool isActive) 
        {
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            DestinationTime = destinationTime;
            Spaceship = spaceship;
            SpaceshipId = spaceship.Id;
            Organisation = organisation;
            OrganisationName = organisation.Name;
            DepartureAddressId = spaceStation.Id;
            ArrivalAddressId = spaceStation.Id;
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
        public virtual SpaceStation DepartureAddress { get; set; }
        public int DepartureAddressId { get; set; }
        public virtual SpaceStation ArrivalAddress { get; set; }
        public int ArrivalAddressId { get; set; }

        public bool IsActive { get; set; }
        public void ChangeStatus(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
