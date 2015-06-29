using PactNet.Mocks.MockHttpService;
using System;
using Xunit;

namespace PactNet.Tests.IntegrationTests
{
    public class PactBuilderIntegrationTests : IUseFixture<IntegrationTestsMyApiPact>
    {
        private IMockProviderService _mockProviderService;
        private string _mockProviderServiceBaseUri;

        public void SetFixture(IntegrationTestsMyApiPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
            _mockProviderService.ClearInteractions();
        }

        [Fact]
        public void WhenNotRegisteringAnyInteractions_VerificationSucceeds()
        {
            _mockProviderService.VerifyInteractions();
        }

        [Fact]
        public void Build_WhenTwoInstancesOfPactBuilderExists_ShouldNotCauseAnExceptionToBeThrown()
        {
            IPactBuilder first = new PactBuilder()
                .ServiceConsumer("Consumer")
                .HasPactWith("Some Producer");

            IPactBuilder second = new PactBuilder()
                .ServiceConsumer("Consumer")
                .HasPactWith("Another Producer");

            first.MockService(1234);
            second.MockService(5678);

            first.Build();

            Exception e = Record.Exception(() => second.Build());

            Assert.Null(e);
        }
    }
}
