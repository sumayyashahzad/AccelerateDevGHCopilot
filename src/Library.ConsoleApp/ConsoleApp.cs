using Library.Infrastructure.Data;
using Library.ApplicationCore;
using Library.ApplicationCore.Entities;

namespace Library.ConsoleApp
{
    public class ConsoleApp
    {
        private readonly JsonData _jsonData;
        private readonly IPatronRepository _patronRepository;

        public ConsoleApp(JsonData jsonData, IPatronRepository patronRepository)
        {
            _jsonData = jsonData;
            _patronRepository = patronRepository;
        }

        public async Task CheckBookAvailabilityByTitle(string title)
        {
            await _jsonData.EnsureDataLoaded();

            var book = _jsonData.Books.FirstOrDefault(
                b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (book == null)
            {
                Console.WriteLine($"Book with title '{title}' not found.");
                return;
            }

            var bookItem = _jsonData.BookItems.FirstOrDefault(bi => bi.BookId == book.Id);
            if (bookItem == null)
            {
                Console.WriteLine($"No book item found for '{book.Title}'.");
                return;
            }

            var activeLoan = _jsonData.Loans.FirstOrDefault(
                loan => loan.BookItemId == bookItem.Id && loan.ReturnDate == null);

            if (activeLoan == null)
            {
                Console.WriteLine($"'{book.Title}' is available for loan.");
            }
            else
            {
                Console.WriteLine($"'{book.Title}' is currently on loan. Due date: {activeLoan.DueDate:d}");
            }
        }
    }
}