[FunctionName("FanOutFanIn_OddEven")]
public static int OddEven([ActivityTrigger] int number, ILogger log)
{
    log.LogInformation($"Number received {number}.");
    //Return the number if number is even else return 0
    int num = 0;
    if(number % 2 == 0)
    {
        num = number;
    }
    return num;
}
