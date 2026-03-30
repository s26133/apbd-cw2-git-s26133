using ProgramAPBD02.Exceptions;
using ProgramAPBD02.Models;
using ProgramAPBD02.Services;

Console.WriteLine("=== Uczelniana Wypożyczalnia Sprzętu ===");

IPenaltyCalculator penaltyCalc = new StandardPenaltyCalculator();
RentalService system = new RentalService(penaltyCalc);

try
{
    var student = new Student("Jan", "Kowalski");
    var employee = new Employee("Anna", "Nowak");
    system.AddUser(student);
    system.AddUser(employee);

    var laptop = new Laptop("Dell XPS", 16, "i7");
    var projector = new Projector("Epson X", 1920, 3000);
    var camera = new Camera("Sony Alpha", 24, "E-mount");
    system.AddEquipment(laptop);
    system.AddEquipment(projector);
    system.AddEquipment(camera);

    Console.WriteLine("Sprzęt i użytkownicy dodani pomyślnie.");

    system.RentEquipment(student.Id, laptop.Id, 7);
    Console.WriteLine($"\nWypożyczono: {laptop.Name} dla {student.FirstName}");

    try
    {
        Console.WriteLine("\nPróba wypożyczenia niedostępnego sprzętu...");
        system.RentEquipment(employee.Id, laptop.Id, 3);
    }
    catch (BusinessRuleException ex)
    {
        Console.WriteLine($"[BŁĄD ZABLOKOWANY]: {ex.Message}");
    }

    try
    {
        Console.WriteLine("\nPróba przekroczenia limitu przez studenta...");
        system.RentEquipment(student.Id, projector.Id, 2); 
        system.RentEquipment(student.Id, camera.Id, 2);    
    }
    catch (BusinessRuleException ex)
    {
        Console.WriteLine($"[BŁĄD ZABLOKOWANY]: {ex.Message}");
    }

    Console.WriteLine("\nZwrot projektora w terminie...");
    system.ReturnEquipment(projector.Id, DateTime.Now);
    Console.WriteLine("Zwrócono. Brak kary.");

    Console.WriteLine("\nSymulacja opóźnionego zwrotu laptopa (5 dni po terminie)...");
    system.ReturnEquipment(laptop.Id, DateTime.Now.AddDays(12)); 

  
    Console.WriteLine("\n" + system.GenerateSummaryReport());

}
catch (Exception ex)
{
    Console.WriteLine($"Nieoczekiwany błąd systemu: {ex.Message}");
}