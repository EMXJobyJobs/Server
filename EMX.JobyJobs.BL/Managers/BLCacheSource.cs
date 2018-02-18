using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.ProxyServices.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace EMX.JobyJobs.BL.Managers
{
  public interface IBLCacheSource : ICacheSource
  {
  }

  public class BLCacheSource
    : IBLCacheSource
  {
    private IServiceProvider _sp;

    public BLCacheSource(IServiceProvider sp)
    {
      _sp = sp;
    }
    public Dictionary<int, string> LoadData(CacheManager.CacheType cacheType)
    {
      switch (cacheType)
      {
        case CacheManager.CacheType.Fields:
          {
            return _sp.GetService<IProfessionsBL>().GetProfessionsHierarchy().Fields.ToDictionary(kp => kp.Key, kp => kp.Value.Title);
          }
          break;
        case CacheManager.CacheType.Professions:
          {
            return _sp.GetService<IProfessionsBL>().GetProfessionsHierarchy().Fields.Values
              .SelectMany(item => item.Professions)
              .ToDictionary(kp => kp.Key, kp => kp.Value.Title);
          }
          break;
        //case CacheType.SubProfessions:
        //  {
        //    return ProfessionsBL.GetProfessionHierarchy().Fields.Values
        //      .SelectMany(item => item.Professions)
        //      .SelectMany(item => item.Value.SubProfessions)
        //      .ToDictionary(kp => kp.Key, kp => kp.Value.Profession.Title);
        //  }
        //  break;
        case CacheManager.CacheType.Cities:
          {
            int idx = 0;
            return CitiesManager.LoadAll().ToDictionary(kp => idx++, kp => kp);
          }
          break;
        case CacheManager.CacheType.Unspecified:
        default:
          throw new ArgumentOutOfRangeException(nameof(cacheType), cacheType, null);
      }
    }
  }
}
