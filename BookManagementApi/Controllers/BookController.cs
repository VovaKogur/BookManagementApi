using ASP.DataAccess.Repository.IRepository;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
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

        public BookController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost, Route(RESTfulEndpointConstants.Books.ADD_BOOK)]
        [RequestFormLimits(MultipartBodyLengthLimit = 5 * 1024 * 1024, ValueCountLimit = 4, ValueLengthLimit = 1024)]
        [Consumes("multipart/form-data")]
        public IActionResult AddBook([FromForm] BookDTO bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            _unitOfWork.Book.Add(book);

            var response = new
            {
                Code = "Success",
                Message = "Book Added!"
            };
            return Ok(response);
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
    }
}
