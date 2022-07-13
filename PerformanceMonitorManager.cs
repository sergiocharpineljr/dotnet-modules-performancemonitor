using Elastic.Apm.Api;

namespace PerformanceMonitor
{
    public class PerformanceMonitorManager
    {
        public static PerformanceMonitorSpan BeginSpan(string spanName)
        {
            var transaction = Elastic.Apm.Agent.Tracer?.CurrentTransaction;
            PerformanceMonitorSpan span = new PerformanceMonitorSpan();
            if (transaction != null)
            {
                span.Span = transaction.StartSpan(spanName, ApiConstants.TypeExternal);
            }

            return span;
        }

        public static void EndSpan(PerformanceMonitorSpan span)
        {
            if (span.Span != null)
            {
                span.Span.End();
            }
        }
    }
}
