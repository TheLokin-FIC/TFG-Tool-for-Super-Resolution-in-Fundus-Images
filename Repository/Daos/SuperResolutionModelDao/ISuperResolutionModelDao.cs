using Repository.Persistence.Models;
using System.Collections.Generic;

namespace Repository.DAOs.SuperResolutionModelDAO
{
    public interface ISuperResolutionModelDAO : IDAO<SuperResolutionModel>
    {
        IList<SuperResolutionModel> FindByModelId(int modelId);
    }
}