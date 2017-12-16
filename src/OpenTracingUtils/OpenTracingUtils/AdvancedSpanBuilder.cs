using System;
using OpenTracing;

namespace OpenTracingUtils
{
    public class AdvancedSpanBuilder : ISpanBuilder
    {
	    private readonly ISpanBuilder _innerSpanBuilder;
	    private readonly ITraceContext _traceContext;

	    public AdvancedSpanBuilder(ISpanBuilder innerSpanBuilder, ITraceContext traceContext)
	    {
		    _innerSpanBuilder = innerSpanBuilder;
		    _traceContext = traceContext;
	    }

		public ISpanBuilder AsChildOf(ISpan parent)
		{
			return _innerSpanBuilder.AsChildOf(parent);
		}

	    public ISpanBuilder AsChildOf(ISpanContext parent)
	    {
			return _innerSpanBuilder.AsChildOf(parent);
	    }

	    public ISpanBuilder FollowsFrom(ISpan parent)
	    {
			return _innerSpanBuilder.FollowsFrom(parent);
	    }

	    public ISpanBuilder FollowsFrom(ISpanContext parent)
	    {
			return _innerSpanBuilder.FollowsFrom(parent);
		}

	    public ISpan Start()
	    {
		    ISpan innerSpan = _innerSpanBuilder.Start();
			ISpan span = new AdvancedSpan(innerSpan, _traceContext, true);
			return span;
	    }

		public ISpan StartNonActive()
		{
			ISpan innerSpan = _innerSpanBuilder.Start();
			ISpan span = new AdvancedSpan(innerSpan, _traceContext, false);
			return span;
		}

		public ISpanBuilder WithStartTimestamp(DateTimeOffset startTimestamp)
	    {
			return _innerSpanBuilder.WithStartTimestamp(startTimestamp);
		}

	    public ISpanBuilder WithTag(string key, string value)
	    {
			return _innerSpanBuilder.WithTag(key, value);
		}

	    public ISpanBuilder WithTag(string key, int value)
	    {
			return _innerSpanBuilder.WithTag(key, value);
		}

	    public ISpanBuilder WithTag(string key, double value)
	    {
			return _innerSpanBuilder.WithTag(key, value);
		}

	    public ISpanBuilder WithTag(string key, bool value)
	    {
			return _innerSpanBuilder.WithTag(key, value);
		}

	    public ISpanBuilder AddReference(string referenceType, ISpanContext referencedContext)
	    {
			return _innerSpanBuilder.AddReference(referenceType, referencedContext);
		}
    }
}
