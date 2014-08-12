using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Threading.Tasks;
using Wp8MapExtensions.Model;

namespace Wp8MapExtensions.Repository
{
    public class PlaneRepository : IPlaneRepository
    {
        private const int Count = 350;
        private const double X = 24.9375d;
        private const double Y = 60.170833;
        private const int Radius = 8000;
        private const int BearingMin = 0;
        private const int BearingMax = 359;
        private readonly Random _random = new Random();

        public Task<IEnumerable<IPlane>> GetAllPlanesAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                var planes = new List<IPlane>();

                for (int i = 0; i < Count; i++)
                    planes.Add(new RealTimePlane(GetLocation(X, Y, Radius), _random.Next(BearingMin, BearingMax)));

                return planes as IEnumerable<IPlane>;
            });

        }

        // Taken from http://gis.stackexchange.com/questions/25877/how-to-generate-random-locations-nearby-my-location
        private GeoCoordinate GetLocation(double currentX, double currentY, int radius)
        {
            double radiusInDegrees = radius / 111000f;
            double w = radiusInDegrees * Math.Sqrt(_random.NextDouble());
            double t = 2 * Math.PI * _random.NextDouble();

            double newY = w * Math.Sin(t);
            double newX = (w * Math.Cos(t)) / Math.Cos(currentY);

            double longitude = newX + currentX;
            double latitude = newY + currentY;

            return new GeoCoordinate(latitude, longitude);
        }
    }
}
