using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Common.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public string Name { get; set; }
        public DataNotFoundException() : base() { }

        public DataNotFoundException(string message) : base(message) { }
        public DataNotFoundException(string message, string name) : base(message)
        {
            Name = name;
        }

    }
}
