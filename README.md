# opentracing-dotnet-activespan
Trace context extensions for opentracing-csharp.

This library provides wrappers and extension methods around OpenTracing interfaces which can be used for propagating the current tracing `Span` throughout the application without changing the application's code.

 However, if the application is starting new threads or is using thread pools, the thread-local context is not going to be carried over into the execution in the next thread. To maintain context propagation, a wrapper `TraceTaskScheduler` and `TraceTaskFactory` are provided that automatically transfers the context onto the new threads.

Features:
1. `ISpan IAdvancedTracer.ActiveSpan { get; }` provides a reference to a current active span.
2. `ITracer.BuildSpan(string operationName)` adds reference "child of" from a new created span to a current active span automatically.
If you do not want this reference, use `IAdvancedTracer.BuildIgnoreActive(string operationName)` method instead.
3. `ISpan ISpanBuilder.Start()` starts a new span and makes it active automatically.
If you do not want to make it active, use `ISpan TraceExtensions.StartNonActive(this ISpanBuilder spanBuilder)` extension method instead.
4. You can make this span active later by using `void TraceExtensions.MakeActive(this ISpan span)` extension method.
5. `void ISpan.Dispose()`/`void ISpan.Finish()`/`void ISpan.Finish(DateTimeOffset finishTimestamp)` restores previous value of active span automatically.

Usage:

```csharp
ITracer zipkinTracer = new ZipkinTracer(...);
ITraceContext traceContext = new TraceContext();
IAdvancedTracer tracer = new AdvancedTracer(zipkinTracer, traceContext);

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
