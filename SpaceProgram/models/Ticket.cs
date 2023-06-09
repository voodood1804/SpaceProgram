using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Application.models
{
    public enum PriorityLevel { Economy, EconomyPlus, Business, BusinessPlus, First}
    [Table("Ticket")]
    public class Ticket
    {
        private PriorityLevel _priorityLevel;

        public Ticket(Flight flight, Passenger passenger, string seatNumber, PriorityLevel priorityLevel, decimal price) 
        {
            Passenger = passenger;
            Flight = flight;
            FlightId = flight.Id;
            SeatNumber = seatNumber;
            this._priorityLevel = priorityLevel;
            DateOfBooking = DateTime.Now;
            Price = price;
            Guid = Guid.NewGuid();
        }

        protected Ticket(Ticket t) 
        {
            Id = t.Id;
            Passenger = t.Passenger;
            PassengerId = t.PassengerId;
            Flight = t.Flight;
            FlightId = t.FlightId;
            SeatNumber = t.SeatNumber;
            PriorityLevel = t.PriorityLevel;
            Price = t.Price;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Ticket() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public int PassengerId { get; private set; }
        public virtual Passenger Passenger { get; private set; }
        public virtual Flight Flight { get; set; }
        public int FlightId { get; set; }
        public string SeatNumber { get; set; }
        public PriorityLevel PriorityLevel { get; set; }
        public DateTime DateOfBooking { get; }
        public decimal Price { get; set; }
        public string BookingType { get; private set; } = default!;
        protected List<Baggage> _baggages = new();
        public virtual IReadOnlyCollection<Baggage> Baggages => _baggages;

        public void AddBaggage(Baggage baggage)
        {
            if (baggage != null)
            {
                if (baggage.Weight > 0 && baggage.Weight < this.Flight.Spaceship.MaxBaggageWeight)
                {
                    _baggages.Add(baggage);
                }
                else
                {
                    throw new ArgumentException("Weight of baggage does not meet the requirements");
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }
        public int CountBaggages()
        {
            return _baggages.Count;
        }
    }
}
