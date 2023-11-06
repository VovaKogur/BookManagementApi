using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using ASP.DataAccess.Repository.IRepository;
using Models.DTO;
using Models.Models;
using BookManagementApi.Controllers;

public class CategoryControllerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CategoryController _controller;

    public CategoryControllerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();

        _controller = new CategoryController(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public void AddCategoryAsync_ValidCategory_ReturnsCreatedResult()
    {
        var categoryDto = new CategoryDTO { /* Initialize with test data */ };
        var category = new Category { /* Initialize with test data */ };

        _mockMapper.Setup(m => m.Map<Category>(It.IsAny<CategoryDTO>())).Returns(category);


        var result = _controller.AddCategoryAsync(categoryDto);


        _mockMapper.Verify(m => m.Map<Category>(categoryDto), Times.Once);

        _mockUnitOfWork.Verify(u => u.Category.Add(category), Times.Once);

        Assert.IsType<CreatedResult>(result);

        var createdResult = result as CreatedResult;
        Assert.NotNull(createdResult);

        Assert.Equal(201, createdResult.StatusCode);

        dynamic responseValue = createdResult.Value;
        Assert.NotNull(responseValue);
        Assert.Equal("Success", responseValue.Code);
        Assert.Equal("Category Added!", responseValue.Message);
    }

}
