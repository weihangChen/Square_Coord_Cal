using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Square_Coord_Cal
{
    public class BusinessLogic
    {
        const double PIx = Math.PI;
        const double R = 6371000;
        // cos(d) = sin(φА)·sin(φB) + cos(φА)·cos(φB)·cos(λА − λB),
        //  where φА, φB are latitudes and λА, λB are longitudes
        // Distance = d * R
        public static double DistanceBetweenPlaces(double lon1, double lat1, double lon2, double lat2)
        {
            double sLat1 = Math.Sin(Radians(lat1));
            double sLat2 = Math.Sin(Radians(lat2));
            double cLat1 = Math.Cos(Radians(lat1));
            double cLat2 = Math.Cos(Radians(lat2));
            var RDlon2 = Radians(lon2);
            var RDlon1 = Radians(lon1);
            var RD = RDlon1 - RDlon2;
            double cLon = Math.Cos(RD);
            double cosD = sLat1 * sLat2 + cLat1 * cLat2 * cLon;
            double d = Math.Acos(cosD);
            double dist = R * d;
            return dist;
        }

        public static double Radians(double x)
        {
            return x * PIx / 180;
        }


        public static List<double> Inverse(double lon1, double lat1, double dist, double lon2 = 0, double lat2 = 0)
        {
            var resultList = new List<double>();
            double sLat1 = Math.Sin(Radians(lat1));
            double cLat1 = Math.Cos(Radians(lat1));

            double d = dist / R;
            var cosD = Math.Cos(d);
            if (lon2 == 0 && lat2 == 0)
                throw new ArgumentException("both lng and lat can't be 0");
            if (lon2 == 0)
            {
                double sLat2 = Math.Sin(Radians(lat2));
                double cLat2 = Math.Cos(Radians(lat2));

                var tmp = (cosD - sLat1 * sLat2) / cLat1;
                var cLon = tmp / cLat2;
                //so far so good
                //double cLon = Math.Cos(Radians(lon1) - Radians(lon2));
                var RDlon1 = Radians(lon1);
                //two senarios, both positvie and negative angle should be included
                var angle_cLon_A = Math.Acos(cLon);
                var RDlon2_A = RDlon1 - angle_cLon_A;
                var lon2_A = RDlon2_A * 180 / PIx;


                var angle_cLon_B = angle_cLon_A * -1;
                var RDlon2_B = RDlon1 - angle_cLon_B;
                var lon2_B = RDlon2_B * 180 / PIx;

                resultList.Add(lon2_A);
                resultList.Add(lon2_B);

            }

            if (lat2 == 0)
            {
                double cLon = Math.Cos(Radians(lon1) - Radians(lon2));
                var tmp = cLat1 * cLon;
                var a = Math.Pow(sLat1, 2) + Math.Pow(tmp, 2);
                var b = -2 * cosD * sLat1;
                var c = Math.Pow(cosD, 2) - Math.Pow(tmp, 2);

                var lat2Sin = SolveQuadratic(a, b, c);
                foreach (var test in lat2Sin)
                {
                    lat2 = Math.Asin(test) * 180 / PIx;
                    resultList.Add(lat2);

                }
            }
            return resultList;
        }


        public static double distFrom(double lat1, double lng1, double lat2, double lng2)
        {
            double dLat = Radians(lat2 - lat1);
            double dLng = Radians(lng2 - lng1);
            double sindLat = Math.Sin(dLat / 2);
            double sindLng = Math.Sin(dLng / 2);
            double a = Math.Pow(sindLat, 2) + Math.Pow(sindLng, 2)
                    * Math.Cos(Radians(lat1)) * Math.Cos(Radians(lat2));
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double dist = R * c;

            return dist;
        }

        //Quadratic Equation
        public static List<double> SolveQuadratic(double a, double b, double c)
        {
            var resultList = new List<double>();
            double sqrtpart = b * b - 4 * a * c;

            double x, x1, x2, img;

            if (sqrtpart > 0)
            {

                x1 = (-b + System.Math.Sqrt(sqrtpart)) / (2 * a);

                x2 = (-b - System.Math.Sqrt(sqrtpart)) / (2 * a);
                resultList.Add(x1);
                resultList.Add(x2);

            }

            else if (sqrtpart < 0)
            {

                sqrtpart = -sqrtpart;

                x = -b / (2 * a);

                img = System.Math.Sqrt(sqrtpart) / (2 * a);
                throw new Exception("not possible");

            }

            else
            {

                x = (-b + System.Math.Sqrt(sqrtpart)) / (2 * a);

                resultList.Add(x);

            }
            return resultList;
        }
    }
}
