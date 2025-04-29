public static class Function1
{
       [FunctionName("Function1")]
public static void Run([BlobTrigger("samples-workitems/{name}", Connection = "")]TextReader myBlob,
[Queue("outputqueue"), StorageAccount("AzureWebJobsStorage")] ICollector<string> msg,
            string name, ILogger log)
       {
            //Read from Blob Text
            string blobContent = myBlob.ReadToEnd();
            msg.Add(blobContent);
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name}");
        }
 } 
