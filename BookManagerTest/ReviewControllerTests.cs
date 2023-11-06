using ASP.DataAccess.Repository.IRepository;
using BookManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Moq;

public class ReviewControllerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly ReviewController _controller;

    public ReviewControllerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _controller = new ReviewController(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task PostReview_BookNotExists_ReturnsNotFound()
    {
        var review = new Review { BookId = 1, ReviewerEmail = "test@example.com" };
        _mockUnitOfWork.Setup(x => x.Book.FindById(review.BookId)).Returns((Book)null);

        var result = await _controller.PostReview(review);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task PostReview_ReviewAlreadyExists_ReturnsBadRequest()
    {
        var review = new Review { BookId = 1, ReviewerEmail = "test@example.com" };
        _mockUnitOfWork.Setup(x => x.Book.FindById(review.BookId)).Returns(new Book());


        var result = await _controller.PostReview(review);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task PostReview_ValidReview_ReturnsCreatedAtActionResult()
    {
        var review = new Review { BookId = 1, ReviewerEmail = "test@example.com" };
        _mockUnitOfWork.Setup(x => x.Book.FindById(review.BookId)).Returns(new Book());

        var result = await _controller.PostReview(review);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.NotNull(createdAtActionResult.Value);
    }

    [Fact]
    public async Task GetReview_ReviewNotExists_ReturnsNotFound()
    {
        var reviewId = 1;
        _mockUnitOfWork.Setup(x => x.Review.GetById(reviewId)).Returns((Review)null);

        var result = await _controller.GetReview(reviewId);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetReview_ReviewExists_ReturnsReview()
    {
        var review = new Review { Id = 1, BookId = 1, ReviewerEmail = "test@example.com" };
        _mockUnitOfWork.Setup(x => x.Review.GetById(review.Id)).Returns(review);

        var result = await _controller.GetReview(review.Id);

        var actionResult = Assert.IsType<ActionResult<Review>>(result);
        Assert.Equal(review.Id, actionResult.Value.Id);
    }

}

