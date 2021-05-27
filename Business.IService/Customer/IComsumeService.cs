using Repository.Entity.Models.Consume;
using Repository.Entity.ViewModels.Index;
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
        Task Add(ConsumeEntity entity);
        Task<List<ChartsPieModel>> LoadPie(Expression<Func<ConsumeEntity,bool>> expression);
        Task<List<ChartsColumnModel>> LoadColumn(Expression<Func<ConsumeEntity, bool>> expression);
        Task<List<ChartsColumnModel>> LoadMonthColumn(Expression<Func<ConsumeEntity, bool>> expression);
    }
}
