using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Doctrina.ExperienceApi.Data.Tests
{
    public class IriTests
    {
        [Fact]
        public void Iri_DefaultValue_MustBeNull()
        {
            // Arrange
            var iri = new Iri();

            // Act
            var json = JsonConvert.SerializeObject(iri);

            // Assert
            json.ShouldBe("null");
        }

        [Fact]
        public void Iri_ObjectDefaultValue_MustBeNull()
        {
            // Arrange
            var obj = new
            {
                value = new Iri()
            };

            // Act
            var json = JsonConvert.SerializeObject(obj);

            // Assert
            json.ShouldBe("{\"value\":null}");
        }

        [Fact]
        public void Iri_Should_ParseValid_String()
        {
            // Arrange
            string verb = "http://adlnet.gov/expapi/verbs/attended";

            // Act
            // Assert
            Should.NotThrow(() =>
            {
                var parsed = new Iri(verb);
            });
        }
    }
}
