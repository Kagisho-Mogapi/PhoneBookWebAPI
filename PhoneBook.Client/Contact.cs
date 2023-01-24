using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Client
{
    internal class Contact
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string? HomeAddress { get; set; }
        public Relationship Relationship { get; set; }


    }

    public enum Relationship
    {
        Friend, Family, CoWorker, Spouse, Acquaintance
    }
}
