using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace baumanaUn.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string Location { get; set; }

        public DateTime DateTimeStartCourse { get; set; }

        public Course Course { get; set; }

        public int CourseId { get; set; }

        public int SeatInStock { get; set; }

        public int SeatAvailable { get; set; }

        public int SeatReserve { get; set; }


    }
}