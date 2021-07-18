using Repository.DAOs.Collections;
using Repository.Exceptions;
using Repository.Persistence.Models;
using System.Collections.Generic;

namespace Repository.DAOs.MachineLearningModelDAO
{
    public interface IMachineLearningModelDAO : IDAO<MachineLearningModel>
    {
        IList<MachineLearningModel> FindByName(string name);

        /// <exception cref="PageSizeException"/>
        /// <exception cref="PageIndexException"/>
        PageList<MachineLearningModel> FindPageByTerm(int pageSize, int pageIndex, string searchTerm);
    }
}