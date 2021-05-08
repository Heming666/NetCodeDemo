using Repository.Entity.Models.Consume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.IService.Customer
{
    public interface IComsumeService
    {
        List<ConsumeEntity> GetList(Expression<Func<ConsumeEntity, bool>> expression);
        void Add(ConsumeEntity entity);
    }
}
