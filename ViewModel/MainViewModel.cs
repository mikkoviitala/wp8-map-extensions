using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
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
            Planes = new ObservableCollection<IPlane>();

            RefreshPlanesCommand = new RelayCommand(async () =>
                {
                    var planes = (await PlaneRepository.GetAllPlanesAsync()).ToList();
                    if (Planes.Any())
                        Planes.Clear();

                    foreach (var item in planes)
                        Planes.Add(item);

                    NotifyPropertyChanged("Planes");
                });
        }

        public ObservableCollection<IPlane> Planes { get; set; }

        public IPlaneRepository PlaneRepository { get; set; }

        public ICommand RefreshPlanesCommand { get; set; }
    }
}
