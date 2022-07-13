using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.Common.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<T> OrderRandom<T>(this IEnumerable<T> enumrable)
        {
            return enumrable.Select(x => new
            {
                Order = new Random().Next(1, 100),
                Item = x
            }).OrderBy(a => a.Order).Select(x => x.Item);
        }
    }
}
