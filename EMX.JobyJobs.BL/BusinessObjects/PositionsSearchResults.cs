using System;
using System.Collections.Generic;
using EMX.JobyJobs.Shared.Definitions;

namespace EMX.JobyJobs.BL.BusinessObjects
{
  /// <summary>
  /// Note: Seeker side only.
  /// </summary>
  public class FeaturedPositionsList : PositionsSearchResults
  {
    public FeaturedPositionsList(List<Position> data)
        : base(data, 5)
    {
    }
  }

  /// <summary>
  /// Defines the criterias for the search of positions.
  /// Note: Seeker side only.
  /// </summary>
  public class PositionsSearchCriteria
  {
    public string SearchText { get; set; }
    public int? FieldId { get; set; }
    public int? ProfessionId { get; set; }
    public Enums.PositionTypes? PositionType { get; set; }
    /// <summary>
    /// Minimal salary
    /// </summary>
    public int? Salary { get; set; }
    /// <summary>
    /// SalaryPerMonth: True or false; false=SalaryPerYear
    /// </summary>
    public bool? SalaryPerMonth { get; set; }
    public string Location { get; set; }

    public PositionsSearchCriteria()
    {

    }

    public PositionsSearchCriteria(string searchText, int? fieldId, int? professionId, Enums.PositionTypes? positionType, int? salary, bool? salaryPerMonth, string location)
    {
      SearchText = searchText;
      FieldId = fieldId;
      ProfessionId = professionId;
      PositionType = positionType;
      Salary = salary;
      SalaryPerMonth = salaryPerMonth;
      Location = location;
    }
  }

  /// <summary>
  /// Note: Seeker side only.
  /// </summary>
  public class PositionsSearchResults : Dictionary<int, Position>       //Dictionary. Key: precedence (1-based), Value: position;
  {
    public PositionsSearchResults(List<Position> data)
    {
      int newPrecedence = 1;
      foreach (var item in data)
      {
        //item.Precedence = newPrecedence;
        this.Add(newPrecedence++, item);
      }
    }
    protected PositionsSearchResults(List<Position> data, int maxItems)
    {
      if (data.Count > maxItems)
      {
        throw new Exception($"more than {maxItems} positions in featured list");
      }

      int newPrecedence = 1;
      foreach (var item in data)
      {
        //item.Precedence = newPrecedence;
        this.Add(newPrecedence++, item);
      }
    }

    //public class PositionsByStatusList : IDictionary<int, IList<Position>>
    //{

    //}
  }
}
