using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Techment.MyEmployeeModule.Data.Models;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace Techment.MyEmployeeModule.Data.Repositories;
public class MyNewEmployeeAppRepository: DbContextRepositoryBase<MyEmployeeModuleDbContext>, IMyNewEmployeeAppRepository
{
    public MyNewEmployeeAppRepository(MyEmployeeModuleDbContext dbContext)
        : base(dbContext)
    {
    }
    public IQueryable<EmployeeEntity> Employees => DbContext.Set<EmployeeEntity>();
    public async Task<IList<EmployeeEntity>> GetEmployeesByIdsAsync(IEnumerable<string> ids)
    {
        if (ids.IsNullOrEmpty())
        {
            return new List<EmployeeEntity>();
        }

        return await Employees.Where(x => ids.Contains(x.Id)).ToListAsync();
    }
}   
