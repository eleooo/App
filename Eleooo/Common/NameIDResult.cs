using System;
using System.Collections.Generic;
using System.Web;

namespace Eleooo.Common
{
    public struct NameIDResult
    {
        public int id { get; set; }
        public string name { get; set; }
        public static NameIDResult GetNameIDResult(int _id, string _name)
        {
            return new NameIDResult { id = _id, name = _name };
        }
    }
}