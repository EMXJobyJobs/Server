using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.BusinessObjects;
using EMX.JobyJobs.BL.Interfaces;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;
using EMX.JobyJobs.Shared.Helpers;

namespace EMX.JobyJobs.BL.Business
{
  public interface IReactionsBL : IBusiness
  {
    void SetSeekerLikesPosition(int seekerId, int positionId);
    void SetSeekerUnlikesPosition(int seekerId, int positionId);
    List<Position> GetSeekerMatches(int seekerId);
    void SetEmployerPersonLikesSeekerAndPosition(int employerPersonId, int seekerId, int positionId);
    void SetEmployerPersonUnlikesSeekerAndPosition(int employerPersonId, int seekerId, int positionId);
    List<Seeker> GetLikedCandidates(int employerPersonId, int positionId);
    List<Seeker> GetLikingCandidates(int positionId);
    List<Seeker> GetEmployerPersonMatches(int employerPersonId, int positionId);
    List<Position> GetLikedPositions(int seekerId);
    List<Employer> GetLikedEmployers(int seekerId);
    List<Position> GetLikingPositions(int seekerId);
    List<Employer> GetLikingEmployers(int seekerId);
  }

  /// <summary>
  /// Handles all business-logic activities around reactions, that is employer-likes-seeker, seeker-likes-employer, and matches.
  /// </summary>
  public class ReactionsBL : IReactionsBL
  {
    //Seekers Point-Of-View:::
    public void SetSeekerLikesPosition(int seekerId, int positionId)
    {
      SetSeekerEmployerReaction(Enums.ReactionTypes.SeekerLikesPosition, seekerId, positionId: positionId);
    }
    public void SetSeekerUnlikesPosition(int seekerId, int positionId)
    {
      SetSeekerEmployerReaction(Enums.ReactionTypes.SeekerLikesPosition, seekerId, positionId: positionId, disable: true);
    }

    public List<Position> GetSeekerMatches(int seekerId)
    {
      using (var db = new JobyJobsDB2())
      {
        var query1 = db.reactions.Where(item => !(item.reaction_disabled != null && item.reaction_disabled == true)
                        && item.reaction_type_id == (int)Enums.ReactionTypes.SeekerLikesPosition
                        && item.seeker_id == seekerId)
                     .Where(item => db.reactions.Any(item0 => item0.reaction_type_id == (int)Enums.ReactionTypes.EmployerPersonLikesSeekerAndPosition
                      && !(item.reaction_disabled != null && item.reaction_disabled == true) && item.position_id == item0.position_id))
                     .Select(item => item.position);
        var res = query1.ToSelectedList(BusinessObjectsExtensions.ToBusiness);   //Todo ori. check. performance.
        return res;
      }
    }
    ///// <summary>
    ///// Returns all the companies that had liked the current seeker.
    ///// </summary>
    ///// <param name="seekerId"></param>
    ///// <returns></returns>
    //public   List<Employer> GetAllSeekerLikes(int seekerId)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    var items = db.reactions.Where(item => item.seeker_id == seekerId
    //            && item.reaction_type_id == (int)Enums.ReactionTypes.EmployerLikesSeeker)
    //      .Select(item => item.employer).AsEnumerable();
    //    return items.Select(ServiceObjectsExtensions.ToBusiness).ToList();
    //  }
    //}
    ///// <summary>
    ///// Returns all the companies that are liked by the current seeker.
    ///// </summary>
    ///// <param name="seekerId"></param>
    ///// <returns></returns>
    //public   List<Employer> GetAllSeekerInitiatedLikes(int seekerId)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    var items = db.reactions.Where(item => item.seeker_id == seekerId
    //                                           && item.reaction_type_id == (int)Enums.ReactionTypes.SeekerLikesEmployer)
    //      .Select(item => item.employer).AsEnumerable();
    //    return items.Select(ServiceObjectsExtensions.ToBusiness).ToList();
    //  }
    //}


