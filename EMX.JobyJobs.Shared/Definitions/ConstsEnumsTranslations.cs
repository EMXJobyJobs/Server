using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMX.JobyJobs.Shared.Definitions
{
  public static class ConstsEnumsTranslations
  {
    public static Guid GetRoleGUID(Enums.UserRoles role)
    {
      switch (role)
      {
        case Enums.UserRoles.Seeker:
          return Consts.SeekerRoleGUID;
          break;
        case Enums.UserRoles.Employer:
          return Consts.EmployerRoleGUID;
          break;
        case Enums.UserRoles.Administrator:
          return Consts.AdminRoleGUID;
          break;
        case Enums.UserRoles.Unspecified:
        default:
          throw new ArgumentOutOfRangeException(nameof(role), role, null);
      }
    }
  }
}
