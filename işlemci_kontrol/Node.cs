using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace işlemci_kontrol
{
    public class Node
    {
        public string p { get; set; }
        public string p1 { get; set; }
        public string p2 { get; set; }
        public string p3 { get; set; }
        public Node sonraki { get; set; }   
        public Node onceki { get; set; }
        public Node(string p)
        {
            this.p = p;
            sonraki = null;
            onceki = null;
        }
        public Node(string p1,string p2, string p3) 
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            sonraki=null;
            onceki=null;
        }
    }
}
