using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace Shop.Controllers
{
    public class Register : Controller
    {
        private readonly ApplicationDbContext _context;

        // SMTP settings for Brevo
        private readonly string _smtpServer = "smtp-relay.brevo.com";
        private readonly int _port = 587; // Recommended SMTP port for Brevo
        private readonly string _username = "csharpnetframwork@gmail.com"; // Your Brevo email address
        private readonly string _password = "KfpJ6dbQECk0sLS1"; // Your SMTP password or API key for Brevo

        // Constructor
        public Register(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterModel register)
        {
            if (register == null)
            {
                return View();
            }
            else
            {
                // Generate a unique token for the approval link
                string approvalToken = Guid.NewGuid().ToString();

                // Create a new RegisterModel for the user
                RegisterModel model = new RegisterModel()
                {
                    Email = register.Email,
                    Username = register.Username,
                    Password = register.Password,
                    ApprovalToken = approvalToken, // Store the approval token
                    IsApproved = false // Mark the user as unapproved initially
                };

                // Save the user in the database (but don't mark them as approved yet)
                _context.RegisterModel.Add(model);
                await _context.SaveChangesAsync();

                // Email body and subject for the approval request
                string subject = "New User Registration Request";
                string body = $"<h1>User Registration Request</h1>" +
                              $"<p><strong>Email:</strong> {register.Email}</p>" +
                              $"<p><strong>Username:</strong> {register.Username}</p>" +
                              $"<p><strong>Password:</strong> {register.Password}</p>" +
                              $"<p><a href='https://yourdomain.com/Register/Approve/{approvalToken}'>Click here to approve the registration</a></p>";

                // Send the email for approval (to an admin, or whoever is responsible for approval)
                await SendEmailAsync("admin@example.com", subject, body);

                // Show success message
                TempData["Success"] = "Your registration request has been sent for approval.";
                return View();
            }
        }

        // Method to send email via Brevo SMTP
        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // Create the SMTP client using the Brevo SMTP server settings
            var smtpClient = new SmtpClient(_smtpServer, _port)
            {
                Credentials = new NetworkCredential(_username, _password),  // Your email and password
                EnableSsl = true  // Use SSL for secure email transmission
            };

            // Create the email message to send
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_username),  // Sender's email address
                Subject = subject,  // Subject of the email
                Body = body,  // Email body (could be HTML or plain text)
                IsBodyHtml = true  // Specify if the body content is HTML
            };

            mailMessage.To.Add(toEmail);  // Add the recipient email address

            try
            {
                // Send the email asynchronously
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                // Catch any errors that occur during email sending and display the error message
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        // Method for the admin to approve the registration
        [HttpGet]
        public async Task<IActionResult> Approve(string approvalToken)
        {
            if (string.IsNullOrEmpty(approvalToken))
            {
                TempData["Error"] = "Invalid approval token.";
                return RedirectToAction("Index");
            }

            // Find the user by the approval token
            var user = await _context.RegisterModel
                .FirstOrDefaultAsync(u => u.ApprovalToken == approvalToken);

            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Index");
            }

            // Approve the user
            user.IsApproved = true;
            _context.RegisterModel.Update(user);
            await _context.SaveChangesAsync();

            // Show success message
            TempData["Success"] = "User registration approved successfully.";
            return RedirectToAction("Index");
        }
    }
}
