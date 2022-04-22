using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataResults.Abstract
{
    public interface IDataResult<out T>
    {
        T Result{get;}
        bool Success { get; set; }
        Exception Error { get; set; } 
    }
}