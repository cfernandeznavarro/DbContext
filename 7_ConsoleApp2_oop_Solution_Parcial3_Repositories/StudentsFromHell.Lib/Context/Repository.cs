using Academy.Lib.Infrastructure;
using Academy.Lib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Academy.Lib.Context
{
    public class Repository<T> where T : Entity
    {
        //private static Dictionary<Guid, T> DbSet { get; set; } = new Dictionary<Guid, T>();

        private AcademyDbContext DbContext { get; set; }

        private DbSet<T> DbSet
        {
            get
            {
                return DbContext.Set<T>();
            }
        }

        public Repository()
        {
            DbContext = new AcademyDbContext();
        }


        public IQueryable<T> QueryAll()
        {
            return DbContext.Set<T>().AsQueryable<T>();
        }

        public T Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual SaveResult<T> Add(T entity)
        {
            var output = new SaveResult<T>();

            if (entity.Id != default)
            {
                output.IsSuccess = false;
                output.Validation.Errors.Add("Ya existe una entity con ese id");

            }
               
            entity.Id = Guid.NewGuid();

            DbContext.Set<T>().Add(entity);
            //this.DbSet.Add(entity);


            DbContext.SaveChanges();

            output.IsSuccess = true;
            output.Entity = entity;

            return output;

        }

        public virtual SaveResult<T> Update(T entity)
        {
            var output = new SaveResult<T>();

            if (entity.Id == default(Guid))
            {
                output.IsSuccess = false;
                output.Validation.Errors.Add("No se puede actualizar una entidad sin Id");
            }

            if (entity.Id != default(Guid) && !DbSet.Any(x => x.Id == entity.Id))
            {
                output.IsSuccess = false;
                output.Validation.Errors.Add("No existe una entity con ese id");
            }

            if (output.IsSuccess)
            {

               var entitywithId = DbSet.Find(entity.Id);
               entitywithId = entity;
            }

            return output;
        }
    }
}
