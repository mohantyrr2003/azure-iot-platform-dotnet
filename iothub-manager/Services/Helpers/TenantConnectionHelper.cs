// <copyright file="TenantConnectionHelper.cs" company="3M">
// Copyright (c) 3M. All rights reserved.
// </copyright>

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Logging;
using Mmm.Platform.IoT.Common.Services;
using Mmm.Platform.IoT.Common.Services.Exceptions;
using Mmm.Platform.IoT.Common.Services.External.AppConfiguration;

namespace Mmm.Platform.IoT.IoTHubManager.Services.Helpers
{
    public class TenantConnectionHelper : ITenantConnectionHelper
    {
        private const string TenantKey = "tenant:";
        private const string IotHubConnectionKey = ":iotHubConnectionString";
        private readonly IAppConfigurationClient appConfig;
        private readonly ILogger<TenantConnectionHelper> logger;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TenantConnectionHelper(
            IAppConfigurationClient appConfigurationClient,
            IHttpContextAccessor httpContextAccessor,
            ILogger<TenantConnectionHelper> logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.appConfig = appConfigurationClient;
            this.logger = logger;
        }

        // Gets the tenant name from the threads current token.
        private string TenantName
        {
            get
            {
                try
                {
                    return this.httpContextAccessor.HttpContext.Request.GetTenant();
                }
                catch (Exception ex)
                {
                    throw new Exception($"A valid tenant Id was not included in the Claim. " + ex);
                }
            }
        }

        public string GetIotHubConnectionString()
        {
            var appConfigurationKey = TenantKey + TenantName + IotHubConnectionKey;
            logger.LogDebug("App Configuration key for IoT Hub connection string for tenant {tenant} is {appConfigurationKey}", TenantName, appConfigurationKey);
            return appConfig.GetValue(appConfigurationKey);
        }

        public string GetIotHubName()
        {
            string currIoTHubHostName = null;
            IoTHubConnectionHelper.CreateUsingHubConnectionString(GetIotHubConnectionString(), (conn) =>
            {
                currIoTHubHostName = IotHubConnectionStringBuilder.Create(conn).HostName;
            });
            if (currIoTHubHostName == null)
            {
                throw new InvalidConfigurationException($"Invalid tenant information for HubConnectionstring.");
            }

            return currIoTHubHostName;
        }

        public RegistryManager GetRegistry()
        {
            RegistryManager registry = null;

            IoTHubConnectionHelper.CreateUsingHubConnectionString(GetIotHubConnectionString(), (conn) =>
            {
                registry = RegistryManager.CreateFromConnectionString(conn);
            });
            if (registry == null)
            {
                throw new InvalidConfigurationException($"Invalid tenant information for HubConnectionstring.");
            }

            return registry;
        }

        public JobClient GetJobClient()
        {
            JobClient job = null;

            IoTHubConnectionHelper.CreateUsingHubConnectionString(GetIotHubConnectionString(), conn =>
             {
                 job = JobClient.CreateFromConnectionString(conn);
             });
            if (job == null)
            {
                throw new InvalidConfigurationException($"Invalid tenant information for HubConnectionstring.");
            }

            return job;
        }
    }
}