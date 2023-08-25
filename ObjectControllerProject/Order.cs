using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderControllerProject
{
    public class Order
    {
        public int Id { get; set; } 
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Id,2:N0} | {CustomerId,2:N0} | {Date,4:d} | {Description}";
        }
    }
}
