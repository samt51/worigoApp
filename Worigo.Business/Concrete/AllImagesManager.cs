using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class AllImagesManager :IAllImagesService
    {
        private IAllImagesDal _AllImages;
        public AllImagesManager(IAllImagesDal AllImages)
        {
            _AllImages = AllImages;
        }
        public void Create(AllImages entity)
        {
            _AllImages.Create(entity);
        }

        public List<AllImages> GetAll()
        {
            return _AllImages.GetAll(x=>x.isDeleted==false);
        }

        public AllImages GetById(int id)
        {
            return _AllImages.GetById(id);
        }

        public void Update(AllImages entity)
        {
            _AllImages.Update(entity);
        }
    }
}
