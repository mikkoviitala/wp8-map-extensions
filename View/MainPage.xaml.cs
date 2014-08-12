using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Toolkit;
using Wp8MapExtensions.Model;

namespace Wp8MapExtensions.View
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const string ActiveButtonContent = "Refresh";
        private const string InactiveButtonContent = "Hold on...";

        public MainPage()
        {
            InitializeComponent();

            Messenger.Default.Register<PropertyChangedMessage<IEnumerable<IPlane>>>(this, message =>
                {
                    ToggleControls(false);
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            RefreshButton.Content = InactiveButtonContent;

                            // Workaround: Annoying MapExtension ItemsSource implementation
                            var itemCollection =
                                MapExtensions.GetChildren(MyMap).OfType<MapItemsControl>().FirstOrDefault();
                            if (itemCollection == null)
                                return;

                            if (itemCollection.Items.Any())
                                itemCollection.Items.Clear();

                            foreach (var item in message.NewValue)
                                itemCollection.Items.Add(item);

                            ToggleControls(true);
                        });
                });
        }

        private void ToggleControls(bool isEnabled)
        {
            RefreshButton.IsEnabled = isEnabled;
            RefreshButton.Content = isEnabled ? ActiveButtonContent : InactiveButtonContent;
        }
    }
}