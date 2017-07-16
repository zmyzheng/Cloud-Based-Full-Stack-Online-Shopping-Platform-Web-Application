namespace GithubHook.Model
{
    using System.Collections.Generic;

    public class Commit
    {
        public string Message { get; set; }
        public IEnumerable<string> Added { get; set; }
        public IEnumerable<string> Removed { get; set; }
        public IEnumerable<string> Modified { get; set; }
    }
}