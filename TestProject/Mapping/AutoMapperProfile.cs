using AutoMapper;
using TestProject.Db.Entity;
using TestProject.Models;

namespace TestProject.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Node, NodeModel>();

            CreateMap<NodeModel, Node>()
                .ForMember(dest => dest.Children, opt => opt.Ignore());

            CreateMap<ExceptionRecord, ExceptionRecordModel>();
            CreateMap<ExceptionData, ExceptionDataModel>();
        }
    }
}
