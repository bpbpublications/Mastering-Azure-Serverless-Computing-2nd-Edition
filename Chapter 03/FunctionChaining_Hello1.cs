[FunctionName("FunctionChaining_Hello1")]
public static string SayHello1([ActivityTrigger] string name, ILogger log)
{
log.LogInformation(($"[Inside Activity Function 2] Saying hello to {name}.");
    return $"Hello1 {name}!";
}
