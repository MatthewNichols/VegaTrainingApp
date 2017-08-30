using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vega.Db;
using Vega.Models;
using Vega.Models.ApiResources;

namespace Vega.Controllers
{
    [Produces("application/json")]
    [Route("api/vehicles")]
    public class VehiclesController : Controller
    {
	    private readonly IMapper _mapper;
	    private readonly VegaDbContext _dbContext;

	    public VehiclesController(IMapper mapper, VegaDbContext dbContext)
	    {
		    _mapper = mapper;
		    _dbContext = dbContext;
	    }

        [HttpPost]
	    public async Task<IActionResult> CreateVehicle([FromBody]VehicleResource vehicleResource)
        {
	        var vehicle = _mapper.Map<VehicleResource, Vehicle>(vehicleResource);

            _dbContext.Vehicles.Add(vehicle);
	        await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<Vehicle, VehicleResource>(vehicle));
	    }
    }
}