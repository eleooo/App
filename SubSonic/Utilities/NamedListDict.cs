using System;
using System.Collections.Generic;
using System.Text;

namespace SubSonic
{
    public class NamedListDict : System.Collections.Specialized.ListDictionary
    {
        public NamedListDict(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
