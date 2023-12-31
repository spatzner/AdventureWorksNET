﻿using System.Security.AccessControl;

namespace AdventureWorks.SqlRepository.DTO
{
    internal class Address
    {
        public int AddressId { get; set; }
        public string? AddressType { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}