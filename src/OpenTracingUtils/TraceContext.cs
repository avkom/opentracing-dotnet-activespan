using System;
using System.Collections.Generic;
using OpenTracing;

namespace OpenTracingUtils
{
	public class TraceContext : ITraceContext
	{
		[ThreadStatic]
		private static Stack<ISpan> _spanStack;

		private Stack<ISpan> GetSpanStack()
		{
			var spanStack = _spanStack;

			if (spanStack == null)
			{
				spanStack = new Stack<ISpan>();
				_spanStack = spanStack;
			}

			return spanStack;
		}

		public ISpan CurrentSpan
		{
			get
			{
				Stack<ISpan> stack = GetSpanStack();

				ISpan span = stack.Count == 0 
					? stack.Peek() 
					: null;

				return span;
			}
		}

		public void Push(ISpan span)
		{
			if (span == null)
			{
				throw new ArgumentNullException(nameof(span));
			}

			Stack<ISpan> stack = GetSpanStack();
			stack.Push(span);
		}

		public void TryPop()
		{
			Stack<ISpan> stack = GetSpanStack();

			if (stack.Count != 0)
			{
				stack.Pop();
			}
		}
	}
}
