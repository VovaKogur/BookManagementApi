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
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost, Route(RESTfulEndpointConstants.Categories.ADD_CATEGORY)]
        public IActionResult AddCategoryAsync([FromBody] CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _unitOfWork.Category.Add(category);

            var response = new
            {
                Code = "Success",
                Message = "Category Added!"
            };
            return Created("", value: response);
        }

    }
}
