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
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            UIEnabled(false);
                            _pushpinLayer.Clear();
                        });

                    Task.Factory.StartNew(() =>
                        {
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

                            DispatcherHelper.CheckBeginInvokeOnUI(() => UIEnabled(true));
                        });
                });
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
                                Style = Resources["PlaneStyle"] as Style
                            };

                        var pushpinOverlay = new MapOverlay
                            {
                                GeoCoordinate = plane.Location,
                                Content = pushpin
                            };
                        _pushpinLayer.Add(pushpinOverlay);
                    }
                });
        }

        private void UIEnabled(bool isEnabled)
        {
            InstantRefreshButton.IsEnabled = DelayedRefreshButton.IsEnabled = isEnabled;
        }
    }
}
