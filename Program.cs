using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Square_Coord_Cal
{
    class Program
    {

        static void Main(string[] args)
        {

            var distance = 1000;
            var d1 = BusinessLogic.DistanceBetweenPlaces(11.009618410623007, 58.875055537326887, 11.009618410623007, 58.884031960021689);
            Console.WriteLine("test formula one in C#: " + d1);
            var d2 = BusinessLogic.distFrom(58.875055537326887, 11.009618410623007, 58.884031960021689, 11.009618410623007);
            Console.WriteLine("test formula one in java: " + d2);
            Console.WriteLine("now start the inverse process of C# formula");

            var Lng_List = BusinessLogic.Inverse(11.009618410623007, 58.875055537326887, distance, 0, 58.875055537326887);
            Console.WriteLine("retain latitude, result list for longitude: " + Lng_List[0] + " and " + Lng_List[1]);


            var Lat_List = BusinessLogic.Inverse(11.009618410623007, 58.875055537326887, distance, 11.009618410623007, 0);
            Console.WriteLine("retain longitude, result list for latitude: " + Lat_List[0] + " and " + Lat_List[1]);

            Console.ReadLine();
        }
    }

}






