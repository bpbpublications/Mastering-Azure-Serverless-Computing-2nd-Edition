[FunctionName("FunctionChaining_Hello2")]
public static string SayHello2([ActivityTrigger] string name, ILogger log)
{
log.LogInformation($"[Inside Activity Function 3] Saying hello to {name}.");
    return $"Hello2 {name}!";
}
