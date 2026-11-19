using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModule;

namespace Services.Specification
{
    public class OrderWithSpecifications : BaseSpecification<Order , Guid>
    {
        public OrderWithSpecifications(string email):base(o => o.UserEmail == email)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o=> o.Items);
        }
        public OrderWithSpecifications(Guid Id) : base(o => o.Id == Id)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.Items);
        }
    }
}
