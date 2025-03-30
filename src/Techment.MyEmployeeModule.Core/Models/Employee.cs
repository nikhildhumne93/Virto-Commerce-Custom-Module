using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtoCommerce.Platform.Core.Common;

namespace Techment.MyEmployeeModule.Core.Models;
public class Employee : AuditableEntity, ICloneable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public  int Age { get; set; }
    public int Salary { get; set; }
    public string Gender { get; set; }
    public object Clone() => MemberwiseClone();
}

