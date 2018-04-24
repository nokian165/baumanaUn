using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace baumanaUn.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Введите ваше имя")]
        public string ClientName { get; set; }

        [Display(Name = "Введите мобильный")]
        public string Phone { get; set; }

        [Display(Name = "Введите e-mail")]
        public string Email { get; set; }

        public DateTime OrderCreated { get; set; }

        public virtual Payment Payment { get; set; }

        public Grade Grade { get; set; }

        [Display(Name = "Выберите программу обучения")]
        public int GradeId { get; set; }
    }
} 