    //Employer Point-Of-View:::
    public void SetEmployerPersonLikesSeekerAndPosition(int employerPersonId, int seekerId, int positionId)
    {
      SetSeekerEmployerReaction(Enums.ReactionTypes.EmployerPersonLikesSeekerAndPosition, seekerId, employerPersonId: employerPersonId, positionId: positionId);
    }
    public void SetEmployerPersonUnlikesSeekerAndPosition(int employerPersonId, int seekerId, int positionId)
    {
      SetSeekerEmployerReaction(Enums.ReactionTypes.EmployerPersonLikesSeekerAndPosition, seekerId, employerPersonId: employerPersonId, positionId: positionId, disable: true);
    }

    public List<Seeker> GetEmployerPersonMatches(int employerPersonId, int positionId)
    {
      using (var db = new JobyJobsDB2())
      {
        var query1 = db.reactions.Where(item => !(item.reaction_disabled != null && item.reaction_disabled == true)
                                                && item.reaction_type_id == (int)Enums.ReactionTypes.EmployerPersonLikesSeekerAndPosition
                                                && item.position_id == positionId && item.employer_person_id == employerPersonId)
          .Where(item => db.reactions.Any(item0 => item0.seeker_id == item.seeker_id && !(item0.reaction_disabled != null && item0.reaction_disabled == true) && item0.reaction_type_id == (int)Enums.ReactionTypes.SeekerLikesPosition
                                                && item0.position.employer.employer_persons.Any(item1 => item1.employer_person_id == employerPersonId)))
          .Select(item => item.seeker)
          .Include(item => item.user).Include(item => item.seeker_resumes).Include(item => item.seeker_job_interests);

        var sql = query1.ToString();
        var res = query1.ToSelectedList(BusinessObjectsExtensions.ToBusiness);   //Todo ori. check. performance.
        return res;
      }
    }

    public List<Position> GetLikedPositions(int seekerId)
    {
      using (var db = new JobyJobsDB2())
      {
        var query1 = db.reactions
          .Where(item => item.active != false && item.reaction_type_id == (int)Enums.ReactionTypes.SeekerLikesPosition && item.seeker_id == seekerId)
          .Select(item => item.position);

        string sql = query1.ToString();
        var res = query1.ToSelectedList(BusinessObjectsExtensions.ToBusiness);
        return res;
      }
    }

    public List<Employer> GetLikedEmployers(int seekerId)
    {
      using (var db = new JobyJobsDB2())
      {
        var query1 = db.employers
          .Where(employer => employer.reactions.Any(item => item.active != false && item.reaction_type_id == (int)Enums.ReactionTypes.SeekerLikesPosition && item.seeker_id == seekerId));

        string sql = query1.ToString();
        var res = query1.ToSelectedList(BusinessObjectsExtensions.ToBusiness);
        return res;
      }
    }

    public List<Position> GetLikingPositions(int seekerId)
    {
      using (var db = new JobyJobsDB2())
      {
        var query1 = db.reactions
          .Where(item => item.active != false && item.reaction_type_id == (int)Enums.ReactionTypes.EmployerPersonLikesSeekerAndPosition && item.seeker_id == seekerId)
          .Select(item => item.position);

        string sql = query1.ToString();
        var res = query1.ToSelectedList(BusinessObjectsExtensions.ToBusiness);
        return res;
      }
    }

    public List<Employer> GetLikingEmployers(int seekerId)
    {
      using (var db = new JobyJobsDB2())
      {
        var query1 = db.employers
          .Where(employer => employer.reactions.Any(item => item.active != false && item.reaction_type_id == (int)Enums.ReactionTypes.EmployerPersonLikesSeekerAndPosition && item.seeker_id == seekerId));

        string sql = query1.ToString();
        var res = query1.ToSelectedList(BusinessObjectsExtensions.ToBusiness);
        return res;
      }
    }

