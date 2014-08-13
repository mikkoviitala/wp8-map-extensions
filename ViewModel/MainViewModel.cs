using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Wp8MapExtensions.Message;
using Wp8MapExtensions.Repository;

namespace Wp8MapExtensions.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
            :this(new PlaneRepository())
        {}

        public MainViewModel(IPlaneRepository planeRepository)
        {
            PlaneRepository = planeRepository;

            InstantRefreshCommand = new RelayCommand(() => SendMessage());
            DelayedRefreshCommand = new RelayCommand(() => SendMessage(75));
        }

        private async void SendMessage(int? delay = null)
        {
            var planes = (await PlaneRepository.GetAllPlanesAsync()).ToList();
            Messenger.Default.Send(new RefreshPlanesMessage(planes, delay));
        }

        public IPlaneRepository PlaneRepository { get; set; }

        public ICommand InstantRefreshCommand { get; set; }

        public ICommand DelayedRefreshCommand { get; set; }
    }
}
