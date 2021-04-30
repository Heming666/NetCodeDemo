using Business.IService.Base;
using Microsoft.EntityFrameworkCore;
using Repository.Entity.Models;
using Repository.Factory;
using System;

namespace Business.Service
{
    public class UserService : IDataBase<UserEntity>, IDepartmentService
    {
        public UserService(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
