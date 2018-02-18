using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMX.JobyJobs.DAL.Interfaces
{
  /// <summary>
  /// Holds the base functionality for all data(access) entity objects.
  /// </summary>
  public interface IDataObject
  {
  }
  /// <summary>
  /// Holds the base functionality for all data entity objects.
  /// contains 'Id' column.
  /// </summary>
  public interface IIdAwareDataObject : IDataObject
  {
    int Id { get; set; }
  }

  /// <summary>
  /// Holds the base functionality for all data entity objects.
  /// contains 'Id,Active' columns.
  /// </summary>
  public interface IActiveAwareDataObject : IIdAwareDataObject
  {
    //int Id { get; set; }
    bool Active { get; set; }
  }

  /// <summary>
  /// Holds the base functionality for all data entity objects.
  /// contains 'Id,LastUpdated' columns.
  /// </summary>
  public interface IUpdateAwareDataObject : IIdAwareDataObject
  {
    //int id { get; set; }
    DateTime LastUpdated { get; set; }
  }

  /// <summary>
  /// Holds the base functionality for all data entity objects.
  /// contains 'Id,Active,LastUpdated' columns.
  /// </summary>
  public interface IActiveUpdateAwareDataObject : IActiveAwareDataObject, IUpdateAwareDataObject
  {
    //int id { get; set; }
    //DateTime last_updated { get; set; }
    //bool active { get; set; }
  }
}
