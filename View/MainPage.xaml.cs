using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Toolkit;
using Wp8MapExtensions.Model;
using Wp8MapExtensions.ViewModel;

namespace Wp8MapExtensions.View
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly MainViewModel _mainViewModel = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
            DataContext = _mainViewModel;
            
            var control = MapExtensions.GetChildren(MyMap).OfType<MapItemsControl>().FirstOrDefault();
            if (control != null) 
                control.ItemsSource = _mainViewModel.Planes;
        }
    }
}