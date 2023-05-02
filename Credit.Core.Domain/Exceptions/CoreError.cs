namespace Credit.Core.Domain.Exceptions
{
    public class CoreError
    {
        public string Key { get; }

        public ICollection<string> Messages { get; }

        public CoreError(string key, ICollection<string> messages)
        {
            Key = key;
            Messages = new List<string>(messages);
        }

        public CoreError(string key, string message)
        {
            Key = key;
            Messages = new List<string>()
            {
                message
            };
        }
    }
}
