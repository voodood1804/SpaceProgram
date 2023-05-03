using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTravelBooking.Application.models
{
    [Table("Planet")]
    public class Planet
    {
        public Planet(int planetId, string name) 
        {
            PlanetId = planetId;
            Name = name;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanetId { get; set;} 
        public string Name { get; set; }
    }
    public record class Address(Planet Planet, string State, string City);
}

