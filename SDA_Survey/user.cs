using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Survey
{
    enum Rating
    {
        s
    }
    class User
    {
        public string full_names { get; set; }
        public string email { get; set; }
        public DateTime dob { get; set; }
        public string contact { get; set; }
        public List<FavFood> foodList { get; set; }
        public List<Likes> likes;

    }
}
