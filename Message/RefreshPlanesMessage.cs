using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using Wp8MapExtensions.Model;

namespace Wp8MapExtensions.Message
{
    public class RefreshPlanesMessage : MessageBase
    {
        public RefreshPlanesMessage()
        {}

        public RefreshPlanesMessage(IEnumerable<IPlane> planes)
            :this(planes, null)
        {}

        public RefreshPlanesMessage(IEnumerable<IPlane> planes, int? delay)
        {
            Planes = planes;
            Delay = delay;
        }

        public IEnumerable<IPlane> Planes { get; set; }

        public int? Delay { get; set; }
    }
}
