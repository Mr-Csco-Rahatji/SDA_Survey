using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Survey
{
    class user
    {
        private string full_names { get; set; }
        private string email { get; set; }
        private DateTime dob { get; set; }
        string contact { get; set; }
        private LinkedList<likes>   likes    { get; set;}
    }
}
