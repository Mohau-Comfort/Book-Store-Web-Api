using AutoMapper;
using BookStore.Api.Data;
using BookStore.Api.Models;

namespace BookStore.Api.Helpers
{
    public class ApplicationMapper : Profile
    {
        // class to help map the methods from different repositries

        public ApplicationMapper()
        {
            CreateMap<Books, BookModel>().ReverseMap();
        }

    }
}
