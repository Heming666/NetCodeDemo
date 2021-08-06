using Business.IService.Base;
using Repository.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Cache;

namespace Demo.Web.Common.Caches
{
    public class DeptCache
    {
        private readonly IDepartmentService departmentService;
        private readonly ICache cache;

        public DeptCache(
            IDepartmentService departmentService,
            ICache cache)
        {
            this.departmentService = departmentService as IDepartmentService;
            this.cache = cache;
        }

        /// <summary>
        /// 从缓存中获取部门数据(所有的部门)
        /// <para>没有的话就从数据库读，并存入到缓存中</para>
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentEntity>> LoadDepts()
        {
            var depts = await cache.GetList<DepartmentEntity>("list_Department");
            if (depts == null)
            {
                depts = await departmentService.GetList(Util.Extension.ExpressionExtension.True<DepartmentEntity>());
                await cache.SetList("list_Department", depts, TimeSpan.FromHours(10));
            }
            return depts;
        }
        public async Task RefreshDepts()
        {
            var depts = await departmentService.GetList(Util.Extension.ExpressionExtension.True<DepartmentEntity>());
            await cache.SetList("list_Department", depts, TimeSpan.FromHours(10));
        }
        public async Task SetDept(DepartmentEntity dept)
        {
            if (await cache.KeyExistsAsync("list_Department"))
            {
                await cache.SetList("list_Department", dept);
            }
            else
            {
                var depts = await departmentService.GetList(Util.Extension.ExpressionExtension.True<DepartmentEntity>());
                await cache.SetList("list_Department", depts, TimeSpan.FromHours(10));
            }
        }


    }
}
