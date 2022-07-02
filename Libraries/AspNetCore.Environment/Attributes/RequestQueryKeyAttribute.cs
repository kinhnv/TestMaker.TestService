using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Environment.Attributes
{
    public class RequestQueryKeyAttribute: Attribute
    {
        private readonly string _key;

        public RequestQueryKeyAttribute(string key)
        {
            _key = key;
        }
    }
}
