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
        public virtual ICollection<TodoTask> TodoTasks { get; set; }
        public string AspNetUserId { get; set; }


    }
}
