using System.Collections.Generic;
using System.Threading.Tasks;
using Wp8MapExtensions.Model;

namespace Wp8MapExtensions.Repository
{
    public interface IPlaneRepository
    {
        Task<IEnumerable<IPlane>> GetAllPlanesAsync();
    }
}