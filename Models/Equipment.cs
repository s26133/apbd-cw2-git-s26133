using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramAPBD02.Models
{
    public abstract class Equipment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;

        protected Equipment(string name) { Name = name; }
    }
}
