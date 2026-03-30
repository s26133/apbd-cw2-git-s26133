using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramAPBD01.Models
{
    public class Student : User
    {
        public override int MaxActiveRentals => 2;
        public Student(string firstName, string lastName) : base(firstName, lastName) { }
    }

    public class Employee : User
    {
        public override int MaxActiveRentals => 5;
        public Employee(string firstName, string lastName) : base(firstName, lastName) { }
    }
}
