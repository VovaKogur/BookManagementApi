using ASP.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Utility;

namespace BookManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public ReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost, Route(RESTfulEndpointConstants.Reviews.POST_REVIEWS)]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            var book =  _unitOfWork.Book.FindById(review.BookId);
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            var existingReview =  _unitOfWork.Review.GetAll().
                FirstOrDefault(r => r.ReviewerEmail == review.ReviewerEmail && r.BookId == review.BookId);


            if (existingReview != null)
            {
                return BadRequest("Reviewer has already reviewed this book.");
            }

            _unitOfWork.Review.Add(review);
            _unitOfWork.Save();

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        [HttpGet("{id}"),Route(RESTfulEndpointConstants.Reviews.GET_REVIEW)]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review =  _unitOfWork.Review.GetById(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        
        private bool BookExists(int id)
        {
            return _unitOfWork.Book.GetAll().Any(e => e.Id == id);
        }
    }
}
