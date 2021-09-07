using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.App.ViewModels
{
    public class UpsertTodoTaskViewModel
    {
        public int Id { get; set; }
        public int TodoId { get; set; }
        public int Position { get; set; }
        [Required]
        public string TaskDescription { get; set; }
        public bool Done { get; set; }
    }
}
