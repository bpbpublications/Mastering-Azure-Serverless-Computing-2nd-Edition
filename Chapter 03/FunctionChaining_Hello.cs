[FunctionName("FunctionChaining_Hello")]
public static string SayHello([ActivityTrigger] string name, ILogger log)
{
    log.LogInformation($"[Inside Activity Function 1] Saying hello to {name}.");
    return $"Hello {name}!";
}
