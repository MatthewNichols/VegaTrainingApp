using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.Interfaces
{
	public interface IRepository<T> where T: class, IHasId
	{
		IList<T> GetAll();
		T GetById(int id, bool includeRelated = true);
		T Add(T entity);
		T Delete(T entity);
		T Delete(int id);
	}
}
