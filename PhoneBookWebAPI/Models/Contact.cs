using System.ComponentModel.DataAnnotations;

namespace PhoneBookWebAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
        public string? HomeAddress { get; set; }
        public Relationship Relationship { get; set; }


    }

    public enum Relationship
    {
        Friend, Family, CoWorker, Spouse, Acquaintance
    }
}
