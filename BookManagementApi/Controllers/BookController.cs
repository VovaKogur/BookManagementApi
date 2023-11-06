using ASP.DataAccess.Repository.IRepository;
using AutoMapper;
using BookManagementApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Models;
using Utility;

namespace BookManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPdfService _pdfService;

        public BookController(IUnitOfWork unitOfWork, IMapper mapper, IPdfService pdfService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _pdfService = pdfService;
        }

        [HttpPost, Route(RESTfulEndpointConstants.Books.ADD_BOOK)]
        public IActionResult AddBook([FromForm] BookDTO bookDto)
        {
            if (bookDto.PdfFile == null || bookDto.PdfFile.Length == 0)
            {
                return BadRequest("PDF is required.");
            }

            if (bookDto.PdfFile.Length > 5 * 1024 * 1024)
            {
                return BadRequest("File size exceeds 5 MB.");
            }
            byte[] fileData;
            using (var binaryReader = new BinaryReader(bookDto.PdfFile.OpenReadStream()))
            {
                fileData = binaryReader.ReadBytes((int)bookDto.PdfFile.Length);
            }


            var extractedText = _pdfService.ExtractTextFromPdfPage(fileData);

            var book = _mapper.Map<Book>(bookDto);

            book.Content = extractedText;

            _unitOfWork.Book.Add(book);

            return CreatedAtAction(nameof(Book), new { id = book.Id }, book);
        }
        [HttpPut, Route(RESTfulEndpointConstants.Books.UPDATE)]
        public IActionResult UpdateBook([FromBody] Book bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            _unitOfWork.Book.Update(book);
            var response = new
            {
                Code = "Updated",
                Message = "Book Updated!"
            };
            return Ok(response);
        }

        [HttpDelete, Route(RESTfulEndpointConstants.Books.REMOVE_BY_ID)]
        public IActionResult RemoveBookById(int bookId)
        {
            _unitOfWork.Book.RemoveById(bookId);
            var response = new
            {
                Code = "Deleted",
                Message = "Book Deleted!"
            };
            return Ok(response);
        }
        [HttpGet]
        [Route(RESTfulEndpointConstants.Books.SEARCH)]
        public IActionResult SearchBooksByPhrase(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
            {
                return BadRequest("Search phrase is required.");
            }
            var booksWithPhrase = _unitOfWork.Book.GetAll()
                .Where(b => b.Content!=null && b.Content.Contains(phrase))
                .ToList();

            var bookDtos = _mapper.Map<List<BookDTO>>(booksWithPhrase);

            return Ok(bookDtos);
        }

    }
}
