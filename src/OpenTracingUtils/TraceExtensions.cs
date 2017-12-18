using OpenTracing;

namespace OpenTracingUtils
{
    public static class TraceExtensions
    {
	    public static ISpan StartNonActive(this ISpanBuilder spanBuilder)
	    {
		    AdvancedSpanBuilder advancedSpanBuilder = spanBuilder as AdvancedSpanBuilder;

		    ISpan span = advancedSpanBuilder != null
			    ? advancedSpanBuilder.StartNonActive()
			    : spanBuilder.Start();

		    return span;
	    }

	    public static void MakeActive(this ISpan span)
	    {
		    AdvancedSpan advancedSpan = span as AdvancedSpan;
		    advancedSpan?.MakeActive();
	    }
    }
}
