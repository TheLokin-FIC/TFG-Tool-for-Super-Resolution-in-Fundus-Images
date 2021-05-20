using Model.Persistence.Models;
using System.Collections.Generic;

namespace Model.Persistence.Daos.SuperResolutionModelDao
{
    public interface ISuperResolutionModelDao : IGenericDao<SuperResolutionModel, int>
    {
        IList<SuperResolutionModel> FindByName(string name);
    }
}