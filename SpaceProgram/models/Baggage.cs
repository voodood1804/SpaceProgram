using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Application.models
{
    [Table("Baggage")]
    public class Baggage
    {
        public Baggage(Person person, double weight, decimal price)
        {
            Person = person;
            PersonFirstname = person.FirstName;
            PersonLastname = person.LastName;
            Weight = weight;
            Price = price;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Baggage() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public int Id { get; private set; }
        [MaxLength(64)]
        public Person Person { get; set; }
        public string PersonFirstname { get; set; }
        public string PersonLastname { get; set; }
        public double Weight { get; set; }
        public decimal Price { get; set; }
    }
}
