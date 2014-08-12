using System.Device.Location;

namespace Wp8MapExtensions.Model
{
    public interface IPlane
    {
        GeoCoordinate Location { get; set; }
        double Bearing { get; set; }
    }
}