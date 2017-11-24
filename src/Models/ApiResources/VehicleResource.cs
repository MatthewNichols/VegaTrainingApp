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
		    Features = new List<KeyValuePairResource>();
	    }

        public int Id { get; set; }
        public KeyValuePairResource Model { get; set; }
	    public KeyValuePairResource Make { get; set; }
        public bool IsRegistered { get; set; }
	    public ContactResource Contact { get; set; }
        public ICollection<KeyValuePairResource> Features { get; set; }
    }
}
