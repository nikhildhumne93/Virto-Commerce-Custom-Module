using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Techment.MyEmployeeModule.Core.Models;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Domain;

namespace Techment.MyEmployeeModule.Data.Models;
public class EmployeeEntity : AuditableEntity, IDataEntity<EmployeeEntity, Employee>
{
    [StringLength(50)]
    public string FirstName { get; set; }
    [StringLength(50)]
    public string LastName { get; set; }
    //[StringLength(250)]
    public int Age { get; set; }
    //[StringLength(250)]
    public int Salary { get; set; }
    [StringLength(10)]
    public string Gender { get; set; }
    public Employee ToModel(Employee model)
    {
        model.Id = Id;
        model.FirstName = FirstName;
        model.LastName = LastName;
        model.Gender = Gender;
        model.Age = Age;
        model.Salary = Salary;
        model.CreatedBy = CreatedBy;
        model.CreatedDate = CreatedDate;
        model.ModifiedBy = CreatedBy;
        model.ModifiedDate = ModifiedDate;

        return model;
    }

    public EmployeeEntity FromModel(Employee model, PrimaryKeyResolvingMap pkMap)
    {
        pkMap.AddPair(model, this);

        Id = model.Id;
        FirstName = model.FirstName;
        LastName = model.LastName;
        Gender = model.Gender;
        Age = model.Age;
        Salary = model.Salary;

        return this;
    }

    public void Patch(EmployeeEntity target)
    {
        target.FirstName = FirstName;
        target.LastName = LastName;
        target.Gender = Gender;
        target.Age = Age;
        target.Salary = Salary;
    }
}


