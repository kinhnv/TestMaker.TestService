using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.Common.Models
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            Successful = true;
            Errors = new List<string>();
        }
        public ServiceResult(string error)
        {
            Successful = false;
            Errors = new List<string> { error };
        }
        public ServiceResult(List<string> errors)
        {
            Successful = false;
            Errors = errors;
        }

        public bool Successful { get; set; }

        public List<string> Errors { get; set; }

    }
    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult(T data)
            : base()
        {
            Data = data;
        }
        public ServiceResult(string error)
            : base(error)
        {
            Data = default;
        }

        public ServiceResult(List<string> errors)
            : base(errors)
        {
            Data = default;
        }

        public T Data { get; set; }
    }
}
