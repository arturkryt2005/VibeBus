﻿using System.ComponentModel.DataAnnotations.Schema;

namespace VipeBus.Application.Entities.Users
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [NotMapped]
        public string LastName => Name.Split(' ')[0];

        [NotMapped]
        public string FirstName => Name.Split(' ')[1];

        [NotMapped]
        public string MiddleName => Name.Split(' ')[2];
    }
}
