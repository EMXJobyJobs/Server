using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMX.JobyJobs.ProxyServices.Managers
{
  /// <summary>
  /// Loads data from an external cities service.
  /// </summary>
  public static class CitiesManager
  {
    public static List<string> LoadAll()
    {
      return Mockup();
    }

    private static List<string> Mockup()
    {
      return new List<string>()
      {
        "נתניה",
        "תל אביב",
        "הרצליה",
        "חולון"
      };

    }
  }
}
