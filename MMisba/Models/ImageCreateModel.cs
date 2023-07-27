using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMisba.Models
{
    public class ImageCreateModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Plzz Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "plzz Choose ImageFile")]
        [Display(Name = "Choose Image")] 

        public IFormFile ImagePath { get; set; }
    }
}
