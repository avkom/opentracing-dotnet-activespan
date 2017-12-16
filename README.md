# opentracing-dotnet-context
Trace context extensions for opentracing-csharp.

Usage:

```csharp
ITracer zipkinTracer = new ZipkinTracer(...);
ITraceContext traceContext = new TraceContext();
ITracer tracer = new AdvancedTracer(zipkinTracer, traceContext);

using (tracer.BuildSpan("1") // Add a reference "child of" to the span "1" from the active span.
    .Start()) // Make the span "1" as the new active span.
{
    ...
    
    tracer.ActiveSpan.SetTag("key1", "value1");
    
    ...
    
    using (tracer.BuildSpan("1.1") // Add a reference "child of" to the span "1.1" from the active span "1" automatically.
        .Start()) // Make the span "1.1" as the new active span automatically.
    {
        ...
        tracer.ActiveSpan.SetTag("key2", "value2");
        ...
    } // The span "1.1" is not anymore the active span. Span "1" is the active span again.
    
    ...
    
    using (ISpan span12 = tracer.BuildSpanIgnoreActive("1.2") // We do not add reference "child of" to a new span "1.2" from the span "1".
        .FollowsFrom(tracer.ActiveSpan) // Add a reference "follows from" to the span "1.2" from the active span "1" manually.
        .StartNonActive()) // The span "1.2" is not made the active span. The span "1" remains the active span.
    {
        ...
        span12.MakeActive(); // Make the span "1.2" the active span manually.
        tracer.ActiveSpan.SetTag("key2", "value2");
        ...
    } // The span "1.2" is not anymore the active span. Span "1" is the active span again.
    
    ...
    
} // The span "1" is not anymore the active span. The active span will have its previous value.


```