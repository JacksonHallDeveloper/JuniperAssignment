using System.Collections.Generic;

namespace JuniperAssignment.Models.User
{
    public class User
    {
        public string Name { get; set; }
        public Address Address { get; set; } = new();
        public List<Address> Addresses { get; set; } = new();
    }
}