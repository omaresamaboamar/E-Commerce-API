using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{
    public enum OrderStatus
    {
        pending = 0,
        PayMentReceived = 1,
        paymentFailed = 2,
    }
}
