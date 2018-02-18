using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMX.JobyJobs.BL.Interfaces
{
  /// <summary>
  /// Holds the base functionality for all business entity objects.
  /// </summary>
  public interface IBusinessObject
  {

  }

  /// <summary>
  /// Holds the base functionality for all business entity objects.
  /// contains 'Id' column.
  /// </summary>
  public interface IIdAwareBusinessObject
  {
    int Id { get; set; }
  }

  /// <summary>
  /// Holds the base functionality for all business entity objects.
  /// contains 'Id,Active' columns.
  /// </summary>
  public interface IActiveAwareBusinessObject
  {
    int Id { get; set; }
    bool Active { get; set; }
  }

  /// <summary>
  /// Holds the base functionality for all business entity objects.
  /// contains 'Id,LastUpdated' columns.
  /// </summary>
  public interface IUpdateAwareBusinessObject
  {
    int Id { get; set; }
    DateTime LastUpdated { get; set; }
  }

  /// <summary>
  /// Holds the base functionality for all business entity objects.
  /// contains 'Id,Active,LastUpdated' columns.
  /// </summary>
  public interface IActiveUpdateAwareBusinessObject
  {
    int Id { get; set; }
    DateTime LastUpdated { get; set; }
    bool Active { get; set; }
  }


}
