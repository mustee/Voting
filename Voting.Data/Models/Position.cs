using System;

namespace Voting.Data.Models
{
    public class Position : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
