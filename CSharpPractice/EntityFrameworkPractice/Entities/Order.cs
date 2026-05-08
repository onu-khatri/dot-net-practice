using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkPractice.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
