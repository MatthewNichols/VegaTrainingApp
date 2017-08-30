using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Db;
using Vega.Models;
using Vega.Models.ApiResources;

namespace Vega.Controllers
{
    [Produces("application/json")]
    public class FeatureController : Controller
    {
	    private readonly VegaDbContext _context;
	    private readonly IMapper _mapper;

	    public FeatureController(VegaDbContext context, IMapper mapper)
	    {
		    _context = context;
		    _mapper = mapper;
	    }

        [HttpGet("/api/features")]
	    public async Task<IEnumerable<FeatureResource>> GetMakes()
        {
	        var features = await _context.Features.ToListAsync();
	        return _mapper.Map<List<Feature>, List<FeatureResource>>(features);
        }

    }
}