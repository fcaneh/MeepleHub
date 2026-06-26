using System;
using System.Collections.Generic;
using System.Text;
using MeepleHub.Domain.Entities;
using MeepleHub.Domain.Interfaces;
using MeepleHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeepleHub.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly MeepleHubDbContext _context;

        public LoanRepository(MeepleHubDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
        }

        public async Task<bool> HasActiveLoanForUserGameAsync(int userGameId)
        {
            return await _context.Loans.AnyAsync(loan => loan.UserGameId == userGameId && loan.StartDate <= DateTime.UtcNow && loan.ReturnedAt == null);
        }

        public async Task<Loan?> GetLoanInvolvingUserAsync(int userId, int loanId)
        {
            return await _context.Loans.Include(loan => loan.UserGame).FirstOrDefaultAsync(loan => loan.Id == loanId &&(loan.UserGame!.UserId == userId || loan.BorrowerUserId == userId));
        }

        public async Task<IEnumerable<Loan>> GetLoansForUserAsync(int userId)
        {
            return await _context.Loans.Include(loan => loan.UserGame).Where(loan => loan.UserGame!.UserId == userId || loan.BorrowerUserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansForUserGameAsync(int userId, int userGameId)
        {
            return await _context.Loans.Include(loan => loan.UserGame).Where(loan => loan.UserGameId == userGameId && loan.UserGame!.UserId == userId).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
