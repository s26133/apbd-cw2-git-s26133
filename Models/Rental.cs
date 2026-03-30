using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramAPBD02.Models
{
    public class Rental
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid EquipmentId { get; set; }
        public Guid UserId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal PenaltyFee { get; set; }

        public bool IsOverdue => !ReturnDate.HasValue && DateTime.Now > DueDate;
    }
}
