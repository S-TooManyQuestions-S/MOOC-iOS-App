using System;
using System.Collections.Generic;
using System.Text;

namespace Stepik.APIser
{
    class Meta
    {
        public int Page { get; } //Текущая страница при десериализации
        public bool HasNext { get; }//Имеет ли текущая страница следюущую
        public Meta(int page, bool has_next)
        {
            Page = page;
            HasNext = has_next;
        }
    }
}
