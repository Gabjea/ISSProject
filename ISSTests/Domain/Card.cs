using System;

namespace App.Domain
{
    public class Card : Payment
    {
        private string cardNumber { get; set; } = string.Empty;
        private string cvv { get; set; } = string.Empty;
        private string expirationDate { get; set; } = string.Empty;

        public Card(string cardNumber, string cvv, string expirationDate) : base()
        {
            this.cardNumber = cardNumber;
            this.cvv = cvv;
            this.expirationDate = expirationDate;
        }

        public Card(int id, int balance, string cardNumber, string cvv, string expirationDate) : base(id, balance)
        {
            this.cardNumber = cardNumber;
            this.cvv = cvv;
            this.expirationDate = expirationDate;
        }

    }

}