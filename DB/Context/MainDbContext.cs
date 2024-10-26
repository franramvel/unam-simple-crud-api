using System;
using System.Collections.Generic;
using DB;
using Microsoft.EntityFrameworkCore;

namespace DB.Context;

public partial class MainDbContext : DbContext
{
    public MainDbContext()
    {
    }

    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }



}
