using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Square_Coord_Cal;
using System.Linq;
using System.Data.Spatial;
namespace LatLngTest
{
    [TestClass]
    public class UnitTest1
    {


        //[TestMethod]
        public void SameAsSTDistance()
        {
            //DECLARE @g geography;
            //DECLARE @h geography;
            //SET @g = geography::STGeomFromText('POINT(-118.291994 36.578581)', 4326);
            //SET @h = geography::STGeomFromText('POINT(-116.83171 36.23998)', 4326);
            //SELECT @g.STDistance(@h);
            //RESULT SHOULD BE 136261,875853806
            var compared_value = 135994.01790639554;
            var distance = BusinessLogic.DistanceBetweenPlaces(-118.291994, 36.578581, -116.83171, 36.23998);

        }

        [TestMethod]
        public void GetLngTest()
        {
            var point1 = DbGeography.FromText("POINT(11.009618410623007 58.875055537326887)");
            //var point2 = DbGeography.FromText("POINT(11.009618410623007 58.884031960021689)");

            var point2 = DbGeography.FromText("POINT(11.026950410623007 58.875055537326887)");

            double distance = BusinessLogic.DistanceBetweenPlaces((double)point1.Longitude, (double)point1.Latitude, (double)point2.Longitude, (double)point2.Latitude);
            var list = BusinessLogic.Inverse((double)point1.Longitude, (double)point1.Latitude, distance, 0, (double)point2.Latitude);
            if (!list.Any(x => Math.Round(x, 6) == Math.Round((double)point2.Longitude, 6)))
                Assert.Fail("either result list is empty or there is no match");
        }

        [TestMethod]
        public void GetLatTest()
        {
            var point1 = DbGeography.FromText("POINT(11.009618410623007 58.875055537326887)");
            var point2 = DbGeography.FromText("POINT(11.009618410623007 58.884031960021689)");
            double distance = BusinessLogic.DistanceBetweenPlaces((double)point1.Longitude, (double)point1.Latitude, (double)point2.Longitude, (double)point2.Latitude);

            var list = BusinessLogic.Inverse((double)point1.Longitude, (double)point1.Latitude, distance, (double)point2.Longitude, 0);
            if (!list.Any(x => Math.Round(x, 6) == Math.Round((double)point2.Latitude, 6)))
                Assert.Fail("either result list is empty or there is no match");

        }

    }
}
