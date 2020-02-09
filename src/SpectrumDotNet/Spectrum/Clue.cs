using Microsoft.Azure.Cosmos.Table;

namespace Spectrum
{
    public class ClueEntity : TableEntity
    {
        public ClueEntity(string clueId)
        {
            this.PartitionKey = clueId; this.RowKey = clueId;
        }
        public ClueEntity() { }
        public string CategoryLeft { get; set; }
        public string CategoryRight { get; set; }
    }
}
