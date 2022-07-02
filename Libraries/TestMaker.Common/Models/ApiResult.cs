using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.Common.Models
{
    public class ApiResult
    {
        public ApiResult()
        {
            Code = 200;
            Errors = null;
        }

        public ApiResult(string error)
        {
            Errors = new List<string> { error };
        }

        public ApiResult(List<string> errors)
        {
            Errors = errors;
        }

        public ApiResult(ServiceResult serviceResult)
        {
            if (serviceResult.Errors.Count == 0)
            {
                Errors = null;
            }
            else
            {
                Errors = serviceResult.Errors;
            }
            Code = GetCodeFromServiceResult(serviceResult);
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Errors { get; set; }

        public int Code { get; set; }

        protected virtual int GetCodeFromServiceResult(ServiceResult serviceResult)
        {
            if (serviceResult is ServiceNotFoundResult)
            {
                return 404;
            }
            if (serviceResult.Successful)
            {
                return 200;
            }
            return 500;
        }
    }
    public class ApiResult<T> : ApiResult
    {
        public ApiResult()
            : base()
        {
            Data = default;
        }

        public ApiResult(ServiceResult<T> serviceResult)
            : base(serviceResult)
        {
            Data = serviceResult.Data;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        protected override int GetCodeFromServiceResult(ServiceResult serviceResult)
        {
            if (serviceResult is ServiceNotFoundResult<T>)
            {
                return 404;
            }
            return base.GetCodeFromServiceResult(serviceResult);
        }
    }
}
