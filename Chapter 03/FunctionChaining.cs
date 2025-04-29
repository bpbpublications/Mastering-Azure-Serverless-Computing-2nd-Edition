[FunctionName("FunctionChaining")]
public static async Task<List<string>> RunOrchestrator(
[OrchestrationTrigger] IDurableOrchestrationContext context)
{
    var outputs = new List<string>();
    //Fetch the input passed on by the HTTP Orchestrator client
    string name = context.GetInput<string>();
    //Chain First Activity Function
    outputs.Add(await   context.CallActivityAsync<string>("FunctionChaining_Hello", name));
    //Chain Second Activity Function
    outputs.Add(await context.CallActivityAsync<string>("FunctionChaining_Hello1", name));
    //Chain Third Activity Function
    outputs.Add(await context.CallActivityAsync<string>("FunctionChaining_Hello2", name));
    return outputs;
}
