namespace Lab.Entities
{
    public class Employee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Role Role { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"{nameof(LastName)}: {LastName}, {nameof(FirstName)}: {FirstName}, {nameof(Role)}: {Role}, {nameof(Age)}: {Age}";
        }
    }
}