using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Application.models
{
    public class Crew : Person
    {
        public Crew(Person p, Organisation organisation, string position)
            : base(p.FirstName, p.LastName, p.SSN, p.BirthDate, p.Address, p.Tel, p.Email)
        {
            Organisation = organisation;
            OrganisationName = organisation.Name;
            Position = position;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Crew() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        public virtual Organisation Organisation { get; set; }
        public string OrganisationName { get; set; }
        public string Position { get; set; }
    }
}
