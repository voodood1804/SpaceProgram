using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Application.models
{
    [Table("Organisation")]
    public class Organisation
    {
        public Organisation(string name)
        {

            Name = name;
        }

        public int Id { get; private set; }
        [MaxLength(64)]
        public string Name { get; set; }
    }
}
