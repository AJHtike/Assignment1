using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LifeLogApp
{
    class EventInformations
    {
        
        public string eventID { get; set; }
        public string eventText { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string datetime { get; set; }

        //public EventInformations(System.Xml.Linq.XElement ele)
        //{
        //    Console.WriteLine(ele);

        //}
    }
}
