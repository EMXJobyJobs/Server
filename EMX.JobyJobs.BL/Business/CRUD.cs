using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.Interfaces;
using EMX.JobyJobs.DAL.Interfaces;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Exceptions;
using EMX.JobyJobs.Shared.Helpers;

namespace EMX.JobyJobs.BL.Business
{

  /// <summary>
  /// interface for CRUD
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public interface ICRUD<TEntity> where TEntity : class
  {
    TEntity Get(int id);
    TEntity Get(int id, JobyJobsDB2 db);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetAll(JobyJobsDB2 db);
    int Add(TEntity b);
    int Add(TEntity b, JobyJobsDB2 db);
    int Update(int id, TEntity b);
    int Update(int id, TEntity b, JobyJobsDB2 db);
    int Delete(int id);
    int Delete(int id, JobyJobsDB2 db);
    int HardDelete(int id);
    int HardDelete(int id, JobyJobsDB2 db);
  }

  /// <summary>
  /// A business-logic CRUD component.
  /// </summary>
  public class CRUD<TEntity> : ICRUD<TEntity> where TEntity : class
  {
    public CRUD()
    {
    }

    /// <summary>
    /// Note: does not include includes.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public TEntity Get(int id)
    {
      using (var db = new JobyJobsDB2())
      {
        return db.Set<TEntity>().Find(id);
      }
    }

    /// <summary>
    /// Note: does not include includes.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="db"></param>
    /// <returns></returns>
    public TEntity Get(int id, JobyJobsDB2 db)
    {
      return db.Set<TEntity>().Find(id);
    }

    public IEnumerable<TEntity> GetAll()
    {
      using (var db = new JobyJobsDB2())
      {
        return db.Set<TEntity>();
      }
    }

    public IEnumerable<TEntity> GetAll(JobyJobsDB2 db)
    {
      return db.Set<TEntity>();
    }

    public int Add(TEntity b)
    {
      using (var db = new JobyJobsDB2())
      {
        //adds the entity.
        TEntity addedEntity = db.Set<TEntity>().Add(b);

        //saves changes.
        db.SaveChanges();

        return addedEntity != null ? 1 : 0;
      }
    }

    public int Add(TEntity b, JobyJobsDB2 db)
    {
      TEntity addedEntity = db.Set<TEntity>().Add(b);

      return 1;
    }

    /// <summary>
    /// Note: only works for db-originated entities.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="modifiedObj"></param>
    /// <returns></returns>
    public int Update(int id, TEntity modifiedObj)
    {
      using (var db = new JobyJobsDB2())
      {

        //updates the entity.
        db.Entry(modifiedObj).State = EntityState.Modified;    //todo ori. check. check if works.

        //saves changes.
        db.SaveChanges();

        return 1;
      }
    }

    /// <summary>
    /// Note: only works for db-originated entities.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="b"></param>
    /// <param name="db"></param>
    /// <returns></returns>
    public int Update(int id, TEntity modifiedObj, JobyJobsDB2 db)
    {
      db.Entry(modifiedObj).State = EntityState.Modified;    //todo ori. check. check if works.
      return 1;
    }

    /// <summary>
    /// only works for Active-Aware data entities.
    /// Note: logical delete.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int Delete(int id)
    {
      using (var db = new JobyJobsDB2())
      {
        //soft deletes the entity.
        var x = Delete(id, db);

        //saves changes
        db.SaveChanges();

        return x;
      }
    }

    public int Delete(int id, JobyJobsDB2 db)
    {
      var itemToDelete = db.Set<TEntity>().Find(id);
      if (!(itemToDelete is IActiveAwareDataObject))
      {
        throw new JobyJobsException("not active aware data object");
      }
      itemToDelete.ToType<IActiveAwareDataObject>().Active = false;
      db.Entry(itemToDelete).State = EntityState.Modified;
      return 1;
    }

    /// <summary>
    /// Note: physical delete.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int HardDelete(int id)
    {
      using (var db = new JobyJobsDB2())
      {
        //hard deletes the entity
        var x = HardDelete(id, db);

        //saves changes.
        db.SaveChanges();

        return x;
      }
    }

    public int HardDelete(int id, JobyJobsDB2 db)
    {
      var itemToDelete = db.Set<TEntity>().Find(id);
      var obj = db.Set<TEntity>().Remove(itemToDelete);
      return 1;
    }

    //public static IQueryable<T> IncludeAll<T>(this IQueryable<T> queryable) where T : class
    //{
    //  var type = typeof(T);
    //  var properties = type.GetProperties();
    //  foreach (var property in properties)
    //  {
    //    var isVirtual = property.GetGetMethod().IsVirtual;
    //    if (isVirtual && properties.FirstOrDefault(c => c.Name == property.Name + "Id") != null)
    //    {
    //      queryable = queryable.Include(property.Name);
    //    }
    //  }
    //  return queryable;
    //}
  }

}
