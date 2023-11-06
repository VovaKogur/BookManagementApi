using ASP.DataAccess.Repository.IRepository;
using AutoMapper;
using BookManagementApi.Controllers;
using BookManagementApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Models;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

public class BookControllerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IPdfService> _mockPdfService;
    private readonly BookController _controller;

    public BookControllerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _mockPdfService = new Mock<IPdfService>();
        _controller = new BookController(_mockUnitOfWork.Object, _mockMapper.Object, _mockPdfService.Object);
    }

    [Fact]
    public void AddBook_WithValidPdf_ReturnsCreatedAtAction()
    {
        var mockFile = new Mock<IFormFile>();
        var content = new MemoryStream();
        var writer = new BinaryWriter(content);
        writer.Write("dummy pdf content");
        content.Position = 0; // Reset the position after writing to the stream.

        mockFile.Setup(_ => _.OpenReadStream()).Returns(content);
        mockFile.Setup(_ => _.FileName).Returns("test.pdf");
        mockFile.Setup(_ => _.Length).Returns(content.Length);

        var bookDto = new BookDTO { PdfFile = mockFile.Object };
        var book = new Book();

        _mockPdfService.Setup(service => service.ExtractTextFromPdfPage(It.IsAny<byte[]>())).Returns("Extracted content");
        _mockMapper.Setup(m => m.Map<Book>(bookDto)).Returns(book);

        var result = _controller.AddBook(bookDto);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        _mockUnitOfWork.Verify(u => u.Book.Add(It.IsAny<Book>()), Times.Once());
        Assert.Equal("Book", createdAtActionResult.ActionName);
    }

    [Fact]
    public void UpdateBook_WithValidData_ReturnsOkResult()
    {
        var bookDto = new Book();
        var book = new Book();

        _mockMapper.Setup(m => m.Map<Book>(bookDto)).Returns(book);

        var result = _controller.UpdateBook(bookDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        _mockUnitOfWork.Verify(u => u.Book.Update(It.IsAny<Book>()), Times.Once());
        Assert.Equal("Updated", (okResult.Value as dynamic).Code);
    }

    [Fact]
    public void RemoveBookById_WithExistingId_ReturnsOkResult()
    {
        var bookId = 1;

        var result = _controller.RemoveBookById(bookId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        _mockUnitOfWork.Verify(u => u.Book.RemoveById(It.IsAny<int>()), Times.Once());
        Assert.Equal("Deleted", (okResult.Value as dynamic).Code);
    }

    [Fact]
    public void SearchBooksByPhrase_WithMatchingBooks_ReturnsOkResult()
    {
        var phrase = "test";
        var books = new List<Book>
        {
            new Book { Content = "This is a test content" }
        }.AsQueryable();

        _mockMapper.Setup(m => m.Map<List<BookDTO>>(It.IsAny<List<Book>>())).Returns(new List<BookDTO>());

        var result = _controller.SearchBooksByPhrase(phrase);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedBooks = Assert.IsType<List<BookDTO>>(okResult.Value);
        Assert.Single(returnedBooks);
    }
}
