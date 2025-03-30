using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techment.MyEmployeeModule.Data.Models;
using VirtoCommerce.Platform.Core.Common;

namespace Techment.MyEmployeeModule.Data.Repositories;
public interface IMyNewEmployeeAppRepository : IRepository
{
    IQueryable<EmployeeEntity> Employees { get; }
    Task<IList<EmployeeEntity>> GetEmployeesByIdsAsync(IEnumerable<string> ids);
}
