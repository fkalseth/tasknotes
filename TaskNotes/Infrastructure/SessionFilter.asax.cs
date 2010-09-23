using System.Web.Mvc;
using Cqrs;
using Ninject;

namespace TaskNotes.Infrastructure
{
    public class SessionFilter : IActionFilter
    {
        private readonly IKernel _kernel;

        public SessionFilter(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if(filterContext.Exception == null && !filterContext.Canceled)
            {
                _kernel.Get<ISession>().CommitChanges();   
            }
        }
    }
}