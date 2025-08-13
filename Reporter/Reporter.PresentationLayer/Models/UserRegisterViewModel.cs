using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reporter.EntityLayer.Entities;

namespace Reporter.PresentationLayer.Models
{
    public class UserRegisterViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
    }
}