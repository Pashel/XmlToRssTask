using System.Web.Mvc;

namespace XmlToRssTask.Filters 
{
    public class AjaxException : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled) {
                // Send Internal server error with original exception message
                var result = new HttpStatusCodeResult(500, exceptionContext.Exception.Message);
                exceptionContext.Result = result;
                exceptionContext.ExceptionHandled = true;
            }
        }
    }
}