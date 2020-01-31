using Microsoft.Azure.Cosmos.Table;

namespace Spectrum
{
    public class GuessEntity : TableEntity
    {
        public GuessEntity(string gameId, string guesserName)
        {
            this.PartitionKey = gameId; this.RowKey = guesserName;
        }
        public GuessEntity() { }
        public double Guess { get; set; }
    }
}
