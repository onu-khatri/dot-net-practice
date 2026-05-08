using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkPractice.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
