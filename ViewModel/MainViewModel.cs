using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Wp8MapExtensions.Model;
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

            RefreshPlanesCommand = new RelayCommand(async () =>
                {
                    var planes = (await PlaneRepository.GetAllPlanesAsync()).ToList();
                    Messenger.Default.Send(new PropertyChangedMessage<IEnumerable<IPlane>>(null, planes, "Planes"));
                });
        }

        public IPlaneRepository PlaneRepository { get; set; }

        public ICommand RefreshPlanesCommand { get; set; }
    }
}
