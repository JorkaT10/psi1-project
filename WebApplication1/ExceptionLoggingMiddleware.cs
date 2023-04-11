using PSI_MobileApp;

namespace WebApplication1
{
	public class ExceptionLoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ExceptionLogger logger;
		public ExceptionLoggingMiddleware(RequestDelegate next)
		{
			_next = next;
			logger = new ExceptionLogger();
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception e)
			{
				logger.Log(e);
				throw;
			}
		}
	}
}
