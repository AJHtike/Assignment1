using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace LifeLogApp
{
    public partial class Form1 : Form
    {
        public string dataPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Data\"));

        public List<string> valueList = new List<string>();

        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //set with Bing Map it is faster than any map provider with Gmap
            gmap.MapProvider = BingMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.SetPositionByKeywords("Perth, Australia");

            //getting rid of the little red cross in the middle of the map
            gmap.ShowCenter = false;

            ReadXMLFromFiles();

        }

        public void ReadXMLFromFiles()
        {
            XmlDocument doc = new XmlDocument();
             doc.Load(dataPath + "lifelog-events.xml");

             XDocument xDoc = XDocument.Parse(doc.InnerXml);

            XNamespace ns = "http://www.xyz.org/lifelogevents";

            var evt = from e in xDoc.Descendants(ns +"Event")
                      select new EventInformations
                      {
                          eventID = e.Element(ns + "eventid").Value,
                          eventText = e.Element(ns + "tweet").Element(ns +"text").Value,
                          Lat = e.Element(ns + "tweet").Element(ns +"location").Element(ns +"lat").Value,
                          Lng = e.Element(ns + "tweet").Element(ns +"location").Element(ns + "long").Value,
                          datetime = e.Element(ns + "tweet").Element(ns + "datetimestamp").Value

                      };

            foreach(var eventinfo in evt)
            {
               // Console.WriteLine("EventID: {0}, Title Text: {1}, Latitude: {2}, Longtitude: {3}, DateTime: {4}", eventinfo.eventID, eventinfo.eventText, eventinfo.Lat, eventinfo.Lng, eventinfo.datetime);

                //get all the location lat,lng and put in the library struct
                PointLatLng p = new PointLatLng();
                p.Lat = Convert.ToDouble(eventinfo.Lat);
                p.Lng = Convert.ToDouble(eventinfo.Lng);

                //get the the list of this event
                valueList = AddtoEventListContainer(eventinfo.eventID, eventinfo.eventText, eventinfo.datetime);

                ////Add to the Dictionary
                //keyValueListDict.Add(eventinfo.GetHashCode(), valueList);

                //int key = eventinfo.GetHashCode();

                //output = GetOutputString(new KeyValuePair<int, List<string>>(key, keyValueListDict[key]));

                //Console.WriteLine("{0} \n",output);
                //Console.WriteLine(" Latitude {0} :, Longtitude {1}:", p.Lat, p.Lng);

                //set the markers
                GMapOverlay markers = new GMapOverlay("markers");
                GMapMarker marker = new GMarkerGoogle(new PointLatLng(p.Lat,p.Lng), GMarkerGoogleType.blue_pushpin);

                gmap.Overlays.Add(markers);
                markers.Markers.Add(marker);

            }
            
        }

        //public static string GetOutputString(KeyValuePair<int, List<string>> kvp)
        //{
        //    string separator = "\n ";  //you can change this if needed (maybe a newline?)
        //    string outputString = kvp.Key + ": " + String.Join(separator, kvp.Value);
        //    return outputString;
        //}

        public List<string> AddtoEventListContainer(string eventID, string eventText, string datetime)
        {
            List<string> _items = new List<string>();

            _items.Add(eventID);
            _items.Add(eventText);
            _items.Add(datetime);

            //foreach(var i in _items)
            //{
            //    Console.WriteLine(i);
            //}

            return _items;
        }

        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            Console.WriteLine(String.Format("Marker {0} was clicked. ", item.Tag));
            //Form f = new ChildForm();
            ChildForm f = new ChildForm();
            f.ShowDialog(this);           
            //f.Dispose();
        }


    }
}
