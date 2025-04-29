[FunctionName("FunctionChaining_HttpStart")]
public static async Task<HttpResponseMessage> HttpStart(
[HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] 
HttpRequestMessage req,
[DurableClient] IDurableOrchestrationClient starter,
ILogger log)
{
   //Invoke Orchestrator Function and pass value Abhishek as parameter
   string instanceId = await starter.StartNewAsync("FunctionChaining",  "Instance","Abhishek");
   log.LogInformation($"Started orchestration with ID = ‘{instanceId}’.");
   //Return Orchestration Workflow invocation status
   return starter.CreateCheckStatusResponse(req, instanceId);
}
