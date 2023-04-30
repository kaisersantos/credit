namespace Credit.Core.Application.Exceptions
{
    public class Error
    {
        public string Key { get; }

        public ICollection<string> Messages { get; }

        public Error(string key, ICollection<string> messages)
        {
            Key = key;
            Messages = new List<string>(messages);
        }

        public Error(string key, string message)
        {
            Key = key;
            Messages = new List<string>()
            {
                message
            };
        }
    }
}
