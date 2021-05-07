using Repository.Entity.Models;
using Repository.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IService.Base
{
    public interface IDepartmentService
    {
        List<DepartmentEntity> GetList(System.Linq.Expressions.Expression<Func<DepartmentEntity, bool>> Exception);
    }
}
