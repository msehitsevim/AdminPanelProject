using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataResults.Abstract;

namespace Core.DataResults.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public T Result { get; set; }
        public bool Success { get; set; }
        public Exception Error { get; set; }
        public DataResult()
        {
            Success = false;
        }

        public DataResult(T result,bool success, Exception error)
        {
            Success = success;
            Error = error;
            Result = result;
        }
}
}