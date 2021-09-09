using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Todo.Api.ViewModels
{
    [DataContract]
    public class UpsertTodoViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [Required]
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public int Position { get; set; }
        [DataMember]
        public List<UpsertTodoTaskViewModel> TodoTasks { get; set; }
    }
    public class UpsertTodoTaskViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Position { get; set; }
        [DataMember]
        [Required]
        public string TaskDescription { get; set; }
        [DataMember]
        public bool Done { get; set; }
    }

}
