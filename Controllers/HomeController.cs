using Microsoft.AspNetCore.Mvc;
using Porfolio_Satendra.Data;
using Porfolio_Satendra.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace Porfolio_Satendra.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserDBContext _dbContext;

        public HomeController(ILogger<HomeController> logger, UserDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(UserInfo user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Users.Add(user); 
                    _dbContext.SaveChanges();

                    SmtpClient SmtpServer = new SmtpClient();
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Credentials = new NetworkCredential("satendray019@gmail.com", "YourPassword"); // Replace with your actual credentials
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.Port = 587;
                    SmtpServer.Host = "smtp.gmail.com"; // You can use the appropriate SMTP host

                    // Create the main email message
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("satendray019@gmail.com");
                    mail.To.Add("recipient@example.com"); // Replace with the recipient's email address
                    mail.Subject = "Thank You for Contacting Us";
                    mail.Body = "Dear Customer,\n\nThank you for reaching out to us. We appreciate your interest and would like to assure you that we are working diligently to respond to your inquiry.\n\nBest regards,\nSATENDRA YADAV";

                    // Create a separate email message for CC recipients
                    MailMessage ccMail = new MailMessage();
                    ccMail.From = new MailAddress("satendray019@gmail.com");
                    ccMail.To.Add("satenyaar@gmail.com"); // Replace with the CC recipient's email address
                    //ccMail.CC.Add("ccrecipient@example.com"); // Replace with another CC recipient's email address
                    ccMail.Subject = "User Contact Details";
                    ccMail.Body = "Dear Team,\n\nA user has contacted us with the following details:\n\n";
                    ccMail.Body += "User Name: " + user.Name.ToUpper() + "\n";
                    ccMail.Body += "User Email: " + user.Email + "\n";
                    ccMail.Body += "Mobile Number: " + user.Mobilenum + "\n\n";
                    ccMail.Body += "Please take appropriate action and respond to the user promptly.\n\nBest regards,\nSATENDRA YADAV";

                    // Send both email messages
                    SmtpServer.Send(mail);
                    SmtpServer.Send(ccMail);




                    TempData["SuccessMessage"] = "User Information has been successfully added.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while adding the user. "+ex.Message+"";
                }

                return RedirectToAction("Success");
            }
            else
            {
              TempData["ErrorMessage"] = "An error occurred while adding the user.";

                return View(user); 
            }
        }

        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}