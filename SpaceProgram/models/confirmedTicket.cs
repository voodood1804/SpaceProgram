using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Application.models
{
    public enum Paymentmethod { MasterCard, CreditCard, PayPal, Klarna, SpaceCredit }
    public class ConfirmedTicket : Ticket
    {
        private Paymentmethod _paymentmethod;
        public ConfirmedTicket(Ticket ticket, DateTime paymentDate, Paymentmethod paymentmethod)
            : base(ticket)
        {
            this._paymentmethod = paymentmethod;
            PaymentDate = paymentDate;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected ConfirmedTicket() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public DateTime PaymentDate { get; set; }
        public Paymentmethod Paymentmethod { get; set;}

        public decimal CalculateTotalPrice() => Baggages.Sum(b => b.Price) + Price;
    }
}
