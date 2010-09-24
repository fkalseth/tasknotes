using System;
using System.Collections.Generic;
using System.Linq;
using Cqrs;
using Cqrs.Events.Handlers;
using TaskNotes.Domain.Events;

namespace TaskNotes.Domain
{
    public class TaskDtoRepository : 
        IDomainEventHandler<TaskCreated>,
        IDomainEventHandler<TaskCompleted>,
        IDomainEventHandler<TaskCancelled>,
        IDomainEventHandler<TaskPostponed>,
        IDomainEventHandler<TaskPrioritised>
    {
        private readonly List<TaskDto>  _tasks = new List<TaskDto>();

        public TaskDto[] GetTasksNotYetCompleted()
        {
            return _tasks.ToArray();
        }

        public TaskDto GetTaskById(Guid id)
        {
            return _tasks.FirstOrDefault(task => task.Id == id);
        }

        void IDomainEventHandler<TaskCreated>.Handle(TaskCreated @event)
        {
            _tasks.Add(new TaskDto {Id = @event.TaskId, Description = @event.Description, Due = @event.Due});
        }

        void IDomainEventHandler<TaskCompleted>.Handle(TaskCompleted @event)
        {
            RemoveTaskById(@event.TaskId);
        }

        void IDomainEventHandler<TaskCancelled>.Handle(TaskCancelled @event)
        {
            RemoveTaskById(@event.TaskId);
        }

        private void RemoveTaskById(Guid taskId)
        {
            TaskDto taskToRemove = GetTaskById(taskId);
            if(null != taskToRemove) _tasks.Remove(taskToRemove);
        }

        void IDomainEventHandler<TaskPostponed>.Handle(TaskPostponed @event)
        {
            var task = GetTaskById(@event.TaskId);
            task.Due = @event.NewDueDate;
        }

        void IDomainEventHandler<TaskPrioritised>.Handle(TaskPrioritised @event)
        {
            var task = GetTaskById(@event.TaskId);
            task.Due = @event.NewDueDate;
        }
    }
}