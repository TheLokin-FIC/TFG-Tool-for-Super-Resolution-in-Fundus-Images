using Business.Exceptions;
using DataTransfer.Input.Dataset;
using DataTransfer.Output.Collections;
using DataTransfer.Output.Dataset;
using System;
using System.Collections.Generic;

namespace Business.Services.DatasetService
{
    public interface IDatasetService
    {
        /// <exception cref="PageException"/>
        Page<DatasetInfo> GetDatasetPage(int pageSize, int pageIndex, string searchTerm, long? userId);

        /// <exception cref="AuthenticationException"/>
        /// <exception cref="ArgumentException"/>
        long CreateDataset(long userId, NewDataset newDataset);

        /// <exception cref="NotFoundException"/>
        /// <exception cref="AuthenticationException"/>
        DatasetDetails GetDataset(long datasetId, long? userId);

        /// <exception cref="NotFoundException"/>
        /// <exception cref="AuthenticationException"/>
        IList<ImageDetails> GetImagesDataset(long datasetId, long? userId);

        /// <exception cref="AuthenticationException"/>
        /// <exception cref="NotFoundException"/>
        void UpdateDataset(long datasetId, long userId, NewDataset newDataset);

        /// <exception cref="AuthenticationException"/>
        /// <exception cref="NotFoundException"/>
        void DeleteDataset(long datasetId, long userId);
    }
}