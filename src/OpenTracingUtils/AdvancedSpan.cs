using System;
using System.Collections.Generic;
using OpenTracing;

namespace OpenTracingUtils
{
    public class AdvancedSpan : ISpan
    {
	    private readonly ISpan _innerSpan;
	    private readonly ITraceContext _traceContext;
	    private bool _active;

	    public AdvancedSpan(ISpan innerSpan, ITraceContext traceContext, bool active)
	    {
		    _innerSpan = innerSpan;
		    _traceContext = traceContext;
		    _active = active;

		    if (_active)
		    {
			    _traceContext.Push(this);
		    }
	    }
		public ISpanContext Context => _innerSpan.Context;

	    public void Dispose()
	    {
		    _innerSpan.Dispose();

		    if (_active)
		    {
			    _traceContext.TryPop();
			    _active = false;
		    }
	    }

	    public ISpan SetOperationName(string operationName)
	    {
		    _innerSpan.SetOperationName(operationName);
		    return this;
	    }

	    public ISpan SetTag(string key, bool value)
	    {
			_innerSpan.SetTag(key, value);
		    return this;
		}

		public ISpan SetTag(string key, double value)
	    {
			_innerSpan.SetTag(key, value);
		    return this;
		}

		public ISpan SetTag(string key, int value)
	    {
			_innerSpan.SetTag(key, value);
		    return this;
		}

		public ISpan SetTag(string key, string value)
	    {
			_innerSpan.SetTag(key, value);
		    return this;
		}

		public ISpan Log(IEnumerable<KeyValuePair<string, object>> fields)
	    {
			_innerSpan.Log(fields);
			return this;
		}

	    public ISpan Log(DateTimeOffset timestamp, IEnumerable<KeyValuePair<string, object>> fields)
	    {
			_innerSpan.Log(timestamp, fields);
			return this;
		}

	    public ISpan Log(string eventName)
	    {
			_innerSpan.Log(eventName);
			return this;
		}

	    public ISpan Log(DateTimeOffset timestamp, string eventName)
	    {
			_innerSpan.Log(timestamp, eventName);
			return this;
		}

	    public ISpan SetBaggageItem(string key, string value)
	    {
			_innerSpan.SetBaggageItem(key, value);
			return this;
		}

	    public string GetBaggageItem(string key)
	    {
			return _innerSpan.GetBaggageItem(key);
		}

	    public void Finish()
	    {
			_innerSpan.Finish();

		    if (_active)
		    {
			    _traceContext.TryPop();
			    _active = false;
		    }
	    }

	    public void Finish(DateTimeOffset finishTimestamp)
	    {
			_innerSpan.Finish(finishTimestamp);

			if (_active)
			{
				_traceContext.TryPop();
				_active = false;
			}
		}

	    public void MakeActive()
	    {
		    if (_active)
		    {
			    throw new InvalidOperationException("Current span is already active.");
		    }

		    _traceContext.Push(this);
		    _active = true;
	    }
	}
}
