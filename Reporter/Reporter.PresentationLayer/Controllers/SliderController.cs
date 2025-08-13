using Microsoft.AspNetCore.Mvc;
using Reporter.BusinessLayer.Abstract;
using Reporter.EntityLayer.Entities;

namespace Reporter.PresentationLayer.Controllers
{
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public IActionResult test()
        {
            return View();
        }

        public IActionResult SliderList()
        {
            var values = _sliderService.TGetListAll();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateSlider()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSlider(Slider slider)
        {
            _sliderService.TInsert(slider);
            return RedirectToAction("SliderList");
        }
        public IActionResult DeleteSlider(int id)
        {
            _sliderService.TDelete(id);
            return RedirectToAction("SliderList");
        }
        [HttpGet]
        public IActionResult UpdateSlider(int id)
        {
            var value = _sliderService.TGetById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateSlider(Slider slider)
        {
            _sliderService.TUpdate(slider);
            return RedirectToAction("SliderList");
        }
    }
}