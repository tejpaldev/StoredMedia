using System;

namespace StoreyedMedia.DAL.Mappers
{
    public class MapperFactory
    {
        public IDataMapper GetMapper(Type dtoType)
        {
            switch (dtoType.Name)
            {
                case "Contact":
                    return new ContactMapper();
                case "Alert":
                    return new AlertMapper();
                case "Tags":
                    return new TagsMapper();
                case "Source":
                    return new SourcesMapper();
                case "Story":
                    return new StoryMapper();
                case "Categories":
                    return new CategoriesMapper();
                case "Media":
                    return new MediaMapper();
                case "Comment":
                    return new CommentMapper();
                default:
                    return new ContactMapper();
            }
        }
    }
}
