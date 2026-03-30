using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramAPBD02.Models
{
    public abstract class User
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public abstract int MaxActiveRentals { get; }

        protected User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
