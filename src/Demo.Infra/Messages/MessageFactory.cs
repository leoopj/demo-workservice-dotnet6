using NewGO.Integration.Infra.Messages;
using System;
using System.Threading;

namespace NewGO.Integration.Infra
{
    public static class MessageFactory
    {
        public static ResourceMessage GetMessage(this string resourceCode)
        {
            return new ResourceMessage
            {
                Code = resourceCode,
                Message = Resource.ResourceManager.GetString(resourceCode, Thread.CurrentThread.CurrentCulture)
            };
        }

        public static ResourceMessage GetMessage(this string resourceCode, params object[] parameters)
        {
            return new ResourceMessage
            {
                Code = resourceCode,
                Message = string.Format(Resource.ResourceManager.GetString(resourceCode, Thread.CurrentThread.CurrentCulture), parameters)
            };
        }

        public static ResourceMessage GetMessage(this string resourceCode, Exception exception)
        {
            return new ResourceMessage
            {
                Code = resourceCode,
                Message = $"{Resource.ResourceManager.GetString(resourceCode, Thread.CurrentThread.CurrentCulture)}. Exception [{exception}]"
            };
        }
    }

    public class ResourceMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return $"{Code}-{Message}";
        }
    }
}
