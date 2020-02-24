using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Data.Helpers
{

    public class SpeakerResourceParameters
    {
        const int maxPageSize = 20;
      
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

    }
}
