using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Db;
using Vega.Interfaces;

namespace Vega.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T: class, IHasId
    {
	    protected readonly VegaDbContext DbContext;
        protected DbSet<T> DbSet { get; set; }

	    protected BaseRepository(VegaDbContext dbContext, DbSet<T> dbSet)
	    {
		    DbContext = dbContext;
		    DbSet = dbSet;
	    }

        public virtual IList<T> GetAll()
        {
	        return DbSet.ToList();
        }

	    public virtual T GetById(int id, bool includeRelated = true)
	    {
		    return DbSet.Find(id);
	    }

	    public virtual T Add(T entity)
	    {
		    DbSet.Add(entity);
		    DbContext.SaveChanges();
		    return entity;
	    }

	    public virtual T Delete(T entity)
	    {
		    DbSet.Remove(entity);
		    DbContext.SaveChanges();
		    return entity;
	    }

	    public virtual T Delete(int id)
	    {
		    var entity = GetById(id, includeRelated: false);

		    if (entity == null)
		    {
			    return null;
		    }

            return Delete(entity);
	    }
    }
}
