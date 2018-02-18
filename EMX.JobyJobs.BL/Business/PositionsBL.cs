using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.BusinessObjects;
using EMX.JobyJobs.BL.Interfaces;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;
using EMX.JobyJobs.Shared.Exceptions;
using EMX.JobyJobs.Shared.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace EMX.JobyJobs.BL.Business
{
  public interface IPositionsBL : IBusiness
  {
    CRUD<position> Crud { get; }
    List<Position> SearchPosition(PositionsSearchCriteria criteria);
    void PostPosition(Position position, bool asDraft);
    void FreezePosition(int positionId);
    void UnfreezePosition(int positionId);
    void ExposePosition(int positionId);
    void ArchivePosition(int positionId);
    void UnarchivePosition(int positionId);
    List<Position> GetPositionsByStatus(int criteriaEmployerId, Enums.PositionStatuses criteriaStatus);
    void UpdatePosition(Position position);
    void AdminCreatePosition(Position position);
    void AdminUpdatePosition(int positionId, Position position);
    void DeletePosition(int positionId);
    Position GetPosition(int positionId);
    void SetPositionRank(int positionId, Enums.PositionRanks rank);
    string GetPositionShareableLink(Guid positionUID);
  }


  /// <summary>
  /// Handles all business-logic activities around position, searches, search tags, position precedence scheme and so on.
  /// also contains general methods for the application.
  /// </summary>
  public class PositionsBL : IPositionsBL
  {
    private IServiceProvider _sp;

    public CRUD<position> Crud { get; private set; }
    //Seekers Point-Of-View (or all):::


    public PositionsBL(IServiceProvider sp)
    {
      this._sp = sp;
    }


    /// <summary>
    /// Seeker side: Searches for all positions matching the specified criteria.
    /// </summary>
    /// <returns></returns>
    public List<Position> SearchPosition(PositionsSearchCriteria criteria)
    {
      using (var db = new JobyJobsDB2())
      {
        string searchTxtLowered = criteria.SearchText?.ToLower() ?? null;
        string locationLowered = criteria.Location?.ToLower() ?? null;
        int? salaryMin = criteria.SalaryPerMonth != null
          ? (criteria.SalaryPerMonth.Value ? criteria.Salary : criteria.Salary / 12)
          : //convert to month
          null;
        IQueryable<position> query = db.positions.Where(item =>
          (searchTxtLowered == null || item.title.ToLower().Contains(searchTxtLowered) ||
           item.description.ToLower().Contains(searchTxtLowered)) && //optional:by title or description
          (criteria.ProfessionId == null || item.profession_id == (criteria.ProfessionId)) && //optional:by field
          (criteria.FieldId == null || item.profession.field_id == (criteria.FieldId)) && //optional:by profession
          (criteria.PositionType == null ||
           item.position_type == ((int?)criteria.PositionType)) && //optional:by position type
          (salaryMin == null || item.salary_min > salaryMin) && //optional:by salary
          (locationLowered == null || item.location.ToLower().Contains(locationLowered)) && //optional:by location
          item.status_id == (int)Enums.PositionStatuses.OnAir);
        var sql = query.ToString();
        var res = query.ToSelectedList(BusinessObjectsExtensions.ToBusiness);
        return res;
      }
    }
    /// <summary>
    /// Seeker side (both): Returns the requested position.
    /// </summary>
    /// <param name="positionId"></param>
    /// <returns></returns>
    public Position GetPosition(int positionId)
    {
      return Crud.Get(positionId).ToBusiness();
    }

    public void SetPositionRank(int positionId, Enums.PositionRanks newRank)
    {
      using (var db = new JobyJobsDB2())
      {
        var position = db.positions.Find(positionId);
        position.rank = (int)newRank;

        db.SetAsModifiedAndSave(position);
      }
    }

    public string GetPositionShareableLink(Guid positionUID)
    {
      return _sp.GetService<ILinkGenerator>().GeneratePositionShareableLink(positionUID);
    }

    //public   FeaturedPositionsList GetFeaturedPositions()
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    var items = db.positions.Take(10).AsEnumerable()
    //        .Select(ServiceObjectsExtensions.ToBusiness)
    //        /*.OrderBy(item => item.Precedence)*/.ToList();
    //    return new FeaturedPositionsList(items);    //todo. add random
    //  }
    //}


    //public   List<Position> GetLastActivePositions()
    //{
    //  return new List<Position>();
    //}


    //public   PositionsStats GetPositionsStats()
    //{
    //  return new PositionsStats();
    //}
    //public   List<Field> GetFields()
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    var items = db.fields.Take(10).AsEnumerable()
    //      .Select(ServiceObjectsExtensions.ToBusiness).ToList();

    //    return items;
    //  }
    //}


    //simple CRUD
    public List<Position> GetAllPositions()
    {
      return Crud.GetAll().ToSelectedList(BusinessObjectsExtensions.ToBusiness);
    }





    //Employers Point-Of-View:::
    public void PostPosition(Position position, bool asDraft)
    {
      using (var db = new JobyJobsDB2())
      {
        //fix param data.
        var dateTimeNow = DateTime.Now;
        position.PositionUID = Guid.NewGuid();
        position.Status = !asDraft ? Enums.PositionStatuses.Draft : Enums.PositionStatuses.OnAir;
        position.CreateDate = dateTimeNow;
        position.PublishDate = !asDraft ? (DateTime?)dateTimeNow : null;
        position.LastUpdated = dateTimeNow;
        position.Active = true;
        db.positions.Add(position.ToDB());

        db.SaveChanges();
      }
    }

    public void FreezePosition(int positionId)
    {
      using (var db = new JobyJobsDB2())
      {
        position dbPosition;
        if (!db.positions.GetAny(item => item.position_id == positionId, out dbPosition))
        {
          throw new JobyJobsException("The Requested position was not found");
        }

        var requestedStatus = Enums.PositionStatuses.Frozen;
        var currentState = dbPosition.status_id.ToType<Enums.PositionStatuses>();
        if (currentState == requestedStatus) //already in the requested status, return.
          return;

        if (currentState != Enums.PositionStatuses.OnAir)
        {
          throw new JobyJobsException($@"Cannot Freeze a position that is not on air - currentState={currentState}");
        }

        dbPosition.status_id = (int)Enums.PositionStatuses.Frozen;
        db.Entry(dbPosition).State = EntityState.Modified;

        db.SaveChanges();
      }
    }

    public void UnfreezePosition(int positionId)
    {
      using (var db = new JobyJobsDB2())
      {
        position dbPosition;
        if (!db.positions.GetAny(item => item.position_id == positionId, out dbPosition))
        {
          throw new JobyJobsException("The Requested position was not found");
        }


        var requestedStatus = Enums.PositionStatuses.OnAir;
        var currentState = dbPosition.status_id.ToType<Enums.PositionStatuses>();
        if (currentState == requestedStatus) //already in the requested status, return.
          return;

        if (currentState != Enums.PositionStatuses.Frozen)
        {
          throw new JobyJobsException(
            $@"Cannot Unfreeze a position that is not currently frozen - currentState={currentState}");
        }

        dbPosition.status_id = (int)Enums.PositionStatuses.OnAir;
        db.Entry(dbPosition).State = EntityState.Modified;

        db.SaveChanges();
      }
    }

    /// <summary>
    /// Sets the position to be OnAir.
    /// </summary>
    public void ExposePosition(int positionId)
    {
      using (var db = new JobyJobsDB2())
      {
        position dbPosition;
        if (!db.positions.GetAny(item => item.position_id == positionId, out dbPosition))
        {
          throw new JobyJobsException("The Requested position was not found");
        }


        var requestedStatus = Enums.PositionStatuses.OnAir;
        var currentState = dbPosition.status_id.ToType<Enums.PositionStatuses>();
        if (currentState == requestedStatus) //already in the requested status, return.
          return;

        if (currentState != Enums.PositionStatuses.Draft)
        {
          throw new JobyJobsException($@"The current position is in an invalid state - currentState={currentState}");
        }

        dbPosition.status_id = (int)Enums.PositionStatuses.OnAir;
        db.Entry(dbPosition).State = EntityState.Modified;

        db.SaveChanges();
      }
    }

    /// <summary>
    /// Employer side: Archives the given position (shown in the 'deleted category' for the employers).
    /// </summary>
    public void ArchivePosition(int positionId)
    {
      using (var db = new JobyJobsDB2())
      {
        position dbPosition;
        if (!db.positions.GetAny(item => item.position_id == positionId, out dbPosition))
        {
          throw new JobyJobsException("The Requested position was not found");
        }


        var requestedStatus = Enums.PositionStatuses.Archived;
        var currentState = dbPosition.status_id.ToType<Enums.PositionStatuses>();
        if (currentState == requestedStatus) //already in the requested status, return.
          return;

        if (currentState != Enums.PositionStatuses.Draft)
        {
          throw new JobyJobsException($@"The current position is in an invalid state - currentState={currentState}");
        }

        dbPosition.status_id = (int)Enums.PositionStatuses.Archived;
        db.Entry(dbPosition).State = EntityState.Modified;

        db.SaveChanges();
      }
    }

    public void UnarchivePosition(int positionId)
    {
      using (var db = new JobyJobsDB2())
      {
        position dbPosition;
        if (!db.positions.GetAny(item => item.position_id == positionId, out dbPosition))
        {
          throw new JobyJobsException("The Requested position was not found");
        }


        var requestedStatus = Enums.PositionStatuses.OnAir;
        var currentState = dbPosition.status_id.ToType<Enums.PositionStatuses>();
        if (currentState == requestedStatus) //already in the requested status, return.
          return;

        if (currentState != Enums.PositionStatuses.Archived)
        {
          throw new JobyJobsException(
            $@"Cannot Unarchive a position that is not currently archived - currentState={currentState}");
        }

        dbPosition.status_id = (int)Enums.PositionStatuses.OnAir;
        db.Entry(dbPosition).State = EntityState.Modified;

        db.SaveChanges();
      }
    }


    public List<Position> GetPositionsByStatus(int criteriaEmployerId, Enums.PositionStatuses criteriaStatus)
    {
      using (var db = new JobyJobsDB2())
      {
        var res = db.positions.Where(item => item.employer_id == criteriaEmployerId);
        if (criteriaStatus != Enums.PositionStatuses.All)
        {
          return res.Where(item => item.status_id == (int)criteriaStatus)
            .ToSelectedList(BusinessObjectsExtensions.ToBusiness);
        }
        else
        {
          return res.ToSelectedList(BusinessObjectsExtensions.ToBusiness);
        }
      }
    }

    /// <summary>
    /// Updates the given position information.
    /// </summary>
    /// <param name="position"></param>
    public void UpdatePosition(Position position)
    {
      using (var db = new JobyJobsDB2())
      {
        int positionId = position.PositionId;
        position dbPosition;
        if (!db.positions.GetAny(item => item.position_id == positionId, out dbPosition))
        {
          throw new JobyJobsException("The Requested position was not found");
        }

        db.Entry(dbPosition.DBUpdateFrom(position)).State = EntityState.Modified;

        db.SaveChanges();
      }
    }





    //Admin Point-Of-View:::
    /// <summary>
    /// Admin side only: adds a new position
    /// </summary>
    /// <param name="position"></param>
    public void AdminCreatePosition(Position position)    //create
    {
      using (var db = new JobyJobsDB2())
      {
        db.positions.Add(position.ToDB());

        db.SaveChanges();
      }
    }

    /// <summary>
    /// Admin side only: updates a position
    /// </summary>
    /// <param name="position"></param>
    public void AdminUpdatePosition(int positionId, Position position)    //update
    {
      using (var db = new JobyJobsDB2())
      {
        position dbPosition;
        if (!db.positions.GetAny(item => item.position_id == positionId, out dbPosition))
        {
          throw new JobyJobsException("The Requested position was not found");
        }

        db.Entry(dbPosition.DBAdminUpdateFrom(position)).State = EntityState.Modified;

        db.SaveChanges();
      }
    }

    /// <summary>
    /// Admin side only: deletes the given position.
    /// Note: logical delete.
    /// </summary>
    public void DeletePosition(int positionId)     //delete
    {
      using (var db = new JobyJobsDB2())
      {
        position dbPosition;
        if (!db.positions.GetAny(item => item.position_id == positionId, out dbPosition))
        {
          throw new JobyJobsException("The Requested position was not found");
        }

        //mark as deleted.
        dbPosition.active = false;

        db.SaveChanges();
      }

    }

  }

}