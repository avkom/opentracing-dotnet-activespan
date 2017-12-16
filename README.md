# opentracing-dotnet-context
Trace context extensions for opentracing-csharp.

Usage:

```csharp
ITracer zipkinTracer = new ZipkinTracer(...);
ITraceContext traceContext = new TraceContext();
ITracer tracer = new AdvancedTracer(zipkinTracer, traceContext);

using (tracer.BuildSpan("operationName").Start())
{
    ...
    
    tracer.ActiveSpan.SetTag("key1", "value1");
    
    ...
    
    using (tracer.BuildSpan("operationName").Start())
    {
        tracer.ActiveSpan.SetTag("key2", "value2");
    }
    
    ...
}

```