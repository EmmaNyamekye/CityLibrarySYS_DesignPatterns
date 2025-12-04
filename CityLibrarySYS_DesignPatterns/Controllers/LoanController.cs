using CityLibrarySYS_DesignPatterns.Data.Services;
using CityLibrarySYS_DesignPatterns.Data.Services.Commands; // NEW: Import Commands namespace
using CityLibrarySYS_DesignPatterns.Models;
using CityLibrarySYS_DesignPatterns.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityLibrarySYS_DesignPatterns.Controllers
{
    public class LoanController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IBookService _bookService;
        private readonly ILoanService _loanService;
        private readonly LoanCommandInvoker _commandInvoker; // NEW: Inject the Invoker

        public LoanController(
            IMemberService memberService,
            IBookService bookService,
            ILoanService loanService,
            LoanCommandInvoker commandInvoker) // NEW: Invoker is injected here
        {
            _memberService = memberService;
            _bookService = bookService;
            _loanService = loanService;
            _commandInvoker = commandInvoker; // Store the Invoker
        }

        private LoanViewModel GetViewModelFromSession()
        {
            var jsonString = HttpContext.Session.GetString("LoanViewModel");
            if (string.IsNullOrEmpty(jsonString))
            {
                return new LoanViewModel();
            }
            return JsonSerializer.Deserialize<LoanViewModel>(jsonString)!;
        }

        private void SetViewModelToSession(LoanViewModel viewModel)
        {
            var jsonString = JsonSerializer.Serialize(viewModel);
            HttpContext.Session.SetString("LoanViewModel", jsonString);
        }

        public IActionResult Index()
        {
            var viewModel = GetViewModelFromSession();

            if (viewModel.LoanCart.Any())
            {
                // Note: Task.Run().Wait() is generally bad practice in ASP.NET, 
                // but kept here to maintain existing synchronous flow in Index.
                Task.Run(async () => viewModel.NextLoanId = await _loanService.GetNextLoanId()).Wait();
            }

            return View(viewModel);
        }

        // --- Step 1: Validate Member (POST) ---
        [HttpPost]
        public async Task<IActionResult> ValidateMember(LoanViewModel model)
        {
            // Start with a clean slate for the session state
            var viewModel = new LoanViewModel();

            if (model.MemberIdInput == null)
            {
                ModelState.AddModelError("MemberIdInput", "Please enter the Member ID.");
                return View("Index", model);
            }

            // Using null-forgiving operator as per the existing pattern, though safer to use a null check
            var member = await _memberService.GetMemberById(model.MemberIdInput.Value);

            // Note: The original code assumes Member model has 'Status' as a string.
            // If you changed it back to 'char', you may need to adjust "I" to 'I' here.
            // Keeping it as "I" for now based on the original user-provided code.
            if (member == null)
            {
                ModelState.AddModelError("MemberIdInput", "Invalid Member ID. Please Try Again!");
                return View("Index", model);
            }

            if (member.Status == 'I') // ASSUMING 'char' status based on previous fix
            {
                ModelState.AddModelError("MemberIdInput", "This member is Inactive and cannot loan books.");
                return View("Index", model);
            }

            viewModel.MemberDetails = member;
            viewModel.MemberIdInput = model.MemberIdInput; // Retain the input ID

            // Calculate the next Loan ID once the member is validated
            viewModel.NextLoanId = await _loanService.GetNextLoanId();

            SetViewModelToSession(viewModel);
            return View("Index", viewModel);
        }

        // --- Step 2: Search Books (POST) ---
        [HttpPost]
        public async Task<IActionResult> SearchBooks(LoanViewModel model)
        {
            var viewModel = GetViewModelFromSession();

            // Re-validate that a member is in the session state
            if (viewModel.MemberDetails == null)
            {
                TempData["Message"] = "Please validate the Member ID before searching for books.";
                return RedirectToAction(nameof(Index));
            }

            viewModel.TitleSearch = model.TitleSearch;

            if (string.IsNullOrEmpty(viewModel.TitleSearch))
            {
                ModelState.AddModelError("TitleSearch", "Please enter a book title to search.");
                SetViewModelToSession(viewModel);
                return View("Index", viewModel);
            }

            // Fetch books that match the title and are available
            viewModel.AvailableBooks = (await _bookService.FindMatchingAvailableBooks(viewModel.TitleSearch))?.ToList() ?? new List<Book>();

            if (!viewModel.AvailableBooks.Any())
            {
                TempData["Message"] = "No books found or available with that title.";
            }

            SetViewModelToSession(viewModel);
            return View("Index", viewModel);
        }

        // --- Step 3a: Add to Cart (POST) ---
        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId)
        {
            var viewModel = GetViewModelFromSession();

            if (viewModel.MemberDetails == null)
            {
                TempData["Message"] = "Session lost. Please re-validate the Member ID.";
                return RedirectToAction(nameof(Index));
            }

            if (viewModel.LoanCart.Count >= 7)
            {
                TempData["Message"] = "Unfortunately, you can only collect 7 books at a time!";
                return RedirectToAction(nameof(Index));
            }

            var bookToAdd = await _bookService.GetBookById(bookId);

            if (bookToAdd == null)
            {
                TempData["Message"] = "Error: Book not found.";
            }
            else if (viewModel.LoanCart.Any(b => b.BookID == bookId))
            {
                TempData["Message"] = $"'{bookToAdd.Title}' is already in the cart.";
            }
            else
            {
                viewModel.LoanCart.Add(bookToAdd);
                TempData["Message"] = $"'{bookToAdd.Title}' added to cart.";
            }

            SetViewModelToSession(viewModel);
            return RedirectToAction(nameof(Index));
        }

        // --- Step 3b: Remove from Cart (POST) ---
        [HttpPost]
        public IActionResult RemoveFromCart(int bookId)
        {
            var viewModel = GetViewModelFromSession();

            var bookToRemove = viewModel.LoanCart.FirstOrDefault(b => b.BookID == bookId);
            if (bookToRemove != null)
            {
                viewModel.LoanCart.Remove(bookToRemove);
                TempData["Message"] = $"'{bookToRemove.Title}' removed from cart.";
            }

            SetViewModelToSession(viewModel);
            return RedirectToAction(nameof(Index));
        }

        // --- Step 4: Confirm Loan (POST) ---
        [HttpPost]
        public async Task<IActionResult> ConfirmLoan()
        {
            var viewModel = GetViewModelFromSession();

            if (viewModel.MemberDetails == null || !viewModel.LoanCart.Any())
            {
                TempData["Message"] = "Cannot confirm loan. Please ensure a member is validated and books are in the cart.";
                return RedirectToAction(nameof(Index));
            }

            // 1. Create the Concrete Command object
            var loanCommand = new CreateLoanCommand(
                loanService: _loanService,
                memberId: viewModel.MemberDetails.MemberId,
                loanCart: viewModel.LoanCart);

            // 2. Set the command on the Invoker
            _commandInvoker.SetCommand(loanCommand);

            // 3. Tell the Invoker to execute (The Invoker then calls loanCommand.Execute())
            await _commandInvoker.InvokeCommand();


            // Clear session and display confirmation
            var confirmedLoanId = viewModel.NextLoanId;
            var confirmedBookCount = viewModel.LoanCart.Count;
            HttpContext.Session.Remove("LoanViewModel");

            TempData["SuccessMessage"] = $"Loan ID {confirmedLoanId} created successfully. {confirmedBookCount} book(s) collected!";

            // Redirect to a fresh start
            return RedirectToAction(nameof(Index));
        }
    }
}