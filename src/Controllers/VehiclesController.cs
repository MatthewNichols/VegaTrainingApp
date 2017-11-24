using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Db;
using Vega.Interfaces;
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
	    private readonly IVehicleRepository _vehicleRepository;
	    private readonly IModelsRepository _modelsRepository;

	    public VehiclesController(IMapper mapper, VegaDbContext dbContext, IVehicleRepository vehicleRepository, IModelsRepository modelsRepository)
        {
            _mapper = mapper;
            _dbContext = dbContext;
	        _vehicleRepository = vehicleRepository;
	        _modelsRepository = modelsRepository;
        }

	    [HttpGet]
	    public IActionResult GetAllVehicles()
	    {
		    var vehicles = _vehicleRepository.GetAll();
            return Ok(_mapper.Map<List<VehicleResource>>(vehicles));
        }

	    [HttpGet("{id}")]
	    public IActionResult GetVehicle(int id)
	    {
		    var vehicle = _vehicleRepository.GetById(id);

		    if (vehicle == null)
		    {
			    return NotFound();
		    }

            return Ok(_mapper.Map<Vehicle, VehicleResource>(vehicle));
        }

	    [HttpPost]
        public IActionResult CreateVehicle([FromBody]SaveVehicleResource vehicleResource)
        {
	        #region Validation
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _modelsRepository.GetById(vehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError(nameof(SaveVehicleResource.ModelId), "Invalid ModelId");
                return BadRequest(ModelState);
            }

            #endregion

            var vehicle = _mapper.Map<Vehicle>(vehicleResource);
            _vehicleRepository.Add(vehicle);

	        vehicle = _vehicleRepository.GetById(vehicle.Id);

            return Ok(_mapper.Map<VehicleResource>(vehicle));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingVehicle = _dbContext.Vehicles.Include(vehicle => vehicle.Features).SingleOrDefault(v => v.Id == id);
            if (existingVehicle == null)
            {
	            return NotFound();
            }

            _mapper.Map(vehicleResource, existingVehicle);
            existingVehicle.LastUpdate  =DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<Vehicle, VehicleResource>(existingVehicle));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
	        var deleted = _vehicleRepository.Delete(id);

	        if (deleted == null)
	        {
		        return NotFound();
	        }

	        return Ok(_mapper.Map<Vehicle, VehicleResource>(deleted));
        }
    }
}