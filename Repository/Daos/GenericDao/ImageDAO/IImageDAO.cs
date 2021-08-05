using Repository.Exceptions;
using Repository.Persistence.Models;
using System.Collections.Generic;

namespace Repository.DAOs.GenericDAO.ImageDAO
{
    public interface IImageDAO : IDAO<Image>
    {
        /// <exception cref="InstanceNotFoundException"/>
        Image FindCover(long datasetId);

        IList<Image> FindByDatasetId(long datasetId);
    }
}