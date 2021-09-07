using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Todo.Domain.Models
{
    public class Todo
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Position { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public DateTime? Deleted { get; set; }
        [Required]
        public virtual ICollection<TodoTask> TodoChecks { get; set; }
        public virtual ApplicationUser AspNetUser { get; set; }
        public string AspNetUsersId { get; set; }


    }
}
