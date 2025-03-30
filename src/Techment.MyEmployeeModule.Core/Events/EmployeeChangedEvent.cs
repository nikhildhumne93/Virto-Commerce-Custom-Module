using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techment.MyEmployeeModule.Core.Models;
using VirtoCommerce.Platform.Core.Events;

namespace Techment.MyEmployeeModule.Core.Events;
public class EmployeeChangedEvent : GenericChangedEntryEvent<Employee>
{
    public EmployeeChangedEvent(IEnumerable<GenericChangedEntry<Employee>> changedEntries)
        : base(changedEntries)
    {
    }
}
 
