using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Environment.Options
{
    public interface IModifiableOptions<TOptions> : IOptions<TOptions> where TOptions : class
    {
    }
}
