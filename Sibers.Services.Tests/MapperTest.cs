using AutoMapper;
using Sibers.Services.Automappers;

namespace Sibers.Services.Tests
{
    public class MapperTest
    {
        /// <summary>
        /// Тест на маппер
        /// </summary>
        [Fact]
        public void TestMap()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });

            config.AssertConfigurationIsValid();
        }
    }
}
