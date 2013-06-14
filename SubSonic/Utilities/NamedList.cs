using System;
using System.Collections.Generic;
using System.Text;

namespace SubSonic
{
    public class NamedList<T> : List<T>
    {
        public NamedList(string name)
        {
            this.Name = name;
        }
    
        public string Name { get; set; }
    }
}
