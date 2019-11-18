﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.IoTSolutions.DeviceTelemetry.Services;
using Microsoft.Azure.IoTSolutions.DeviceTelemetry.Services.Runtime;
using Mmm.Platform.IoT.Common.Services.Diagnostics;
using Mmm.Platform.IoT.Common.Services.Exceptions;
using Mmm.Platform.IoT.Common.Services.External.CosmosDb;
using Mmm.Platform.IoT.Common.Services.External.TimeSeries;
using Mmm.Platform.IoT.Common.Services.Helpers;
using Mmm.Platform.IoT.Common.TestHelpers;
using Moq;
using Newtonsoft.Json.Linq;
using Xunit;

namespace DeviceTelemetry.Services.Test
{
    public class MessagesTest
    {
        private const int SKIP = 0;
        private const int LIMIT = 1000;

        private readonly Mock<IStorageClient> storageClient;
        private readonly Mock<ITimeSeriesClient> timeSeriesClient;
        private readonly Mock<ILogger> logger;
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;
        private readonly Mock<IAppConfigurationHelper> appConfigHelper;
        private readonly IMessages messages;

        public MessagesTest()
        {
            var servicesConfig = new ServicesConfig()
            {
                MessagesConfig = new StorageConfig("database"),
                StorageType = "tsi"
            };
            this.storageClient = new Mock<IStorageClient>();
            this.timeSeriesClient = new Mock<ITimeSeriesClient>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
            this.appConfigHelper = new Mock<IAppConfigurationHelper>();
            this.logger = new Mock<ILogger>();
            this.messages = new Messages(
                servicesConfig,
                this.storageClient.Object,
                this.timeSeriesClient.Object,
                this.logger.Object,
                this.httpContextAccessor.Object,
                this.appConfigHelper.Object);
        }

        [Fact, Trait(Constants.TYPE, Constants.UNIT_TEST)]
        public async Task InitialListIsEmptyAsync()
        {
            // Arrange
            this.ThereAreNoMessagesInStorage();
            var devices = new string[] { "device1" };

            // Act
            var list = await this.messages.ListAsync(null, null, "asc", SKIP, LIMIT, devices);

            // Assert
            Assert.Empty(list.Messages);
            Assert.Empty(list.Properties);
        }

        [Fact, Trait(Constants.TYPE, Constants.UNIT_TEST)]
        public async Task GetListWithValuesAsync()
        {
            // Arrange
            this.ThereAreSomeMessagesInStorage();
            var devices = new string[] { "device1" };

            // Act
            var list = await this.messages.ListAsync(null, null, "asc", SKIP, LIMIT, devices);

            // Assert
            Assert.NotEmpty(list.Messages);
            Assert.NotEmpty(list.Properties);
        }

        [Fact, Trait(Constants.TYPE, Constants.UNIT_TEST)]
        public async Task ThrowsOnInvalidInput()
        {
            // Arrange
            var xssString = "<body onload=alert('test1')>";
            var xssList = new List<string>
            {
                "<body onload=alert('test1')>",
                "<IMG SRC=j&#X41vascript:alert('test2')>"
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidInputException>(async () => await this.messages.ListAsync(null, null, xssString, 0, LIMIT, xssList.ToArray()));
        }

        private void ThereAreNoMessagesInStorage()
        {
            this.timeSeriesClient.Setup(x => x.QueryEventsAsync(null, null, It.IsAny<string>(), SKIP, LIMIT, It.IsAny<string[]>()))
                .ReturnsAsync(new MessageList());
        }

        private void ThereAreSomeMessagesInStorage()
        {
            var sampleMessages = new List<Message>();
            var sampleProperties = new List<string>();

            var data = new JObject
            {
                { "data.sample_unit", "mph" },
                { "data.sample_speed", "10" }
            };

            sampleMessages.Add(new Message("id1", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), data));
            sampleMessages.Add(new Message("id2", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), data));

            sampleProperties.Add("data.sample_unit");
            sampleProperties.Add("data.sample_speed");

            this.timeSeriesClient.Setup(x => x.QueryEventsAsync(null, null, It.IsAny<string>(), SKIP, LIMIT, It.IsAny<string[]>()))
                .ReturnsAsync(new MessageList(sampleMessages, sampleProperties));
        }
    }
}
