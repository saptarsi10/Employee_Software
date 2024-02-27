using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Models;

public partial class EmployeeAppDbContext : DbContext
{
    public EmployeeAppDbContext()
    {
    }

    public EmployeeAppDbContext(DbContextOptions<EmployeeAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeSalary> EmployeeSalaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:ConStr");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F119B10A179");

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<EmployeeSalary>(entity =>
        {
            entity.HasKey(e => e.SalaryId).HasName("PK__Employee__4BE20457169E4154");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeSalaries)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__EmployeeS__Emplo__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
