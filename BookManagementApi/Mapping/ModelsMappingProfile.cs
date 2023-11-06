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
            // Book mappings
            CreateMap<Book, BookDTO>()
                .ForMember(dto => dto.Category.Name, conf => conf.MapFrom(ol => ol.Category.Name))
                .ForMember(dto => dto.AverageRating, conf => conf.MapFrom(ol => ol.Reviews.Any() ? ol.Reviews.Average(r => r.Rating) : 0));

            CreateMap<BookDTO, Book>()
                .ForMember(entity => entity.Category, conf => conf.Ignore()) // Assume that we handle the category assignment separately
                .ForMember(entity => entity.Reviews, conf => conf.Ignore());

            // Category mappings
            CreateMap<Category, CategoryDTO>();

            CreateMap<CategoryDTO, Category>()
                .ForMember(entity => entity.Books, conf => conf.Ignore());

            // Review mappings
            CreateMap<Review, ReviewDTO>()
                .ForMember(dto => dto.BookId, conf => conf.MapFrom(ol => ol.Book.Id));

            CreateMap<ReviewDTO, Review>()
                .ForMember(entity => entity.Book, conf => conf.Ignore()); // Assume we handle book assignment separately
        }
    }
}
    
