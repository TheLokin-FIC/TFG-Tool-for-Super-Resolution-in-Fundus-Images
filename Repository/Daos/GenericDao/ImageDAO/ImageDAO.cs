using Repository.Exceptions;
using Repository.Persistence;
using Repository.Persistence.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository.DAOs.GenericDAO.ImageDAO
{
    public class ImageDAO : GenericDAO<Image>, IImageDAO
    {
        public ImageDAO(ApplicationDbContext context) : base(context)
        {
        }

        public Image FindCover(long datasetId)
        {
            IEnumerable<Image> imageContext = context.Set<Image>().AsEnumerable();

            return imageContext.Where(x => x.DatasetId == datasetId).FirstOrDefault() ?? throw new InstanceNotFoundException(typeof(Image), datasetId);
        }

        public IList<Image> FindByDatasetId(long datasetId)
        {
            IEnumerable<Image> imageContext = context.Set<Image>().AsEnumerable();

            return imageContext.Where(x => x.DatasetId == datasetId).OrderBy(x => x.Name).ToList();
        }
    }
}