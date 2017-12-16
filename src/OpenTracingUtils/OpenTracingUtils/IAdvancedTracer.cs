using OpenTracing;

namespace OpenTracingUtils
{
    public interface IAdvancedTracer : ITracer
    {
		ISpan ActiveSpan { get; }

	    ISpanBuilder BuildSpanIgnoreActive(string operationName);
    }
}
