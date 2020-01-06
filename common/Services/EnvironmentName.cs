﻿namespace Mmm.Platform.IoT.Common.Services
{
    public static class EnvironmentName
    {
        public static readonly string Development = "dev";
        public static readonly string Qa = nameof(Qa).ToLowerInvariant();
        public static readonly string Workbench = "wkbnch";
        public static readonly string Production = "prod";
    }
}