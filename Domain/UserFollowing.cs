using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserFollowing
    {
        public string ObserverId { get; set; }
        public AppUser Observer { get; set; } //użytkownik, który będzie obserwatorem

        public string TargetId { get; set; }
        public AppUser Target {  get; set; }
    }
}
