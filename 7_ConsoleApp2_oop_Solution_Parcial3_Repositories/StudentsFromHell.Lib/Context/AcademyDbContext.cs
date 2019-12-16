using Academy.Lib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Academy.Lib.Context
{
    public class AcademyDbContext : DbContext
    {

        static string DbConnection = "Data Source=C:\\Users\\Cristina\\Desktop\\C#\\Practica Alumnos\\7_ConsoleApp2_oop_Solution_Parcial3_Repositories\\ConsoleApp1\\academy.db";
        static string AssemblyName = "Academy.App";


        static DbContextOptionsBuilder _optionsBuidler;
        public static DbContextOptionsBuilder OptionsBuilder
        {
            get
            {
                if (_optionsBuidler == null)
                {
                    var optionsBuilder = new DbContextOptionsBuilder<AcademyDbContext>();
                    optionsBuilder.UseSqlite(DbConnection, b => b.MigrationsAssembly(AssemblyName));

                    _optionsBuidler = optionsBuilder;
                }

                return _optionsBuidler;
            }
            set
            {
                _optionsBuidler = value;
            }

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public AcademyDbContext()
            : base(OptionsBuilder.Options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Entity>()
                .Ignore(x => x.CurrentValidation);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(DbConnection, b => b.MigrationsAssembly(AssemblyName));

        }
    }
}