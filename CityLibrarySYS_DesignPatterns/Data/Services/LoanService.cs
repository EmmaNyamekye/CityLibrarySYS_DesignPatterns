using CityLibrarySYS_DesignPatterns.Data.Services.Observers;
using CityLibrarySYS_DesignPatterns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    public class LoanService : ILoanService
    {
        // Publisher/Subject event declaration using the modern async Func pattern
        public event Func<LoanEvent, Task>? OnLoanEvent;

        private readonly IMemberService _memberService;
        private readonly IBookService _bookService;
        private readonly LibraryDatabaseContext _context;
        private readonly ILoanRules _loanRules;

        public LoanService(
            IMemberService memberService,
            IBookService bookService,
            LibraryDatabaseContext context,
            MemberStatusObserver observer,
            ILoanRules loanRules)
        {
            _memberService = memberService;
            _bookService = bookService;
            _context = context;
            _loanRules = loanRules;

            // Observer subscription: The observer instance is injected and subscribes here
            OnLoanEvent += observer.HandleLoanEvent;
        }

        // Implementation of ILoanService.CreateLoan
        public async Task<(bool Success, string Message)> CreateLoan(int memberId, List<Book> loanCart)
        {
            // --- CRITICAL: Pre-loan Check/Clear Status Logic ---
            // Calls the logic in MemberService to check if the fine period is over,
            // clear the status if expired, or deny the loan if still inactive.
            var (canBorrow, statusMessage) = await _memberService.CheckAndClearInactiveStatus(memberId);

            if (!canBorrow)
            {
                return (false, statusMessage);
            }

            int newLoanId = GetNextLoanId();

            // Loan creation proceeds...
            foreach (var book in loanCart)
            {
                var loanItem = new LoanItem
                {
                    MemberId = memberId,
                    BookId = book.BookID,
                    LoanDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(_loanRules.LoanDurationDays),
                    Status = 'O'
                };
                await _context.LoanItems.AddAsync(loanItem);
                await _bookService.ChangeBookStatus(book.BookID, 'L');
            }
            await _context.SaveChangesAsync();

            return (true, $"Loan {newLoanId} successfully created. Status: {statusMessage}");
        }

        // Implementation of ILoanService.ReturnBook (API entry point for returns)
        public async Task ReturnBook(LoanItem loanItem)
        {
            var existingItem = await _context.LoanItems.FirstOrDefaultAsync(li => li.LoanItemId == loanItem.LoanItemId);
            if (existingItem != null)
            {
                existingItem.ReturnDate = DateTime.Now;
                existingItem.Status = 'R';
                _context.LoanItems.Update(existingItem);
                await _context.SaveChangesAsync();

                // Trigger the event handler logic
                await BookReturned(existingItem);
            }
        }

        // Implementation of ILoanService.BookReturned (Internal event handler logic)
        public async Task BookReturned(LoanItem loanItem)
        {
            await _bookService.ChangeBookStatus(loanItem.BookId, 'A');

            // Check for lateness and calculate duration
            var isLate = loanItem.ReturnDate.HasValue && loanItem.ReturnDate.Value > loanItem.DueDate;

            int lateDays = 0;
            if (isLate)
            {
                // Calculate the exact late duration in days (rounded up)
                TimeSpan lateDuration = loanItem.ReturnDate!.Value - loanItem.DueDate;
                lateDays = (int)Math.Ceiling(lateDuration.TotalDays);
            }

            // Notify Observers
            if (OnLoanEvent != null)
            {
                var eventType = isLate ? "BookReturnedLate" : "BookReturnedOnTime";
                var message = isLate
                    ? $"Book {loanItem.BookId} returned late by {lateDays} day(s) by member {loanItem.MemberId}."
                    : $"Book {loanItem.BookId} returned on time by member {loanItem.MemberId}.";

                var eventData = new LoanEvent
                {
                    MemberId = loanItem.MemberId,
                    LoanId = loanItem.LoanItemId,
                    EventType = eventType,
                    Message = message,
                    LateDurationDays = lateDays // Pass the calculated late duration
                };

                await OnLoanEvent.Invoke(eventData);
            }
        }

        // Remaining methods (GetNextLoanId, GetOutstandingLoanItemsByBookId) unchanged
        public int GetNextLoanId()
        {
            return (_context.LoanItems.Any() ? _context.LoanItems.Max(l => l.LoanItemId) : 0) + 1;
        }

        public async Task<IEnumerable<LoanItem>> GetOutstandingLoanItemsByBookId(int bookId)
        {
            return await _context.LoanItems
                .Where(li => li.BookId == bookId && li.Status == 'O')
                .ToListAsync();
        }
    }
}