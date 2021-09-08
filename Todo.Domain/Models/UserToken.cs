using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Models
{
    public class UserToken
    {
        public int Id { get; set; }
        public Guid Token { get; set; }
        public virtual ApplicationUser AspNetUser { get; set; }
        public string AspNetUserId { get; set; }
        public DateTime Expiration { get; set; }
    }
}
