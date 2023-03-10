using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Libary.Dtos
{
    public class ErrorDto
    {
        public List<string> Errors { get;private set; } = new List<string>();
        public bool IsShow { get; private set; }


        public ErrorDto(string error, bool isShow)
        {
            Errors.Add(error);
            this.IsShow = isShow;
            //this.IsShow = true;
        }

        public ErrorDto(List<string> errors, bool isShow)
        {
            Errors = errors;
            this.IsShow = isShow;
        }
    }
}
