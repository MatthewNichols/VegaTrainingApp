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
    public class MakesController : Controller
    {
	    private readonly VegaDbContext _context;
	    private readonly IMapper _mapper;

	    public MakesController(VegaDbContext context, IMapper mapper)
	    {
		    _context = context;
		    _mapper = mapper;
	    }

        [HttpGet("/api/makes")]
	    public async Task<IEnumerable<MakeResource>> GetMakes()
        {
	        var makes = await _context.Makes.Include(m => m.Models).ToListAsync();
	        return _mapper.Map<List<Make>, List<MakeResource>>(makes);
        }

    }
}