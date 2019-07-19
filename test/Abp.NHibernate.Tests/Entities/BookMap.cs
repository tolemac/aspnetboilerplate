using Abp.NHibernate.EntityMappings;

namespace Abp.NHibernate.Tests.Entities
{
    public class BookMap : EntityMap<Book>
    {
        public BookMap() : base("Books")
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Name);

            this.MapFullAudited();
        }
    }
}