using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Interfaces
{
    public interface IMessageRepository
    {
        Task SendMessage(string productName, decimal productPrice);
    }
}
