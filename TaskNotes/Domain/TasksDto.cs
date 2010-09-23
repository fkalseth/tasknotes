using System;
using System.Collections.Generic;

namespace TaskNotes.Domain
{
    public class TasksDto
    {
        private readonly TaskDto[] _tasks;

        public TasksDto(TaskDto[] tasks)
        {
            _tasks = tasks;
        }

        public TaskDto[] Tasks { get { return _tasks; } }
    }
}