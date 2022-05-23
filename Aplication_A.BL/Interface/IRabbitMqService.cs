using Aplication_A.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication_A.BL.Interface
{
    public interface IRabbitMqService
    {
        Task SendPersonAsync(Person p);
    }
}
