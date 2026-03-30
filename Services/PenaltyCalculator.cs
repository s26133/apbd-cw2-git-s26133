using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramAPBD01.Services
{
    public interface IPenaltyCalculator
    {
        decimal CalculatePenalty(DateTime dueDate, DateTime returnDate);
    }

    public class StandardPenaltyCalculator : IPenaltyCalculator
    {
        public decimal CalculatePenalty(DateTime dueDate, DateTime returnDate)
        {
            if (returnDate <= dueDate) return 0m;
            return (returnDate - dueDate).Days * 10m;
        }
    }
}
