using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace EnUcuzUrun.Net5.Res.Api.Model
{
    public class Failure<T> : Response<T>
    {
        public Failure(string comment, string message = "Hata")
        {
            Result = false;
            ResultCode = -1;
            Message = message;
            Comment = comment;
        }
    }
}
