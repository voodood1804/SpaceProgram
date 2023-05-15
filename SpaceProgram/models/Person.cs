using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using SpaceProgram.Application.models;

namespace SpaceProgram.Application.models
{
    public record PlanetAddress(string Planet, string State, string City);
    [Table("Person")]
    public class Person
    {
        public Person(string firstName, string lastName, int ssn, DateTime birthDate, PlanetAddress address, string tel, string email) 
        {
            Guid = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            SSN = ssn;
            BirthDate = birthDate;
            Address = address;
            Tel = tel;
            Email = email;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Person() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        [MaxLength(64)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        [MaxLength(64)]
        public int SSN { get; set; }
        public DateTime BirthDate { get; set; }
        public PlanetAddress Address { get; set; }
        public string Tel { get; set; }
        [MaxLength(64)]
        public string Email { get; set; }
        public string PersonType { get; private set; } = default!;

    }
}
