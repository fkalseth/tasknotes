using System;
using System.Collections.Generic;

namespace TaskNotes.Domain
{
    public class TasksViewModel
    {
        private readonly TaskDto[] _tasks;

        public TasksViewModel(TaskDto[] tasks)
        {
            _tasks = tasks;
        }

        public TaskDto[] Tasks { get { return _tasks; } }
    }
}