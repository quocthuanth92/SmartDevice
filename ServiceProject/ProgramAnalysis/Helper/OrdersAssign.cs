using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramAnalysis.Helper
{
    public class OrdersAssign
    {
        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public static double CalculateDistance(Location location1, Location location2)
        {
            double circumference = 40000.0; // Earth's circumference at the equator in km
            double distance = 0.0;

            //Calculate radians
            double latitude1Rad = DegreesToRadians(location1.Latitude);
            double longitude1Rad = DegreesToRadians(location1.Longitude);
            double latititude2Rad = DegreesToRadians(location2.Latitude);
            double longitude2Rad = DegreesToRadians(location2.Longitude);

            double logitudeDiff = Math.Abs(longitude1Rad - longitude2Rad);

            if (logitudeDiff > Math.PI)
            {
                logitudeDiff = 2.0 * Math.PI - logitudeDiff;
            }

            double angleCalculation =
                Math.Acos(
                  Math.Sin(latititude2Rad) * Math.Sin(latitude1Rad) +
                  Math.Cos(latititude2Rad) * Math.Cos(latitude1Rad) * Math.Cos(logitudeDiff));

            distance = circumference * angleCalculation / (2.0 * Math.PI);

            return distance;
        }

        public static double CalculateDistance(params Location[] locations)
        {
            double totalDistance = 0.0;

            for (int i = 0; i < locations.Length - 1; i++)
            {
                Location current = locations[i];
                Location next = locations[i + 1];

                totalDistance += CalculateDistance(current, next);
            }

            return totalDistance;
        }

        public static IEnumerable<Location> GetTopDeliveryVehicles(Location Mark, IEnumerable<Location> list)
        {
            IEnumerable<Location> result = new List<Location>();
            foreach (Location elm in list)
            {
                elm.Distance = CalculateDistance(Mark, elm);
            }
            result = list.OrderByDescending(x => x.Distance).Take(Utility.NumDeliveryVehicles).ToList();
            return result;
        }
    }
}