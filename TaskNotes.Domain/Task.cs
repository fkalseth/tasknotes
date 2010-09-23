using System;
using System.Collections.Generic;
using Cqrs;
using Cqrs.Events;
using TaskNotes.Domain.Events;

namespace TaskNotes.Domain
{
    public class Task : AggregateRoot
    {
        private TaskState _state;
        private DateTime _dueDate;

        public Task(IEnumerable<IDomainEvent> events)
            : base(events)
        {}

        public Task(Guid id, string description, DateTime due)
        {
            if (due < DateTime.Now) throw new ArgumentException("The due date cannot be in the past");
            if (String.IsNullOrEmpty(description)) throw new ArgumentException("The description cannot be empty");
            if (id == Guid.Empty) throw new ArgumentException("The id cannot be empty");

            Apply(new TaskCreated(id, description, due));
        }

        private void OnCreated(TaskCreated @event)
        {
            Id = @event.TaskId;
            _dueDate = @event.Due;
            _state = TaskState.Pending;
        }

        protected override void RegisterDomainEvents(EventHandlerCollection events)
        {
            events.Register<TaskCreated>(OnCreated);
            events.Register<TaskPostponed>(OnPostponed);
            events.Register<TaskCompleted>(OnCompleted);
            events.Register<TaskCancelled>(OnCancelled);
            events.Register<TaskPrioritised>(OnPrioritised);
        }

        public void Postpone(DateTime newDueDate)
        {
            if(newDueDate < _dueDate) throw new ArgumentException("the new due date must be larger than current due date");
            Apply(new TaskPostponed(Id, _dueDate, newDueDate));
        }

        private void OnPostponed(TaskPostponed ev)
        {
            _dueDate = ev.NewDueDate;
        }
        
        public void Prioritise(DateTime newDueDate)
        {
            if (newDueDate > _dueDate) throw new ArgumentException("the new due date must be less than the current due date");
            if(newDueDate < DateTime.Now) throw new ArgumentException("the new due date cannot be in the past");
            Apply(new TaskPrioritised(Id, _dueDate, newDueDate));
        }

        private void OnPrioritised(TaskPrioritised ev)
        {
            _dueDate = ev.NewDueDate;
        }

        public void Cancel()
        {
            if(_state != TaskState.Pending) throw new InvalidOperationException("Cannot cancel a task that is not pending");
            Apply(new TaskCancelled(Id));
        }

        private void OnCancelled(TaskCancelled ev)
        {
            _state = TaskState.Cancelled;
        }

        public void Complete()
        {
            if(_state != TaskState.Pending) throw new InvalidOperationException("Cannot complete a task that is not pending");
            Apply(new TaskCompleted(Id, DateTime.Now));
        }

        private void OnCompleted(TaskCompleted ev)
        {
            _state = TaskState.Completed;
        }
    }
}