using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Todo.Domain.Models
{
    public class TodoCheck
    {
        public int Id { get; set; }
        public int TodoId { get; set; }
        public virtual Todo Todo { get; set; }
        public int Position { get; set; }
        [Required]
        public string TaskDescription { get; set; }
        public bool Done { get; set; }
        public DateTime Edited { get; set; }
    }
}
