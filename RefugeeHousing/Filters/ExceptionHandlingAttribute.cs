using System.Web.Mvc;
using NLog;

namespace RefugeeHousing.Filters
{
    public class ExceptionHandlingAttribute : HandleErrorAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnException(ExceptionContext exceptionContext)
        {
            Logger.Error(exceptionContext.Exception, "Unexpected exception. Sending an error response to the client.");

            base.OnException(exceptionContext);
        }
    }
}