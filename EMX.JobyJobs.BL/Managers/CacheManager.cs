using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace EMX.JobyJobs.BL.Managers
{

  public interface ICacheSource
  {
    Dictionary<int, string> LoadData(CacheManager.CacheType cacheType);   //Dictionary. K:ItemId(set list_idx if irrelevant), V:Object.
  }

  public interface ICacheManager
  {
    Dictionary<int, string> Fields { get; set; } //Dictionary. K:FieldId, V:FieldObject
    Dictionary<int, string> Professions { get; set; } //Dictionary. K:FieldId, V:ProfessionObject
    Dictionary<int, string> SubProfessions { get; set; } //Dictionary. K:ProfessionId, V:SubprofessionObject
    List<string> Cities { get; set; } //Note: In israel
    void Initialize(ICacheSource cacheSource);
    void Bust();
    void Close();
  }

  /// <summary>
  /// Handles all cache requests for the application.
  /// </summary>
  public class CacheManager : ICacheManager
  {
    public enum CacheType
    {
      Unspecified = 0,
      Fields = 1,
      Professions = 2,
      Cities = 4
    }

    private ICacheSource _cacheSource;

    public Dictionary<int, string> Fields { get; set; }  //Dictionary. K:FieldId, V:FieldObject
    public Dictionary<int, string> Professions { get; set; }   //Dictionary. K:FieldId, V:ProfessionObject
    public Dictionary<int, string> SubProfessions { get; set; }   //Dictionary. K:ProfessionId, V:SubprofessionObject

    public List<string> Cities { get; set; }   //Note: In israel

    public CacheManager()
    {
      Fields = new Dictionary<int, string>();
      Professions = new Dictionary<int, string>();
      SubProfessions = new Dictionary<int, string>();
      Cities = new List<string>();
    }

    public void Initialize(ICacheSource cacheSource)
    {
      _cacheSource = cacheSource;

      //Loads all data.
      Reload();
    }

    private void Reload()
    {
      Fields = _cacheSource.LoadData(CacheType.Fields);
      Professions = _cacheSource.LoadData(CacheType.Professions);
      Cities = _cacheSource.LoadData(CacheType.Cities).Values.ToList();
    }

    public void Bust()
    {
      //Reloads all data.
      Reload();
    }

    public void Close()
    {
      _cacheSource = null;
    }
  }


}
