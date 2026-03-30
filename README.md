# apbd-cw2-git-s26133
# Wypożyczalnia Sprzętu Uczelnianego

## Jak odpalić?
Wystarczy wejść do folderu z projektem i wpisać w konsoli:
`dotnet run`
Program sam wykona scenariusz testowy pokazujący dodawanie, wypożyczanie, błędy i naliczanie kar.

## Dlaczego tak to napisałem?

Podzieliłem kod na foldery Models, Services i Exceptions, żeby nie trzymać wszystkiego w jednym pliku Program.cs i zachować porządek.

* SRP (Odpowiedzialność): Modele nie mają w sobie żadnych Console.WriteLine ani logiki sprawdzania warunków. Całą czarną robotę robi `RentalService`. Klasa Program służy tylko do wyświetlania.
* Dziedziczenie: Użyłem go tam, gdzie miało sens w domenie. Sprzęt dziedziczy po `Equipment`, a użytkownicy po `User`. Dzięki temu nie muszę pisać w serwisie wielkich instrukcji if/else (np. `if (typ == "Student") limit = 2`), tylko sprawdzam nadpisaną właściwość `MaxActiveRentals`.
* Niski Coupling: Kalkulator kar nie jest wklejony w kod wypożyczalni. Użyłem interfejsu `IPenaltyCalculator`. Jak regulamin się zmieni, to dopiszę nową klasę.
* Kohezja: `RentalService` zajmuje się operacjami na wypożyczeniach. 
* Obsługa błędów: Zrobiłem `BusinessRuleException`. Jak ktoś chce wypożyczyć zajęty sprzęt, serwisrzuca wyjątek, a konsola wyświetla ładny komunikat.