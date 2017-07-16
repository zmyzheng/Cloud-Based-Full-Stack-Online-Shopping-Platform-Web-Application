namespace GithubHook.Model
{
    using System.Collections.Generic;

    public class GithubPayload
    {
        public IEnumerable<Commit> Commits { get; set; }
    }
}