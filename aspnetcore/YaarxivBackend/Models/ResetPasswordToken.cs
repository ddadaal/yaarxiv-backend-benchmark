using System;
using System.Collections.Generic;

namespace YaarxivBackend.Models
{
    public partial class ResetPasswordToken
    {
        public string Id { get; set; }
        public string UserEmail { get; set; }
        public DateTime Time { get; set; }
    }
}
