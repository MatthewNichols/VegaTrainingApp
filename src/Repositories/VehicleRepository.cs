using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Vega.Db;
using Vega.Interfaces;
using Vega.Models;

namespace Vega.Repositories
{
	public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
	{
		public VehicleRepository(VegaDbContext dbContext): base(dbContext, dbContext.Vehicles){}

		public override IList<Vehicle> GetAll()
		{
			return DbSet.Include(vehicle => vehicle.Features).ToList();
        }

		public override Vehicle GetById(int id, bool includeRelated = true)
		{
			if (!includeRelated)
			{
				return base.GetById(id, includeRelated: false);
			}

            return DbSet
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefault(vehicle => vehicle.Id == id);
        }
	}
}