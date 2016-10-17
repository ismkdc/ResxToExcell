using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxToExcell
{
    class Resx
    {
        public string FileName { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public static List<Resx> resx_List = new List<Resx>();
    }
}
