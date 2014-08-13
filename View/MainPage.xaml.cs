using System.Linq;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Toolkit;
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