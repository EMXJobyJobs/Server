namespace EMX.JobyJobs.BL.BusinessObjects
{
  //public class FeaturedSeekersList : SeekersSearchResults
  //{
  //  public FeaturedSeekersList(List<Position> data)
  //      : base(data, 5)
  //  {
  //  }
  //}

  /// <summary>
  /// Defines the criterias for the search of seekers.
  /// </summary>
  public class SeekersSearchCriteria
  {
    public string SearchText { get; set; }
    //public int? FieldId { get; set; }
    //public int? ProfessionId { get; set; }
    //public Enums.PositionTypes? PositionType { get; set; }
    public int? AgeMin { get; set; }

    public int? AgeMax { get; set; }
    /// <summary>
    /// Maximal salary
    /// </summary>
    public int? MaxSalary { get; set; }
    /// <summary>
    /// SalaryPerMonth: True or false; false=SalaryPerYear
    /// </summary>
    public bool? MaxSalaryPerMonth { get; set; }
    public string Location { get; set; }

    public SeekersSearchCriteria()
    {

    }

    public SeekersSearchCriteria(string searchText, int ageMin, int ageMax, int? salary, bool? salaryPerMonth, string location)
    {
      SearchText = searchText;
      AgeMin = ageMin;
      AgeMax = ageMax;
      MaxSalary = salary;
      MaxSalaryPerMonth = salaryPerMonth;
      Location = location;
    }
  }
  //public class SeekersSearchResults : Dictionary<int, Position>       //Dictionary. Key: precedence (1-based), Value: seeker;
  //{
  //  public SeekersSearchResults(List<Position> data)
  //  {
  //    int newPrecedence = 1;
  //    foreach (var item in data)
  //    {
  //      //item.Precedence = newPrecedence;
  //      this.Add(newPrecedence++, item);
  //    }
  //  }
  //  protected SeekersSearchResults(List<Position> data, int maxItems)
  //  {
  //    if (data.Count > maxItems)
  //    {
  //      throw new Exception($"more than {maxItems} seekers in featured list");
  //    }

  //    int newPrecedence = 1;
  //    foreach (var item in data)
  //    {
  //      //item.Precedence = newPrecedence;
  //      this.Add(newPrecedence++, item);
  //    }
  //  }
  //}
}