    /// <summary>
    /// Returns the seekers that were liked by the given employerPersonId as candidates for the specified position.
    /// Note: I Liked.
    /// </summary>
    /// <param name="positionId"></param>
    /// <re turns></returns>
    public List<Seeker> GetLikedCandidates(int employerPersonId, int positionId)
    {
      using (var db = new JobyJobsDB2())
      {
        var query1 = db.reactions
          .Where(item => item.active != false && item.reaction_type_id == (int)Enums.ReactionTypes.EmployerPersonLikesSeekerAndPosition && item.position_id == positionId && item.employer_person_id == employerPersonId)
          .Select(item => item.seeker)
          .Include(item => item.user).Include(item => item.seeker_resumes).Include(item => item.seeker_job_interests); ;

        string sql = query1.ToString();
        var res = query1.ToSelectedList(BusinessObjectsExtensions.ToBusiness);
        return res;
      }
    }

    /// <summary>
    /// Returns the seekers that had liked the current position.
    /// Note: Liked me.
    /// </summary>
    /// <param name="positionId"></param>
    /// <returns></returns>
    public List<Seeker> GetLikingCandidates(int positionId)
    {
      using (var db = new JobyJobsDB2())
      {
        var query1 = db.reactions
          .Where(item => item.position_id == positionId && item.active != false && item.reaction_type_id == (int)Enums.ReactionTypes.SeekerLikesPosition)
          .Select(item => item.seeker)
            .Include(item => item.user).Include(item => item.seeker_resumes).Include(item => item.seeker_job_interests); ;

        string sql = query1.ToString();
        var res = query1.ToSelectedList(BusinessObjectsExtensions.ToBusiness);
        return res;
      }
    }


    //public List<Seeker> GetEmployerPersonMatches(int employerPersonId)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    var query1 = db.reactions.Where(item => !(item.reaction_disabled != null && item.reaction_disabled == true)
    //                                            && item.reaction_type_id == (int)Enums.ReactionTypes.EmployerPersonLikesSeekerAndPosition
    //                                            && item.employer_person_id == employerPersonId)
    //      .Where(item => db.reactions.Any(item0 => item0.seeker_id == item.seeker_id && !(item0.reaction_disabled != null && item0.reaction_disabled == true) && item0.reaction_type_id == (int)Enums.ReactionTypes.SeekerLikesPosition
    //                                               && item0.position.employer.employer_persons.Any(item1 => item1.employer_person_id == employerPersonId)))
    //      .Select(item => item.seeker)
    //      .Include(item => item.user).Include(item => item.seeker_resumes).Include(item => item.seeker_job_interests);

    //    var sql = query1.ToString();
    //    var res = query1.ToSelectedList(BusinessObjectsExtensions.ToBusiness);   //Todo ori. check. performance.
    //    return res;
    //  }
    //}

    //public List<Seeker> GetEmployerMatches(int employerId)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    var query1 = db.reactions.Where(item => !(item.reaction_disabled != null && item.reaction_disabled == true)
    //                                            && item.reaction_type_id == (int)Enums.ReactionTypes.EmployerPersonLikesSeekerAndPosition
    //                                            && item.employer_persons.employer_id == employerId)
    //      .Where(item => db.reactions.Any(item0 => item0.seeker_id == item.seeker_id && !(item0.reaction_disabled != null && item0.reaction_disabled == true) && item0.reaction_type_id == (int)Enums.ReactionTypes.SeekerLikesPosition
    //                                               && item0.position.employer_id == employerId))
    //      .Select(item => item.seeker)
    //      .Include(item => item.user).Include(item => item.seeker_resumes).Include(item => item.seeker_job_interests);

    //    var sql = query1.ToString();
    //    var res = query1.ToSelectedList(BusinessObjectsExtensions.ToBusiness);   //Todo ori. check. performance.
    //    return res;
    //  }
    //}



    ///// <summary>
    ///// Returns all the seekers that had liked the current employer.
    ///// </summary>
    ///// <param name="seekerId"></param>
    ///// <returns></returns>
    //public   List<Seeker> GetAllEmployerLikes(int employerId)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    var items = db.reactions.Where(item => item.seeker_id == employerId
    //                                           && item.reaction_type_id == (int)Enums.ReactionTypes.SeekerLikesEmployer)
    //      .Select(item => item.seeker).AsEnumerable();
    //    return items.Select(ServiceObjectsExtensions.ToBusiness).ToList();
    //  }
    //}
    ///// <summary>
    ///// Returns all the seekers that are liked by the current employer.
    ///// </summary>
    ///// <param name="seekerId"></param>
    ///// <returns></returns>
    //public   List<Seeker> GetAllEmployerInitiatedLikes(int employerId)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    var items = db.reactions.Where(item => item.seeker_id == employerId
    //                                           && item.reaction_type_id == (int)Enums.ReactionTypes.EmployerLikesSeeker)
    //      .Select(item => item.seeker).AsEnumerable();
    //    return items.Select(ServiceObjectsExtensions.ToBusiness).ToList();
    //  }
    //}


