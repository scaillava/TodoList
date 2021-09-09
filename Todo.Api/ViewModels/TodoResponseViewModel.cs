using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Api.ViewModels
{
   

    public class TodoResponseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public DateTime? Deleted { get; set; }
        public List<TodoTaskResponseViewModel> TodoTasks { get; set; }
    }


    public class TodoTaskResponseViewModel
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public string TaskDescription { get; set; }
        public bool Done { get; set; }
        public DateTime Edited { get; set; }
    }

}
