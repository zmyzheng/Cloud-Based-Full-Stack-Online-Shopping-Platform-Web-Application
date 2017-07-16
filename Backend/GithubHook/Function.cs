namespace GithubHook
{
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Amazon.Lambda.Core;
    using GithubHook.Model;
    using Newtonsoft.Json;

    public class Function
    {
        private const string SlackUrl = "https://hooks.slack.com/services/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";

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task FunctionHandler(GithubPayload payload)
        {
            var res = payload.Commits.Select(c =>
                new
                {
                    Message = c.Message,
                    Added = c.Added.Where(s => s.StartsWith("Backend/Source/Shared/")),
                    Removed = c.Removed.Where(s => s.StartsWith("Backend/Source/Shared/")),
                    Modified = c.Modified.Where(s => s.StartsWith("Backend/Source/Shared/"))
                }
            );

            var client = new HttpClient();
            foreach (var r in res)
            {
                if (r.Added.Count() == 0 && r.Removed.Count() == 0 && r.Modified.Count() == 0) continue;
                await client.PostAsync(SlackUrl, new StringContent(
                    JsonConvert.SerializeObject(
                        new
                        {
                            text = JsonConvert.SerializeObject(
                                new
                                {
                                    Message = r.Message,
                                    Added = r.Added,
                                    Removed = r.Removed,
                                    Modified = r.Modified
                                }, Formatting.Indented
                            )
                        }
                    )
                ));
            }
        }
    }
}
