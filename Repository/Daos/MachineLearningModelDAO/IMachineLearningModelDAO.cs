using Repository.DAOs.Collections;
using Repository.Exceptions;
using Repository.Persistence.Models;

namespace Repository.DAOs.MachineLearningModelDAO
{
    public interface IMachineLearningModelDAO : IDAO<MachineLearningModel>
    {
        /// <exception cref="PageSizeException"/>
        /// <exception cref="PageIndexException"/>
        PageList<MachineLearningModel> FindPageByTerm(int pageSize, int pageIndex, string searchTerm);
    }
}