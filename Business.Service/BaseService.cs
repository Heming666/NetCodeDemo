using Business.IService.Base;
using Microsoft.EntityFrameworkCore;
using Repository.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public class BaseService<TEntity> : IBaseService where TEntity : class, new()
    {
        private IRepositoryFactory _repositoryFactory;
        public IRepository<TEntity> Repository;
        private DbContext _mydbcontext;

        public BaseService(IRepositoryFactory repositoryFactory, DbContext mydbcontext):base()
        {
            this._repositoryFactory = repositoryFactory;
           this.Repository= _repositoryFactory.CreateRepository<TEntity>(mydbcontext);
            this._mydbcontext = mydbcontext;
        }
        public BaseService()
        {
           this.Repository= this.CreateService<TEntity>();
        }
        public IRepository<T> CreateService<T>() where T : class, new()
        {
            return _repositoryFactory.CreateRepository<T>(_mydbcontext);
        }
    }
}
