using Repository.DAOs.Collections;
using Repository.Exceptions;
using Repository.Persistence.Models;
using System.Collections.Generic;

namespace Repository.DAOs.GenericDAO.MachineLearningModelDAO
{
    public interface IMachineLearningModelDAO : IDAO<MachineLearningModel>
    {
        /// <exception cref="PageSizeException"/>
        /// <exception cref="PageIndexException"/>
        PageList<MachineLearningModel> FindPageByTerm(int pageSize, int pageIndex, string searchTerm);

        IList<MachineLearningModel> FindByName(string name);
    }
}