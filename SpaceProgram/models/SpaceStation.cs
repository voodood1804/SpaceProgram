using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTravelBooking.Application.models
{
    [Table("SpaceStation")]
    public class SpaceStation
    {
        public SpaceStation(int stationId, string name) 
        {
            StationId = stationId;
            Name = name;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StationId { get; set; }
        public string Name { get; set; }
    }
    public record class StationAddress(SpaceStation SpaceStation, int SolarSystem);
}
