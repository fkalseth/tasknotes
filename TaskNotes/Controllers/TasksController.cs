using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using TaskNotes.Domain;

namespace TaskNotes.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskDtoRepository _tasks;

        public TasksController(TaskDtoRepository tasks)
        {
            _tasks = tasks;
        }

        public ViewResult All()
        {
            var tasks = _tasks.GetTasksNotYetCompleted();
            return View(new TasksViewModel(tasks));
        }
    }
}