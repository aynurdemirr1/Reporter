using Reporter.BusinessLayer.Abstract;
using Reporter.DataAccessLayer.Abstract;
using Reporter.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporter.BusinessLayer.Concrete
{
    public class SliderManager : ISliderService
    {
        private readonly ISliderDal _sliderDal;

        public SliderManager(ISliderDal sliderDal)
        {
            _sliderDal = sliderDal;
        }

        public void TDelete(int id)
        {
            _sliderDal.Delete(id);
        }

        public Slider TGetById(int id)
        {
            return _sliderDal.GetById(id);
        }

        public List<Slider> TGetListAll()
        {
            return _sliderDal.GetListAll();
        }

        public void TInsert(Slider entity)
        {
            _sliderDal.Insert(entity);
        }

        public void TUpdate(Slider entity)
        {
            _sliderDal.Update(entity);
        }
    }
}