using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Techment.MyEmployeeModule.Core.Models;
using Techment.MyEmployeeModule.Data.Models;
using Techment.MyEmployeeModule.Data.Repositories;
using Techment.MyEmployeeModule.Data.Services.Crud;
using VirtoCommerce.Platform.Core.Caching;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.GenericCrud;
using VirtoCommerce.Platform.Data.GenericCrud;

namespace Techment.MyEmployeeModule.Data.Services.Search;
public class EmployeeSearchService : SearchService<EmployeeSearchCriteria, EmployeeSearchResult, Employee, EmployeeEntity>
{
    public EmployeeSearchService(Func<IMyNewEmployeeAppRepository> repositoryFactory,
        IPlatformMemoryCache platformMemoryCache,
        ICrudService<Employee> employeeService, IOptions<CrudOptions> crudOptions) : base(repositoryFactory, platformMemoryCache, employeeService, crudOptions)
    {

    }
    protected override IQueryable<EmployeeEntity> BuildQuery(IRepository repository, EmployeeSearchCriteria criteria)
    {
        var query = ((IMyNewEmployeeAppRepository)repository).Employees;

        if (criteria.ObjectIds != null && criteria.ObjectIds.Count() > 0)
        {
            query = query.Where(x => criteria.ObjectIds.Contains(x.Id));
        }
        if (!string.IsNullOrEmpty(criteria.SearchPhrase))
        {
            query = query.Where(x => x.Id.Contains(criteria.SearchPhrase) ||
                x.FirstName.Contains(criteria.SearchPhrase) || x.LastName.Contains(criteria.SearchPhrase));
        }

        return query;
    }
    protected override IList<SortInfo> BuildSortExpression(EmployeeSearchCriteria criteria)
    {
        var sortInfos = criteria.SortInfos;
        if (sortInfos.IsNullOrEmpty())
        {
            sortInfos = new[]
            {
                    new SortInfo
                    {
                        SortColumn = nameof(EmployeeEntity.CreatedDate),
                        SortDirection = SortDirection.Descending
                    }
                };
        }
        return sortInfos;
    }
}

