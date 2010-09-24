using System;
using System.Web.Mvc;
using Cqrs.Commands;
using TaskNotes.Domain;
using TaskNotes.Domain.Commands;

namespace TaskNotes.Controllers
{
    public class TaskController : Controller
    {
        private readonly ICommandSender _commands;
        private readonly TaskDtoRepository _repository;

        public TaskController(ICommandSender commands, TaskDtoRepository repository)
        {
            _commands = commands;
            _repository = repository;
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View(new TaskDto());
        }

        [HttpPost]
        public ActionResult Create(TaskDto task)
        {
            var command = new CreateTaskCommand(task.Id, task.Description, task.Due);
            return Execute(command) ?? View(task);
        }

        public ActionResult Complete(Guid taskId)
        {
            var command = new CompleteTaskCommand(taskId);
            return Execute(command) ?? RedirectToAction("All", "Tasks");
        }

        public ActionResult Cancel(Guid taskId)
        {
            var command = new CancelTaskCommand(taskId);
            return Execute(command) ?? RedirectToAction("All", "Tasks");
        }

        [HttpGet]
        public ViewResult Prioritise(Guid taskId)
        {
            var task = _repository.GetTaskById(taskId);
            return View(new NewDueDateViewModel(){TaskId = task.Id, Description = task.Description, NewDueDate = task.Due});
        }
        
        [HttpPost]
        public ActionResult Prioritise(NewDueDateViewModel task)
        {
            var command = new PrioritiseTaskCommand(task.TaskId, task.NewDueDate);
            return Execute(command) ?? View(task);
        }

        [HttpGet]
        public ViewResult Postpone(Guid taskId)
        {
            var task = _repository.GetTaskById(taskId);
            return View(new NewDueDateViewModel{TaskId = task.Id, Description = task.Description, NewDueDate = task.Due});
        }

        [HttpPost]
        public ActionResult Postpone(NewDueDateViewModel task)
        {
            var command = new PostponeTaskCommand(task.TaskId, task.NewDueDate);
            return Execute(command) ?? View(task);
        }
        
        private ActionResult Execute(ICommand command)
        {
            RedirectToRouteResult result = null;

            if (ModelState.IsValid)
            {
                try
                {
                    _commands.Send(command);
                    result = RedirectToAction("All", "Tasks");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Command", ex.Message);
                }
            }

            return result;
        }
    }
}