﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Watches.Application.DataTransfer
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public IEnumerable<UserUseCaseDto> UserUseCases { get; set; }
    }
}
