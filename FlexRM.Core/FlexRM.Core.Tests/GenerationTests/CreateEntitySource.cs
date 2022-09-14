using FlexRM.Core.Application.Generation;
using FlexRM.Core.Domain.Entities.Configuration;

namespace FlexRM.Core.Tests.GenerationTests
{
    public class CreateEntitySource
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateSimpleEntity()
        {
            var entity = new Entity() { Id = Guid.NewGuid(), Name = "Test" };
            entity.Columns.Add(new Column() { Name = "TestColumn", Type = Domain.Enums.ColumnTypes.String });

            var generator = new EntitySourceGenerator(entity);
            var expected = "using System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing System.Text;\r\nusing System.Threading.Tasks;\r\n\r\nnamespace FlexRM.Generated.Domain.Entities\r\n{\r\n    public class Test\r\n    {\r\n        public string TestColumn { get; set; }\r\n    }\r\n}";
            var result = generator.Generate();
            Assert.AreEqual(expected, result);
        }
    }
}