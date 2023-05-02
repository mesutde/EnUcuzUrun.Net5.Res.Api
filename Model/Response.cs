using System.Collections.Generic;
using System;

namespace EnUcuzUrun.Net5.Res.Api.Model
{
    public class Response<T>
    {
        public bool Result { get; set; }
        public int ResultCode { get; set; }
        public string Message { get; set; }

        public string Comment { get; set; }

        public string UpdateTime { get; set; }

        public IEnumerable<T> Data { get; set; }

        public Response()
        { }

        public Response(IEnumerable<T> data)
        {
            Result = true;
            Message = "İşlem Başarıyla tamamlandı";
            Data = data;
        }

        public Response(Exception exc)
        {
            Result = false;
            Message = exc.Message;
        }
    }
}
