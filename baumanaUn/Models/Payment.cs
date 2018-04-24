using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace baumanaUn.Models
{
    public class Payment
    {
        [ForeignKey("Order")]
        [Display(Name = "Номер заказа")]
        public int Id { get; set; }

        public bool PaymentStatus { get; set; }


        public virtual Order Order { get; set; }
    }
}