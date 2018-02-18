using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMX.JobyJobs.BL.Interfaces
{
  /// <summary>
  /// Contains the base functionality for all business-layer managers. (xxxBL)
  /// </summary>
  public interface IBusiness
  {
  
  }

  /// <summary>
  /// Contains the base functionality for all business-layer managers. (xxxBL)
  /// </summary>
  public interface ICRUDBusiness<TEntity> : IBusiness
    where TEntity : class
  {
    IEnumerable<TEntity> GetBooks();
    TEntity GetBook(int id);
    int AddBook(TEntity b);
    int UpdateBook(int id, TEntity b);
    int DeleteBook(int id);
  }
  /// <summary>
  /// Generic Class; Contains the base functionality for all business-layer managers. (xxxBL)
  /// </summary>
  public interface ICRUDBusinessGeneral
  {
    IEnumerable GetBooksGeneral();
    object GetBookGeneral(int id);
    int AddBook(object b);
    int UpdateBook(int id, object b);
    int DeleteBookGeneral(int id);
  }
}
