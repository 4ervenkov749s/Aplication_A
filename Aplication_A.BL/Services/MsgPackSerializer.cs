using Confluent.Kafka;
using MessagePack;

namespace Aplication_A.BL.Services
{
    public class MsgPackSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            return MessagePackSerializer.Serialize<T>(data);
        }
    }
}
