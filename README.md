# opentracing-dotnet-context
Trace context extensions for opentracing-csharp.

Usage:

```csharp
ITracer zipkinTracer = new ZipkinTracer(...);
ITraceContext traceContext = new TraceContext();
ITracer tracer = new AdvancedTracer(zipkinTracer, traceContext);

using (tracer
    .BuildSpan("1") // Add a reference "child of" to the span "1" from the current active span.
    .Start()) // Make the span "1" active automatically.
{
    ...
    
    tracer.ActiveSpan.SetTag("key", "value");
    
    ...
    
    using (tracer
        .BuildSpan("1.1") // Add a reference "child of" from span "1.1" to the active span "1" automatically.
        .Start()) // Make the span "1.1" active automatically.
    {
        ...
        tracer.ActiveSpan.SetTag("key2", "value2");
        ...
    } // The span "1.1" is not anymore active. Span "1" is active again.
    
    ...
    
    using (ISpan span12 = tracer
        .BuildSpanIgnoreActive("1.2") // Do not add reference "child of" from span "1.2" to the active span "1" automatically.
        .FollowsFrom(tracer.ActiveSpan) // Add a reference "follows from" from span "1.2" to the active span "1" manually.
        .StartNonActive()) // Do not make span "1.2" active automatically. The span "1" remains active.
    {
        ...
        span12.MakeActive(); // Make the span "1.2" active manually.
        tracer.ActiveSpan.SetTag("key2", "value2");
        ...
    } // The span "1.2" is not anymore active. Span "1" is active again.
    
    ...
    
} // The span "1" is not anymore active. The active span has its previous value.

```