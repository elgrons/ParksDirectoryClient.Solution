using Microsoft.AspNetCore.Mvc;
using ParksDirectoryClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Diagnostics;

namespace ParksDirectoryClient.Controllers;

public class ParksController : Controller
{
  public async Task<IActionResult> Index(int page = 1, int pageSize = 6)
  {
    Park park = new Park();
    List<Park> parkList = new List<Park> { };
    using (var httpClient = new HttpClient())
    {
      using (var response = await httpClient.GetAsync($"https://localhost:5001/api/Parks?page={page}&pagesize={pageSize}"))
      {
        var parkContent = await response.Content.ReadAsStringAsync();
        JArray parkArray = JArray.Parse(parkContent);
        parkList = parkArray.ToObject<List<Park>>();
      }
    }

    ViewBag.TotalPages = parkList.Count();
    //page number inside the url
    ViewBag.CurrentPage = page;
    //amnt of items on the page
    ViewBag.PageSize = pageSize;
     //the amount of destinations returned from our database
    // ViewBag.Pages = pageCount;

    return View(parkList);
  }

  public IActionResult Details(int id)
  {
    Park park = Park.GetDetails(id);
    return View(park);
  }

  public ActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public ActionResult Create(Park park)
  {
    Park.Post(park);
    return RedirectToAction("Index");
  }

  public ActionResult Edit(int id)
  {
    Park park = Park.GetDetails(id);
    return View(park);
  }

  [HttpPost]
  public ActionResult Edit(Park park)
  {
    Park.Put(park);
    return RedirectToAction("Details", new { id = park.ParkId});
  }

  public ActionResult Delete(int id)
  {
    Park park = Park.GetDetails(id);
    return View(park);
  }

  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Park.Delete(id);
    return RedirectToAction("Index");
  }
}
