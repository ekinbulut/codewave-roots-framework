public static class TraceIdProvider
{
    private static AsyncLocal<string> _traceId = new AsyncLocal<string>();

    public static void SetTraceId(string traceId)
    {
        _traceId.Value = traceId;
    }

    public static string GetTraceId()
    {
        return _traceId.Value;
    }
}