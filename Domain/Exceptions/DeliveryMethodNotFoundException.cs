using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class DeliveryMethodNotFoundException(int Id) : NotFoundException($"No Delivery method by Id = {Id} Found")
    {

    }
}
