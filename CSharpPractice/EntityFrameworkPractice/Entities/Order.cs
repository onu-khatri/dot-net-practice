using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkPractice.Entities
{
    [Table("Orders")]
    [Index(nameof(CustomerId))]
    [Index(nameof(CustomerId), nameof(OrderDate))]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public int ProductId { get; set; }

        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public Customer Customer { get; set; }
    }
}
