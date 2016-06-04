using AutoMapper;
using SerializationClient.Models;
using SerializationTool.Models;

namespace SerializationTool.Startup
{
    /// <summary>
    /// Register mapper types.
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Resgister all mappings.
        /// </summary>
        public static void RegisterMappings()
        {
            Mapper
               .CreateMap<FolderModel, TreeViewItemModel>()
               .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name))
               .ForMember(x => x.ChildItems, opt => opt.MapFrom(s => s.SubFolderModels));
        }
    }
}
