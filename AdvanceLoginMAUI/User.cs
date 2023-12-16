using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvanceLoginMAUI
{
    public class User
    {
        [Key]
        public int id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public DateTime DOB { get; set; }
        
        [Required]
        public string POB { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public byte[] myArray { get; set; }
    }
}
