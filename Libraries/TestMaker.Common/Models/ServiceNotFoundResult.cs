using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Constants;

namespace TestMaker.Common.Models
{
    public class ServiceNotFoundResult : ServiceResult
    {
        public ServiceNotFoundResult()
            : base(string.Format(SERVICE_RESULT_CONSTANTS.NOT_FOUND_WITH_PARAMS_MODEL_AND_ID, "object"))
        {

        }

        public ServiceNotFoundResult(Guid id)
            : base(string.Format(SERVICE_RESULT_CONSTANTS.NOT_FOUND_WITH_PARAMS_MODEL_AND_ID, "object", id))
        {

        }

        public ServiceNotFoundResult(int id)
            : base(string.Format(SERVICE_RESULT_CONSTANTS.NOT_FOUND_WITH_PARAMS_MODEL_AND_ID, "object", id))
        {

        }

        public ServiceNotFoundResult(string id)
            : base(string.Format(SERVICE_RESULT_CONSTANTS.NOT_FOUND_WITH_PARAMS_MODEL_AND_ID, "object", id))
        {

        }
    }

    public class ServiceNotFoundResult<T> : ServiceResult<T>
    {
        public ServiceNotFoundResult()
            : base(string.Format(SERVICE_RESULT_CONSTANTS.NOT_FOUND_WITH_PARAMS_MODEL_AND_ID, typeof(T).Name))
        {

        }

        public ServiceNotFoundResult(Guid id)
            : base(string.Format(SERVICE_RESULT_CONSTANTS.NOT_FOUND_WITH_PARAMS_MODEL_AND_ID, typeof(T).Name, id))
        {

        }

        public ServiceNotFoundResult(int id)
            : base(string.Format(SERVICE_RESULT_CONSTANTS.NOT_FOUND_WITH_PARAMS_MODEL_AND_ID, typeof(T).Name, id))
        {

        }

        public ServiceNotFoundResult(string id)
            : base(string.Format(SERVICE_RESULT_CONSTANTS.NOT_FOUND_WITH_PARAMS_MODEL_AND_ID, typeof(T).Name, id))
        {
            
        }
    }
}
