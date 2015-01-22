using IOT.Domain.Frames;
using IOT.WebApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOT.WebApplication.Controllers
{
    public class FramesController : Controller
    {

        private List<Frame> GenerateMockFrames()
        {

            List<Frame> list = new List<Frame>();

            var elementTypes = new List<FrameElement>();
            elementTypes.Add(new FrameElement() { Name = "Int", Size = 32, Id = 1, Description = "test integer element" });
            elementTypes.Add(new FrameElement() { Name = "Long", Size = 64, Id = 2, Description = "test long element" });
            elementTypes.Add(new FrameElement() { Name = "Bool", Size = 8, Id = 3, Description = "test boolean element" });
            elementTypes.Add(new FrameElement() { Name = "Float", Size = 32, Id = 4, Description = "test float element" });

            var frame1 = new Frame() { Id = 1, Name="Test Frame 1" };
            var frame2 = new Frame() { Id = 2, Name="Test Frame 2" };
            foreach (var item in elementTypes)
            {
                frame1.AddElement(item);
            }

            elementTypes.Add(new FrameElement() { Name = "Double", Size = 64, Id = 5, Description = "test double element" });
            elementTypes.Add(new FrameElement() { Name = "String", Size = 0, Id = 6, Description = "test string element" });
            elementTypes.Add(new FrameElement() { Name = "GIS", Size = 128, Id = 7, Description = "test GIS element" });

            foreach (var item in elementTypes)
            {
                frame2.AddElement(item);
            }

            list.Add(frame1);
            list.Add(frame2);
            return list;
        }


        //
        // GET: /Frames/
        public ActionResult Index()
        {

            var list = GenerateMockFrames();
            return View(list);
        }


        [HttpGet]
        public ActionResult Details(int id)
        {
            var list = GenerateMockFrames();
            return View(new FrameBuilderViewModel() { Frame = list.Find(f => f.Id == id) });
        }

        [HttpPost]
        public PartialViewResult AddFrameElement(FrameBuilderViewModel vm, int frameId)
        {
            var list = GenerateMockFrames();
            var frame = list.Find(f => f.Id == frameId);
            frame.AddElement(vm.NewElement);
            return PartialView("_FramePartial", frame);
        }
	}
}