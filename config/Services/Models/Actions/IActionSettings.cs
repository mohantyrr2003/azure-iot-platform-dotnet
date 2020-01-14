﻿// Copyright (c) Microsoft. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mmm.Platform.IoT.Config.Services.Models.Actions
{
    public interface IActionSettings
    {
        ActionType Type { get; }

        // Note: This should always be initialized as a case-insensitive dictionary
        IDictionary<string, object> Settings { get; set; }

        Task InitializeAsync();
    }
}