using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using baumanaUn.Models;
using baumanaUn.ViewModels;
using System.Data.Entity;
using System.Globalization;


namespace baumanaUn.Controllers
{
    public class LessonController : Controller
    {
        private ApplicationDbContext _context;

        public LessonController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Index----------------------------------------------------
        public ActionResult Order()
        {
            var orders = _context.Orders.ToList();

            return View(orders);
        }

        public ActionResult Course()
        {
            var courses = _context.Courses.ToList();

            return View(courses);
        }

        public ActionResult Payment()
        {
            var payments = _context.Payments.ToList();

            return View(payments);
        }

        public ActionResult Grade()
        {
            var grades = _context.Grades.ToList();

            return View(grades);
        }

        public ViewResult Manager()
        {
            var mainTables = _context.Grades
                .Include(m => m.Course)
                .ToList();

            return View(mainTables);
        }

        public ActionResult Success()
        {
            return View();
        }

        // New -------------------------------------------------------

        public ActionResult OrderNew()
        {

            var grades = _context.Grades.ToList();
            var courses = _context.Courses.ToList();
            var viewModel = new OrderFormViewModel
            {
                Orders = new Order(),
                Grades = grades,
                Courses = courses
            };

            return View("OrderForm", viewModel);
        }

        public ActionResult PaymentNew()
        {

            var viewModel = new Payment();


            return View("PaymentForm", viewModel);
        }


        // Save -----------------------------------------------------

        [HttpPost]
        public ActionResult OrderSave(OrderFormViewModel orderFormViewModel)
        {
            // Выбрать ближайшее занятие по дате по выбранному курсу c условием, что есть свободные места в группе
            var gradeInDb = _context.Grades
                .Where(b => b.DateTimeStartCourse > DateTime.Now)
                .FirstOrDefault(c => c.CourseId == orderFormViewModel.Orders.GradeId && c.SeatAvailable > 0);

            if (gradeInDb != null)
            {
                gradeInDb.SeatAvailable -= 1;
                gradeInDb.SeatReserve += 1;

                var viewModel = new Order()
                {
                    ClientName = orderFormViewModel.Orders.ClientName,
                    Phone = orderFormViewModel.Orders.Phone,
                    Email = orderFormViewModel.Orders.Email,
                    OrderCreated = DateTime.Now,
                    GradeId = gradeInDb.Id
                };

                _context.Orders.Add(viewModel);
                _context.SaveChanges();

                // прочитать вновь созданную строку в базе заказов
                var orderInDb = _context.Orders.Single(c => c.Id == viewModel.Id);

                // инициализировать запись в базе Оплаты (не оплачено) согласно заказу
                var viewModelPayment = new Payment
                {
                    Id = orderInDb.Id,
                    PaymentStatus = false
                };

                _context.Payments.Add(viewModelPayment);
                _context.SaveChanges();

                // перенаправление на страницу оплаты данного заказа
                return View("PaymentForm", viewModelPayment);

            }
            else
                return RedirectToAction("OrderNew", "Lesson");
        }

        [HttpPost]
        public ActionResult PaymentSave(Payment payment)
        {
            var paymentInDb = _context.Payments.SingleOrDefault(c => c.Id == payment.Id);

            
            if (paymentInDb != null)
            {
                // Успешное прохождение оплаты
                paymentInDb.PaymentStatus = true;
                
                // Cнятие с резерва места в классе
                var ordersInDb = _context.Orders.Single(g => g.Id == paymentInDb.Id);
                var gradeInDb = _context.Grades.Single(g => g.Id == ordersInDb.GradeId);
                gradeInDb.SeatReserve -= 1;

                _context.SaveChanges();

                return RedirectToAction("Success", "Lesson");
            }
            else
            {
                var viewModel = new Payment
                {
                    PaymentStatus = payment.PaymentStatus,
                    Id = 0
                };

                return View("PaymentForm", viewModel);
            }

        }

    }
}