﻿using System.ComponentModel.DataAnnotations.Schema;

namespace LibaryManagementSystem.Core.Models.Entities
{
    public class User
    {
        [Column("UserId")]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
