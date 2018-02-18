using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.BusinessObjects;
using EMX.JobyJobs.BL.Interfaces;
using log4net.Core;

namespace EMX.JobyJobs.BL.Business
{

  public interface IAdminBL : IBusiness
  {
    void RegisterAdminPerson(AdminPerson item);
  }
  public class AdminBL : IAdminBL
  {
    private readonly ILogger _logger;
    public void RegisterAdminPerson(AdminPerson item)
    {

    }
  }

}
