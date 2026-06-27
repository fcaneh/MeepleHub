namespace MeepleHub.Api.Requests.Loans
{
    public class CreateLoanRequest
    {
        public int BorrowerUserId { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public string? Notes { get; set; }
    }
}
