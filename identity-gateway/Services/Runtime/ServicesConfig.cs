﻿// Copyright (c) Microsoft. All rights reserved.

using System.Collections.Generic;
using Mmm.Platform.IoT.Common.Services.External;
using Mmm.Platform.IoT.Common.WebService.Auth;

namespace IdentityGateway.Services.Runtime
{
    public interface IServicesConfig : IUserManagementClientConfig, IAuthMiddlewareConfig
    {
        string PrivateKey { get; }
        string PublicKey { get; }
        string StorageAccountConnectionString { get; }
        string AzureB2CBaseUri { get; }
        string Port { get; }
        string SendGridAPIKey { get; }
    }

    public class ServicesConfig : IServicesConfig
    {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public Dictionary<string, List<string>> UserPermissions { get; set; }
        public string StorageAccountConnectionString { get; set; }
        public string AzureB2CBaseUri { get; set; }
        public string Port { get; set; }
        public string SendGridAPIKey { get; set; }
        public string UserManagementApiUrl { get; set; }
    }
}