using System;
using System.Collections.Generic;

namespace EmployeeApp.Models;

public partial class EmployeeSalary
{
    public int SalaryId { get; set; }

    public int? EmployeeId { get; set; }

    public double? Salary { get; set; }

    public virtual Employee? Employee { get; set; }
}
