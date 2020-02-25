using System.Collections.Generic;

namespace api.Models.Dtos
{
    public class ListResultDto<T>
    {
        public int Total {get; set;}
        public List<T> Items {get; set;}
    }
}