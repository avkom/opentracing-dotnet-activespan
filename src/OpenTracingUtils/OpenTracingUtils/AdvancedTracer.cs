using OpenTracing;
using OpenTracing.Propagation;

namespace OpenTracingUtils
{
    public class AdvancedTracer : IAdvancedTracer
    {
	    private readonly ITracer _innerTracer;
	    private readonly ITraceContext _traceContext;

	    public AdvancedTracer(ITracer innerTracer, ITraceContext traceContext)
	    {
		    _innerTracer = innerTracer;
		    _traceContext = traceContext;
	    }

	    public ISpan ActiveSpan => _traceContext.CurrentSpan;

		public ISpanBuilder BuildSpan(string operationName)
		{
			ISpanBuilder innerSpanBuilder = _innerTracer.BuildSpan(operationName);
			ISpanBuilder spanBuilder = new AdvancedSpanBuilder(innerSpanBuilder, _traceContext);
			spanBuilder.AsChildOf(_traceContext.CurrentSpan);
			return spanBuilder;
		}

		public ISpanBuilder BuildSpanIgnoreActive(string operationName)
		{
			ISpanBuilder innerSpanBuilder = _innerTracer.BuildSpan(operationName);
			ISpanBuilder spanBuilder = new AdvancedSpanBuilder(innerSpanBuilder, _traceContext);
			return spanBuilder;
		}

		public void Inject<TCarrier>(ISpanContext spanContext, Format<TCarrier> format, TCarrier carrier)
	    {
		    _innerTracer.Inject(spanContext, format, carrier);
	    }

	    public ISpanContext Extract<TCarrier>(Format<TCarrier> format, TCarrier carrier)
	    {
		    return _innerTracer.Extract(format, carrier);
	    }

    }
}
