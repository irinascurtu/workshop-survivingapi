using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain
{
    public partial class Speaker
    {
        public Speaker()
        {
            Talks = new HashSet<Talk>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Email { get; set; }
        public string LinkedIn { get; set; }
        public string Skype { get; set; }
        public string GitHub { get; set; }
        public string Twitter { get; set; }
        public string CompanyName { get; set; }
        public string CompanyWebsite { get; set; }
        public string Description { get; set; }
   

        public virtual ICollection<Talk> Talks { get; set; }
    }
}
