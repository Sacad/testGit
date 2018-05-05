using AGLTest.Models;
using AGLTest.Services;
using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace AGLTest.Controllers
{
    public class PetController : Controller
    {
        //
        // GET: /Pets/

        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        [Route("Cats")]
        public ActionResult Index()
        {

            IList<Cat> resp = new List<Cat>();
            
            try
            {
                resp = _petService.GetCatsByOwnerGender();
            }
            catch
            {
                return RedirectToAction("Error");
            }               

            return View(resp);
        }


        public ActionResult Error()
        {
            return View();
        }


       
    }
}
