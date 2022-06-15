using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceDetection.Messaging.InterfacesConstants.Constants
{
    public static class RabbitMQMassTransitConstants
    {
        public const string RabbitMQUri = "rabbitmq://rabbit,q:5672";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string RegiserOrderServiceQueue = "regiseter.order.command";
    }
}
