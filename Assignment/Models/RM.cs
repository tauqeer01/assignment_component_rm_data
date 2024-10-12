namespace Assignment.Models
{
    public class RM
    {
        public int Id { get; set; } // Primary Key
        public string RMName { get; set; } // Name of the RM
        public decimal RMRate { get; set; } // Rate of the RM

        // Foreign key to link to the Component
        public int ComponentId { get; set; } // Foreign Key
        public RmComponent Component { get; set; } // Navigation property for the associated component
    }
}
