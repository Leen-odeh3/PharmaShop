﻿namespace pharmacy.Core.Entities.OrderAggregate;
public class Address : BaseEntity
{
    public Address(string firstName, string lastName, string street, string city, string country)
    {
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        City = city;
        Country = country;
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}