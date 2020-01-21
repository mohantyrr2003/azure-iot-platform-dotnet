using System;
using System.Threading.Tasks;
using Mmm.Platform.IoT.Common.TestHelpers;
using Mmm.Platform.IoT.Config.Services;
using Mmm.Platform.IoT.Config.WebService.v1.Controllers;
using Moq;
using Xunit;

namespace Mmm.Platform.IoT.Config.WebService.Test.Controllers
{
    public class UserSettingsControllerTest : IDisposable
    {
        private readonly Mock<IStorage> mockStorage;
        private readonly UserSettingsController controller;
        private readonly Random rand;
        private bool disposedValue = false;

        public UserSettingsControllerTest()
        {
            this.mockStorage = new Mock<IStorage>();
            this.controller = new UserSettingsController(this.mockStorage.Object);
            this.rand = new Random();
        }

        [Fact]
        public async Task GetUserSettingAsyncTest()
        {
            var id = this.rand.NextString();
            var name = this.rand.NextString();
            var description = this.rand.NextString();

            this.mockStorage
                .Setup(x => x.GetUserSetting(It.IsAny<string>()))
                .ReturnsAsync(new
                {
                    Name = name,
                    Description = description
                });

            var result = await this.controller.GetUserSettingAsync(id) as dynamic;

            this.mockStorage
                .Verify(x => x.GetUserSetting(It.Is<string>(s => s == id)), Times.Once);

            Assert.Equal(result.Name.ToString(), name);
            Assert.Equal(result.Description.ToString(), description);
        }

        [Fact]
        public async Task SetUserSettingAsyncTest()
        {
            var id = this.rand.NextString();
            var name = this.rand.NextString();
            var description = this.rand.NextString();

            this.mockStorage
                .Setup(x => x.SetUserSetting(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new
                {
                    Name = name,
                    Description = description
                });

            var result = await this.controller.SetUserSettingAsync(id, new
            {
                Name = name,
                Description = description
            }) as dynamic;

            this.mockStorage
                .Verify(
                    x => x.SetUserSetting(
                        It.Is<string>(s => s == id),
                        It.Is<object>(o => this.CheckTheme(o, name, description))),
                    Times.Once);

            Assert.Equal(result.Name.ToString(), name);
            Assert.Equal(result.Description.ToString(), description);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    controller.Dispose();
                }

                disposedValue = true;
            }
        }

        private bool CheckTheme(object obj, string name, string description)
        {
            var dynamiceObj = obj as dynamic;
            return dynamiceObj.Name.ToString() == name && dynamiceObj.Description.ToString() == description;
        }
    }
}
