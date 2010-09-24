using System;
using System.ComponentModel.DataAnnotations;

namespace TaskNotes.Domain
{
    public class NewDueDateViewModel
    {
        public Guid TaskId { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public DateTime NewDueDate { get; set; }
    }
}