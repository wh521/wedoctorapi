using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Core.Vue
{
    /// <summary>
    /// 由于前端一律区分大小写
    /// </summary>
    public class Route
    {

        [JsonIgnoreAttribute]
        public string routeid { get; set; }
        public string path { get; set; }

        public string component { get; set; }

        public string redirect { get; set; }

        public string name { get; set; }

        public bool alwaysShow { get; set; } = false;

        public RouteMeta meta { get; set; }

        public List<Route> children { get; set; }

        public bool hidden { get; set; } = false;
    }
}
