using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTravelBooking.Application.models
{
    [Table("Spaceship")]
    public class Spaceship
    {
        public Spaceship(string name, Producer producer, int seats, double maxBaggageWeight) 
        {
            Name = name;
            Producer = producer;
            ProducerName = producer.Name;
            Seats = seats;
            MaxBaggageWeight = maxBaggageWeight;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Spaceship() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public int Id { get; private set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public virtual Producer Producer { get; set; }
        public string ProducerName { get; set; }
        public int Seats { get; set; }
        public double MaxBaggageWeight { get; set; } //per baggage
    }
}
