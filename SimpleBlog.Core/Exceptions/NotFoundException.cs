using System;

namespace SimpleBlog.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public string? ResourceName { get; }
        public object? ResourceKey { get; }

        public NotFoundException(string resourceName, object resourceKey)
            : base($"Resource '{resourceName}' with key '{resourceKey}' was not found.")
        {
            ResourceName = resourceName;
            ResourceKey = resourceKey;
        }
    }
}