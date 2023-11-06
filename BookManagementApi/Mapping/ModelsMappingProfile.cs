using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using Models.DTO;
using Models.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;

namespace BookManagementApi.Mapping
{
    public class ModelsMappingProfile : Profile
    {
        public ModelsMappingProfile()
        {
            CreateMap<Book, BookDTO>()
                  .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                  .ReverseMap();

            CreateMap<BookDTO, Book>()
            .ForMember(dest => dest.Content, opt => opt.Ignore());

            CreateMap<Category, CategoryDTO>();

            CreateMap<CategoryDTO, Category>()
                .ForMember(entity => entity.Books, conf => conf.Ignore());

            CreateMap<Review, ReviewDTO>()
                .ForMember(dto => dto.BookId, conf => conf.MapFrom(ol => ol.Book.Id));

            CreateMap<ReviewDTO, Review>()
                .ForMember(entity => entity.Book, conf => conf.Ignore()); // Assume we handle book assignment separately
        }
    }
}

