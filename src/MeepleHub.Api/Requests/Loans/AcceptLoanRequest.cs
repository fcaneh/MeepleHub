namespace MeepleHub.Api.Requests.Loans
{
    public class AcceptLoanRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
    }
}
