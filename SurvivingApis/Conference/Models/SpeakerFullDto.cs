using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conference.Models
{
    public class SpeakerFullDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Twitter { get; set; }
        public string Github { get; set; }
    }
}
