using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techment.MyEmployeeModule.Core.Events;
using Techment.MyEmployeeModule.Core.Models;
using Techment.MyEmployeeModule.Data.Models;
using Techment.MyEmployeeModule.Data.Repositories;
using VirtoCommerce.Platform.Core.Caching;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Events;
using VirtoCommerce.Platform.Data.GenericCrud;

namespace Techment.MyEmployeeModule.Data.Services.Crud;
public class EmployeeService : CrudService<Employee, EmployeeEntity, EmployeeChangingEvent, EmployeeChangedEvent>
{
    public EmployeeService(Func<IMyNewEmployeeAppRepository> repositoryFactory,
        IPlatformMemoryCache platformMemoryCache,
        IEventPublisher eventPublisher) : base(repositoryFactory, platformMemoryCache, eventPublisher)
    {

    }

    protected override async Task<IList<EmployeeEntity>> LoadEntities(IRepository repository, IList<string> ids, string responseGroup)
    {
        var departmentEntities = await ((IMyNewEmployeeAppRepository)repository).GetEmployeesByIdsAsync(ids);
        return departmentEntities.ToList();
    }
}
