using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webinar.Models
{
    public class orgtTress
    {
        public int id { get; set; }
        public string name { get; set; }
       

    }

    public class child: orgtTress
    {
        public int reportingto { get; set; }
    }


    public class orgtree
    {
        public int Id { get; set; }
        public int reportingto { get; set; }
    
        public string Name { get; set; }
        public List<orgtree> Children { get; set; }
    }
}