    #region Private methods
    private void SetSeekerEmployerReaction(Enums.ReactionTypes reaction, int seekerId, int? employerId = null, int? employerPersonId = null, int? positionId = null, bool disable = false)
    {
      using (var db = new JobyJobsDB2())
      {
        switch (reaction)
        {
          case Enums.ReactionTypes.SeekerLikesEmployer:
          case Enums.ReactionTypes.EmployerLikesSeeker:
            {
              reaction foundItem;
              if (!db.reactions.GetAny(item => item.seeker_id == seekerId && item.employer_id == employerId, out foundItem))
              {
                if (disable)   //in case disable and none found, return.
                  return;

                db.reactions.Add(new reaction()
                {
                  seeker_id = seekerId,
                  employer_id = employerId,
                  reaction_type_id = (int)reaction,
                  active = true
                });
                db.SaveChanges();
                return;
              }

              foundItem.reaction_type_id = (int)reaction;
              foundItem.reaction_disabled = disable;   //set disabled if necessary.
              db.Entry(foundItem).State = EntityState.Modified;
              db.SaveChanges();
            }
            break;
          case Enums.ReactionTypes.SeekerLikesPosition:
            {
              reaction foundItem;
              if (!db.reactions.GetAny(item => item.seeker_id == seekerId && item.position_id == positionId, out foundItem))
              {
                if (disable)   //in case disable and none found, return.
                  return;

                db.reactions.Add(new reaction()
                {
                  seeker_id = seekerId,
                  position_id = positionId,
                  reaction_type_id = (int)reaction,
                  active = true
                });
                db.SaveChanges();
                return;
              }

              foundItem.reaction_type_id = (int)reaction;
              foundItem.reaction_disabled = disable;   //set disabled if necessary.
              db.Entry(foundItem).State = EntityState.Modified;
              db.SaveChanges();
            }
            break;
          case Enums.ReactionTypes.EmployerPersonLikesSeeker:
            {
              reaction foundItem;
              if (!db.reactions.GetAny(item => item.employer_person_id == employerPersonId && item.seeker_id == seekerId, out foundItem))
              {
                if (disable)   //in case disable and none found, return.
                  return;

                db.reactions.Add(new reaction()
                {
                  employer_person_id = employerPersonId,
                  seeker_id = seekerId,
                  reaction_type_id = (int)reaction,
                  active = true
                });
                db.SaveChanges();
                return;
              }

              foundItem.reaction_type_id = (int)reaction;
              foundItem.reaction_disabled = disable;   //set disabled if necessary.
              db.Entry(foundItem).State = EntityState.Modified;
              db.SaveChanges();
            }
            break;
          case Enums.ReactionTypes.EmployerPersonLikesSeekerAndPosition:
            {
              reaction foundItem;
              if (!db.reactions.GetAny(item => item.employer_person_id == employerPersonId &&
                  item.seeker_id == seekerId && item.position_id == positionId, out foundItem))
              {
                if (disable)   //in case disable and none found, return.
                  return;

                db.reactions.Add(new reaction()
                {
                  employer_person_id = employerPersonId,
                  seeker_id = seekerId,
                  position_id = positionId,
                  reaction_type_id = (int)reaction,
                  active = true
                });
                db.SaveChanges();
                return;
              }

              foundItem.reaction_type_id = (int)reaction;
              foundItem.reaction_disabled = disable;   //set disabled if necessary.
              db.Entry(foundItem).State = EntityState.Modified;
              db.SaveChanges();
            }
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof(reaction), reaction, null);
        }

      }
    }
    #endregion

  }
}
