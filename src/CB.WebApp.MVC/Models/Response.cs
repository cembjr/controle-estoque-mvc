using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CB.WebApp.MVC.Models
{
    public class ResponseResult<T> : IResponseErros
        where T : class
    {
        public ResponseResult()
        {
            Result = Activator.CreateInstance<T>();
            ErrosResponse = new ErrosResponse();
        }
        public bool Sucesso { get; set; }

        public T Result { get; set; }

        public ErrosResponse ErrosResponse { get; set; }
    }

    public interface IResponseErros
    {
        public ErrosResponse ErrosResponse { get; set; }
    }

    public interface IResponse { }
}
