using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrum
{
    public class GameEntity : TableEntity
    {
        public GameEntity(string gameId, string psychic)
        {
            this.PartitionKey = gameId; this.RowKey = psychic;
        }
        public GameEntity() { }
        public string CategoryLeft { get; set; }
        public string CategoryRight { get; set; }
        public double Location { get; set; }
    }
}
