using System;
using System.ComponentModel.DataAnnotations;

namespace TaskNotes.Domain
{
    public class TaskDto
    {
        public TaskDto()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Due { get; set; }

        public bool IsPastDue { get { return Due <= DateTime.Now; } }
    }
}