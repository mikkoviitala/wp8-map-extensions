using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using Wp8MapExtensions.Message;
using Wp8MapExtensions.Model;

namespace Wp8MapExtensions.View
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly MapLayer _pushpinLayer;

        public MainPage()
        {
            InitializeComponent();

            _pushpinLayer = new MapLayer();
            MyMap.Layers.Add(_pushpinLayer);

            Messenger.Default.Register<RefreshPlanesMessage>(this, message =>
                Task.Factory.StartNew(() => 
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() => _pushpinLayer.Clear());
                        if (message.Delay != null)
                        {
                            foreach (var plane in message.Planes)
                            {
                                AddPins(new[] {plane});
                                Thread.Sleep(message.Delay.Value);
                            }
                        }
                        else
                        {
                            AddPins(message.Planes);
                        }
                    }));
        }

        private void AddPins(IEnumerable<IPlane> planes)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    foreach (var plane in planes)
                    {
                        var pushpin = new Pushpin
                            {
                                GeoCoordinate = plane.Location, 
                                Style = Resources["PushpinStyle"] as Style
                            };

                        //create MapOverlay with pushpin as content
                        var pushpinOverlay = new MapOverlay
                            {
                                GeoCoordinate = plane.Location,
                                Content = pushpin
                            };
                        _pushpinLayer.Add(pushpinOverlay);
                    }
                });
        }
    }
}