using Elastic.Apm.Api;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace PerformanceMonitor
{
    [PSerializable]
    public class PerformanceMonitorAttribute : OnMethodBoundaryAspect
    {
        public ISpan Span { get; set; }

        public override void OnEntry(MethodExecutionArgs args)
        {
            string methodName = string.Format("{0}.{1}", args.Method.DeclaringType.Name, args.Method.Name);
            var transaction = Elastic.Apm.Agent.Tracer?.CurrentTransaction;
            if (transaction != null)
            {
                this.Span = transaction.StartSpan(methodName, ApiConstants.TypeExternal);
            }
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            if (this.Span != null)
            {
                this.Span.End();
            }
        }
    }
}
