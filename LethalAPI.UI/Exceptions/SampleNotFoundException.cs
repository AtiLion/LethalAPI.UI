using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalAPI.UI.Exceptions
{
    public class SampleNotFoundException : Exception
    {
        public SampleNotFoundException(string id) : base("Sample not found: " + id) {}
    }
}
