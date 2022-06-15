using FaceDetection.Messaging.InterfacesConstants.Commands;
using FaceDetection.Messaging.InterfacesConstants.Constants;
using FaceDetection.MVC.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FaceDetection.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusControl busControl;

        public HomeController(ILogger<HomeController> logger, IBusControl busControl)
        {
            _logger = logger;
            this.busControl = busControl;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterOrder(OrderViewModel model)
        {
            using var memoryStream = new MemoryStream();
            using var uploadedFile = model.File.OpenReadStream();
            await uploadedFile.CopyToAsync(memoryStream);

            if(model.OrderId == Guid.Empty)
            {
                model.OrderId = Guid.NewGuid();
            }
            model.ImageData = memoryStream.ToArray();
            model.ImageUrl = model.File.FileName;

            var sendToUri = new Uri($"{RabbitMQMassTransitConstants.RabbitMQUri}{RabbitMQMassTransitConstants.RegisterOrderCommandQueue}");
            var endpoint = await busControl.GetSendEndpoint(sendToUri);
            await endpoint.Send<IRegisterOrderCommand>(new 
            { 
                model.OrderId,
                model.UserEmail, 
                model.ImageData, 
                model.ImageUrl
            });


            return View("Thanks");
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
