using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Entities;

namespace MeepleHub.Domain.Interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetLoansForUserAsync(int userId);
        Task<IEnumerable<Loan>> GetLoansForUserGameAsync(int userId, int userGameId);
        Task<Loan?> GetLoanInvolvingUserAsync(int userId, int loanId);
        Task<bool> HasActiveLoanForUserGameAsync(int userGameId);
        Task AddAsync(Loan loan);
        Task SaveChangesAsync();
        
    }
}

