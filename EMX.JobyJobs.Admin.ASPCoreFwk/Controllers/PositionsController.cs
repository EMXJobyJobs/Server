using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMX.JobyJobs.Admin.ASPCoreFwk.Models;
using EMX.JobyJobs.BL.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMX.JobyJobs.Admin.ASPCoreFwk.Controllers
{
  [Authorize(Roles = "Administrator")]
  public class PositionsController : Controller
  {
    private IPositionsBL _positionsBL;
    private IServiceProvider _sp;

    public PositionsController(IServiceProvider sp, IPositionsBL positionsBL)
    {
      this._sp = sp;
      this._positionsBL = positionsBL;
    }

    // GET: /<controller>/
    public IActionResult Index()
    {
      return View();
    }

    /// <summary>
    /// Creates a new position.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult CreatePosition(CreateUpdatePositionViewModel data)
    {
      _positionsBL.AdminCreatePosition(data.ToBusiness());
      return Ok();
    }

    /// <summary>
    /// Updates the given position information.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult UpdatePosition(CreateUpdatePositionViewModel data)
    {
      _positionsBL.AdminUpdatePosition(data.PostionId, data.ToBusiness());
      return Ok();
    }

  }
}
