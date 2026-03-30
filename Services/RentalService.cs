using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramAPBD02.Services
{
    using ProgramAPBD02.Exceptions;
    using ProgramAPBD02.Models;

    public class RentalService
    {
        private readonly List<Equipment> _equipment = new();
        private readonly List<User> _users = new();
        private readonly List<Rental> _rentals = new();
        private readonly IPenaltyCalculator _penaltyCalculator;

        public RentalService(IPenaltyCalculator penaltyCalculator)
        {
            _penaltyCalculator = penaltyCalculator;
        }

        public void AddUser(User user) => _users.Add(user);
        public void AddEquipment(Equipment item) => _equipment.Add(item);

        public IEnumerable<Equipment> GetAllEquipment() => _equipment;
        public IEnumerable<Equipment> GetAvailableEquipment() => _equipment.Where(e => e.Status == EquipmentStatus.Available);

        public void MarkAsMaintenance(Guid equipmentId)
        {
            var eq = _equipment.FirstOrDefault(e => e.Id == equipmentId);
            if (eq != null) eq.Status = EquipmentStatus.Maintenance;
        }

        public void RentEquipment(Guid userId, Guid equipmentId, int days)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId) ?? throw new BusinessRuleException("Nie znaleziono użytkownika.");
            var eq = _equipment.FirstOrDefault(e => e.Id == equipmentId) ?? throw new BusinessRuleException("Nie znaleziono sprzętu.");

            if (eq.Status != EquipmentStatus.Available)
                throw new BusinessRuleException($"Sprzęt {eq.Name} nie jest dostępny.");

            var activeRentalsCount = _rentals.Count(r => r.UserId == userId && r.ReturnDate == null);
            if (activeRentalsCount >= user.MaxActiveRentals)
                throw new BusinessRuleException($"Użytkownik {user.FirstName} przekroczył limit wypożyczeń ({user.MaxActiveRentals}).");

            eq.Status = EquipmentStatus.Rented;
            _rentals.Add(new Rental
            {
                UserId = userId,
                EquipmentId = equipmentId,
                RentDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(days)
            });
        }

        public void ReturnEquipment(Guid equipmentId, DateTime returnDate)
        {
            var rental = _rentals.FirstOrDefault(r => r.EquipmentId == equipmentId && r.ReturnDate == null)
                ?? throw new BusinessRuleException("To urządzenie nie jest obecnie wypożyczone.");

            var eq = _equipment.First(e => e.Id == equipmentId);

            rental.ReturnDate = returnDate;
            rental.PenaltyFee = _penaltyCalculator.CalculatePenalty(rental.DueDate, returnDate);
            eq.Status = EquipmentStatus.Available;
        }

        public IEnumerable<Rental> GetActiveRentalsForUser(Guid userId) =>
    _rentals.Where(r => r.UserId == userId && r.ReturnDate == null);

        public IEnumerable<Rental> GetOverdueRentals() =>
            _rentals.Where(r => r.IsOverdue);

        public string GenerateSummaryReport()
        {
            var total = _equipment.Count;
            var available = _equipment.Count(e => e.Status == EquipmentStatus.Available);
            var rented = _equipment.Count(e => e.Status == EquipmentStatus.Rented);
            var maintenance = _equipment.Count(e => e.Status == EquipmentStatus.Maintenance);
            var overdue = GetOverdueRentals().Count();

            return $"Raport Systemu:\n- Całkowity sprzęt: {total}\n- Dostępny: {available}\n- Wypożyczony: {rented}\n- W serwisie: {maintenance}\n- Przeterminowane zwroty: {overdue}";
        }
    }
    }
