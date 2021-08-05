using Repository.DAOs.Collections;
using Repository.Exceptions;
using Repository.Persistence.Models;

namespace Repository.DAOs.GenericDAO.DatasetDAO
{
    public interface IDatasetDAO : IDAO<Dataset>
    {
        /// <exception cref="PageSizeException"/>
        /// <exception cref="PageIndexException"/>
        PageList<Dataset> FindPageByTerm(int pageSize, int pageIndex, string searchTerm);

        /// <exception cref="PageSizeException"/>
        /// <exception cref="PageIndexException"/>
        PageList<Dataset> FindPageByTermAndUser(int pageSize, int pageIndex, string searchTerm, long userId);
    }
}