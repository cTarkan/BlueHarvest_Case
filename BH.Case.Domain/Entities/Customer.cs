namespace BH.Case.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        // Parameterless constructor for EF Core
        public Customer() { }

        public Customer(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
} 