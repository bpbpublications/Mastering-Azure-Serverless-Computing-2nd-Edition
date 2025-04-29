[FunctionName("FanOutFanIn_HttpStart")]
public static async Task<HttpResponseMessage> HttpStart(
[HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req, [DurableClient] IDurableOrchestrationClient starter,
ILogger log)
{
    // Function input comes from the request content.
    string instanceId = await starter.StartNewAsync("FanOutFanIn", null);
    log.LogInformation($"Started orchestration with ID = ‘{instanceId}’.");
    return starter.CreateCheckStatusResponse(req, instanceId);
}
