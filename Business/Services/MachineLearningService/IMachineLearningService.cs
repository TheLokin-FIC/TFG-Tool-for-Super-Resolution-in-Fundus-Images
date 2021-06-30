using Business.Exceptions;
using SharedData.Collections;
using SharedData.MachineLearning;

namespace Business.Services.MachineLearningService
{
    public interface IMachineLearningService
    {
        /// <exception cref="PageException"/>
        Page<ModelDetails> GetPage(int pageSize, int pageIndex, string searchTerm);
    }
}