using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Wp8MapExtensions.Message;
using Wp8MapExtensions.Model;
using Wp8MapExtensions.Repository;

namespace Wp8MapExtensions.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private bool _uiEnabled = true;

        public MainViewModel()
            :this(new PlaneRepository())
        {}

        public MainViewModel(IPlaneRepository planeRepository)
        {
            PlaneRepository = planeRepository;

            InstantRefreshCommand = new RelayCommand(async () => SendMessage());

            DelayedRefreshCommand = new RelayCommand(async () => SendMessage(100));
        }

        private async void SendMessage(int? delay = null)
        {
            Planes = (await PlaneRepository.GetAllPlanesAsync()).ToList();
            Messenger.Default.Send(new RefreshPlanesMessage(Planes, delay));
        }

        public IEnumerable<IPlane> Planes { get; set; } 

        public IPlaneRepository PlaneRepository { get; set; }

        public ICommand InstantRefreshCommand { get; set; }

        public ICommand DelayedRefreshCommand { get; set; }

        public bool UIEnabled
        {
            get { return _uiEnabled; }
            set { _uiEnabled = value; NotifyPropertyChanged(); }
        }
    }
}
