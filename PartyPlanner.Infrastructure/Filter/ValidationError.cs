using System;

namespace PartyPlanner.Infrastructure.Filter
{
    [Serializable]
    public class ValidationError
    {
        public ValidationError(string message)
        {
            Message = message;
        }

        public ValidationError(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }

        public string Message { get; set; }
    }
}
