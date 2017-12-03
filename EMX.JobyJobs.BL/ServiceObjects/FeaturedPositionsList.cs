using System;
using System.Collections.Generic;

namespace EMX.JobyJobs.BL.ServiceObjects
{
    public class FeaturedPositionsList : PositionsSearchResults
    {
        public FeaturedPositionsList(List<Position> data)
            : base(data, 5)
        {
        }
    }

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
    }
}
