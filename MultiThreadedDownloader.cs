using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class MultiThreadedDownloader
{
    static async Task Main()
    {
        string[] urls = {
            "https://example.com/file1.jpg",
            "https://example.com/file2.jpg"
        };

        using HttpClient client = new HttpClient();
        var tasks = new Task[urls.Length];

        for (int i = 0; i < urls.Length; i++)
        {
            int index = i;
            tasks[i] = Task.Run(async () =>
            {
                string fileName = Path.GetFileName(urls[index]);
                Console.WriteLine($"Downloading {fileName}...");
                byte[] data = await client.GetByteArrayAsync(urls[index]);
                await File.WriteAllBytesAsync(fileName, data);
                Console.WriteLine($"Completed {fileName}");
            });
        }

        await Task.WhenAll(tasks);
        Console.WriteLine("All downloads completed!");
    }
}
