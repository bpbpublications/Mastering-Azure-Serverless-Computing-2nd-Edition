[FunctionName("FanOutFanIn")]
public static async Task<int> RunOrchestrator(
[OrchestrationTrigger] IDurableOrchestrationContext context)
{
    //Fan Out Activity Function Instances   
    var tasks = new Task<int>[20];
    for (int i = 0; i < 20; i++)
    {
       int num = i + 1;
       Tasks[i] = context.CallActivityAsync<int>("FanOutFanIn_OddEven", num);
    }
    await Task.WhenAll(tasks);
    //Once all Tasks complete
    int sum = tasks.Sum(t => t.Result);
    return sum;
}
