using DocumentVerification.Enums;
using System;

namespace DocumentVerification.Models
{
    public class Document
    {
        public DocumentType DocumentType { get; set; }

        public State State { get; set; }

        public string Name { get; set; }

        public string CardNumber { get; set; }

        public string Address { get; set; }

        public string LicenceNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
