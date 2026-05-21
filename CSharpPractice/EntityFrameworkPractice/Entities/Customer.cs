using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkPractice.Entities
{
    [Table("Customers")]
    [Index(nameof(Email), IsUnique = true)]
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]        
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [Unicode(false)]
        [Column("EmailAddress")]
        public string Email { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
