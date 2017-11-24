using Vega.Db;
using Vega.Interfaces;
using Vega.Models;

namespace Vega.Repositories
{
	public class ModelRepository : BaseRepository<Model>, IModelsRepository {
		public ModelRepository(VegaDbContext dbContext) : base(dbContext, dbContext.Models)
		{
		}
	}
}