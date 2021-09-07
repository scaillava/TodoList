using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.App.ViewModels
{
    public class UpsertTodoViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Position { get; set; }
        public List<UpsertTodoTaskViewModel> TodoChecks { get; set; }
    }


}
