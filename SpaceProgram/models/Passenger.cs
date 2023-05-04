using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Application.models
{
    public class Passenger : Person
    {
        public Passenger(Person p)
            : base(p.FirstName, p.LastName, p.SSN, p.BirthDate, p.Address, p.Tel, p.Email) { }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Passenger() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected List<Ticket> _tickets = new();
        public virtual IReadOnlyCollection<Ticket> Tickets => _tickets;
        public void BuyTicket(Ticket t)
        {
            if (t != null)
            {
                if (t.DateOfBooking < t.Flight.ArrivalTime)
                {
                    if (t.Flight.IsActive == true)
                        _tickets.Add(t);
                }
                else
                {
                    throw new Exception("Buying a Ticket from the selected flight is not possible anymore");
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public void CancelTicket(Ticket t)
        {
            if (t != null)
            {
                _tickets.Remove(t);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public int CountTickets()
        { 
            return _tickets.Count; 
        }

        public void ConfirmTicket(Ticket t, DateTime paymentDate, Paymentmethod paymentmethod)
        {
            if (t is ConfirmedTicket)
                return;

            var confirmedTicket = new ConfirmedTicket(t, paymentDate, paymentmethod);
            _tickets.Remove(t);
            _tickets.Add(confirmedTicket);
        }
    }
}
