using FlexRM.Core.Application.Generation;
using FlexRM.Core.Domain.Entities.Configuration;

namespace FlexRM.Core.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var entity = new Entity() { Id = Guid.NewGuid(), Name = "Test" };
            entity.Columns.Add(new Column() { Name = "TestColumn", Type = Domain.Enums.ColumnTypes.String });
            var generator = new EntitySourceGenerator(entity);
            var result = generator.Generate();
        }
    }
}