using Microsoft.AspNetCore.Mvc;
using weegci.models;
using weegci.Models;
using weegci.services;
using weegci.services.interfaces;

namespace weegci.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailService _emailService;

        public ContactController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(ContactFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _emailService.SendEmailAsync(model);
                if (result.IsSucceeded)
                {
                    ViewBag.Success = "Your message has been sent successfully!";
                    ModelState.Clear();
                    TempData["Success"] = "Your message has been sent successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = $"There was an error sending your message. Please see below and try again later. \n{result.Message}";
                    return RedirectToAction("Index");
                }
            }
            var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();


            TempData["Error"] = string.Join("\n", errors);
            return RedirectToAction("Index");
        }

    }
}
