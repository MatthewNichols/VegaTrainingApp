using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.Models.ApiResources
{
    public class VehicleResource
    {
	    public VehicleResource()
	    {
		    Features = new List<int>();
	    }

        public int Id { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public bool IsRegistered { get; set; }
	    public ContactResource Contact { get; set; }
        public ICollection<int> Features { get; set; }
    }

	public class ContactResource
	{
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
