using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMX.JobyJobs.Admin.ASPCoreFwk.Controllers
{
  [Authorize(Roles = "Administrator")]
  public class InterviewsController : Controller
  {
    // GET: Interviews
    public ActionResult Index()
    {
      return View();
    }

    // GET: Interviews/Details/5
    public ActionResult Details(int id)
    {
      return View();
    }

    // GET: Interviews/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Interviews/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
      try
      {
        // TODO: Add insert logic here

        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: Interviews/Edit/5
    public ActionResult Edit(int id)
    {
      return View();
    }

    // POST: Interviews/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
      try
      {
        // TODO: Add update logic here

        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: Interviews/Delete/5
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: Interviews/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
      try
      {
        // TODO: Add delete logic here

        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }
  }
}