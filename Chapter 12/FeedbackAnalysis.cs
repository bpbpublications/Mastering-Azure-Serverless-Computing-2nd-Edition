using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.AI.TextAnalytics;
using Azure;

namespace func_feedbackanalysis
{
    public class FeedbackAnalysis
    {
        private readonly ILogger<FeedbackAnalysis> _logger;

        public FeedbackAnalysis(ILogger<FeedbackAnalysis> logger)
        {
            _logger = logger;
        }

        [Function("FeedbackAnalysis")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            // API Key and Text Analytics Service endpoint
            string apiKey = "[API Key]";
            string endpoint = "[Endpoint URL]";

            // Get the Tweet from the body of the request
            string tweetText = await new StreamReader(req.Body).ReadToEndAsync();

            AzureKeyCredential creds = new AzureKeyCredential(apiKey);
            Uri uri = new Uri(endpoint);
            var client = new TextAnalyticsClient(uri, creds);
            var sentiment = client.AnalyzeSentiment(tweetText);
            double positiveScore = sentiment.Value.ConfidenceScores.Positive;

            //Derive suggested action from the score
            string suggestedAction = string.Empty;
            if (positiveScore < 0.6)
            {
                suggestedAction = @"Customer has some issues. 
                                    Please act on it immediately. 
                                    Customer Satisfaction Score : "
                    + Math.Ceiling(positiveScore * 100) + "%.:" + "Negative";
            }
            else
            {
                suggestedAction = @"Customer is satisfied. 
                                    Continue to keep the customer happy. 
                                    Customer Satisfaction Score : "
                    + Math.Ceiling(positiveScore * 100) + "%.:" + "Positive";
            }


            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult(suggestedAction);
        }
    }
}
