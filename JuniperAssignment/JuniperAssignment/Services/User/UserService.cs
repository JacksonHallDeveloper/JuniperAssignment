using System.Collections.Generic;
using JuniperAssignment.Models.User;

namespace JuniperAssignment.Services.User
{
    public class UserService : IUserService
    {
        private readonly List<Address> _addresses;
        private readonly Address _primaryAddress;
        private readonly Models.User.User _user;

        public UserService()
        {
            _addresses = new List<Address>
            {
                new()
                {
                    Street1 = "1200 NJ-17",
                    City = " Ramsey",
                    State = "NJ",
                    Country = "US",
                    PostalCode = "07446"
                },
                new()
                {
                    Street1 = "340 Delaware Ave",
                    City = "Delmar",
                    State = "NY",
                    Country = "US",
                    PostalCode = "12054"
                },
                new()
                {
                    Street1 = "Mannerheiminaukio 2",
                    City = "Helenski",
                    Country = "FI",
                    PostalCode = "00150"
                }
            };

            _primaryAddress = _addresses[0];

            _user = new Models.User.User
            {
                Name = "John Doe",
                Address = _primaryAddress,
                Addresses = _addresses
            };
        }

        public Models.User.User GetCurrentUser()
        {
            return _user;
        }

        public void SetCurrentAddress(Address address)
        {
            _user.Address = address;
        }
    }
}