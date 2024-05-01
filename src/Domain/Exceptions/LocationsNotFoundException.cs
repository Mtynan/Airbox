using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class LocationsNotFoundException : Exception
    {
        public LocationsNotFoundException()
            : base("No locations found for the user.")
        {
        }

        public LocationsNotFoundException(string message)
        : base(message)
        {
        }

    }
}
