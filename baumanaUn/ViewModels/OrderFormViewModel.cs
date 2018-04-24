using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using baumanaUn.Models;


namespace baumanaUn.ViewModels
{
    public class OrderFormViewModel
    {

        public Order Orders { get; set; }
        public IEnumerable<Grade> Grades { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}