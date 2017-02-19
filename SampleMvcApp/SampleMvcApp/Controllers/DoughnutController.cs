using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Sample.Domain;

namespace SampleMvcApp.Controllers
{
    public class DoughnutController : Controller
    {
        // GET: Doughnut
        public ActionResult Index()
        {
			IEnumerable<Sample.Domain.Doughnut> model;
			using (var sampleContext = new SampleContext())
			{
				model = sampleContext.Doughnuts.Include("Flavor").ToList();
			}
            return View(model);
        }

		public ActionResult Create(string message = "")
		{
			Sample.Domain.Doughnut model = new Doughnut();
			ViewBag.Title = "Create Doughnut";
			ViewBag.ResponseMessage = message;
			return View("Edit", model);
		}
        
		public ActionResult Edit(int Id, string message = "")
		{
			Sample.Domain.Doughnut model;
			ViewBag.Title = "Edit Doughnut";
			ViewBag.ResponseMessage = message;
			using (var sampleContext = new SampleContext())
			{
				model = sampleContext.Doughnuts.Include("Flavor").FirstOrDefault(x => x.Id == Id);
			    if (model == null)
			    {
				    return RedirectToAction("Create", new { message = "This doughnut id doesn't exist. Lets create one." });
			    }
                //string flavor = model?.Flavor?.Name;
			    return View(model);
			}
		}

		public async Task<ActionResult> Delete(int Id)
		{
			using (var sampleContext = new SampleContext())
			{
				Doughnut existingDoughnut = sampleContext.Doughnuts.FirstOrDefault(x => x.Id == Id);
				if (existingDoughnut != null)
				{
					sampleContext.Doughnuts.Remove(existingDoughnut);
					await sampleContext.SaveChangesAsync();
				}
			}

			return RedirectToAction("Index");
		}

		public ActionResult Details(int Id)
		{
            Sample.Domain.Doughnut model;
            ViewBag.Title = "Doughnut Details";
            using (var sampleContext = new SampleContext())
            {
                model = sampleContext.Doughnuts.Include("Flavor").FirstOrDefault(x => x.Id == Id);
                if (model == null)
                {
                    return RedirectToAction("Create", new { message = "This doughnut id doesn't exist. Lets create one." });
                }
                //string flavor = model?.Flavor?.Name;
                return View(model);
            }
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SaveDoughnut(Doughnut doughnut)
		{
			bool success = false;
			if (ModelState.IsValid)
			{
				using (SampleContext sampleContext = new SampleContext())
				{
					if (doughnut.Id == 0)
					{
						if (!DoesDoughnutExist(sampleContext, doughnut.Name, doughnut.Id))
						{
							sampleContext.Doughnuts.Add(doughnut);
							success = true;
						}
					}
					else
					{
						Doughnut existingDoughnut = sampleContext.Doughnuts.Include("Flavor").FirstOrDefault(x => x.Id == doughnut.Id);
						if (!DoesDoughnutExist(sampleContext, doughnut.Name, doughnut.Id))
						{
							existingDoughnut.Name = doughnut.Name;
							existingDoughnut.FlavorId = doughnut.FlavorId;
							success = true;
						}
					}
					await sampleContext.SaveChangesAsync();
				}
				if (!success)
				{
					return RedirectToAction("Edit", new { Id = doughnut.Id, message = $"The doughnut {doughnut.Name} already exists. Please enter a different name." });
				}
			}

			return RedirectToAction("Index");
		}

		private bool DoesDoughnutExist(SampleContext sampleContext, string name, int Id)
		{
			return sampleContext.Doughnuts.FirstOrDefault(x => x.Name == name && x.Id != Id) != null;
		}
    }
}