﻿using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace FaceDetection.MVC.Models
{
    public class OrderViewModel
    {
        [Display(Name="Order Id")]
        public Guid OrderId { get; set; }

        [Display(Name ="Email")]
        public string UserEmail { get; set; }

        public IFormFile File { get; set; }

        [Display(Name ="Image URL")]
        public string ImageUrl { get; set; }

        [Display(Name = "Order Status")]
        public string StatusString { get; set; }

        public byte[] ImageData { get; set; }

    }
}