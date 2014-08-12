using System.ComponentModel;
using System.Device.Location;
using System.Runtime.CompilerServices;

namespace Wp8MapExtensions.Model
{
    public class RealTimePlane : IPlane, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private GeoCoordinate _location;
        private int _bearing;

        public RealTimePlane()
        {}

        public RealTimePlane(GeoCoordinate location, int bearing)
        {
            Location = location;
            Bearing = bearing;
        }

        public GeoCoordinate Location
        {
            get { return _location; }
            set { _location = value; OnPropertyChanged(); }
        }

        public int Bearing
        {
            get { return _bearing; }
            set { _bearing = value; OnPropertyChanged();}
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) 
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
