namespace TransactionsAPI.Models
{
    public record TransactionRequest
    {
        public required int TransactionId { get; init; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required TransactionType TransactionType { get; init; }
        public required DateTime TransactionDate { get; init; }
        public required int TransactionQuantity { get; init; }
    }
}
