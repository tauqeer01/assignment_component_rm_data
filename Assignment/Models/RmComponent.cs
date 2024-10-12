using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class RmComponent
    {
        [Key]
        public int Id { get; set; } // Primary Key

        public string ComponentCode { get; set; } // Component Code
        public string ComponentDescription { get; set; } // Component Description

        // Navigation property - one Component can have many RMs
        public List<RM> RMData { get; set; }
    }
}
