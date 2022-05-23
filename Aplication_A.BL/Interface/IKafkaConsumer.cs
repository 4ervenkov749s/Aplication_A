using Aplication_A.Models;
using System.Threading.Tasks;

namespace Aplication_A.BL.Interface
{
    public interface IKafkaConsumer
    {
        public Task PublishPersonAsync(Person p);

    }
}
