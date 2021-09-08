using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Todo.Api.ViewModels
{
    [DataContract]
    public class UpsertTodoTaskViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int TodoId { get; set; }
        [DataMember]
        public int Position { get; set; }
        [DataMember]
        [Required]
        public string TaskDescription { get; set; }
        [DataMember]
        public bool Done { get; set; }
    }
}
