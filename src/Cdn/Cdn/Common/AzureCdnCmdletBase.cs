﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Management.Cdn;
using Microsoft.WindowsAzure.Commands.Common.CustomAttributes;

namespace Microsoft.Azure.Commands.Cdn.Common
{
    /// <summary>
    /// Base class of Azure Cdn Cmdlet.
    /// </summary>
    [GenericBreakingChange("When you bump up Az.CDN module version, there will be changes to parameters and output, please use the new version", "2.0.0")]
    public class AzureCdnCmdletBase : AzureRMCmdlet
    {
        private ICdnManagementClient _cdnManagementClient;

        private Dictionary<string, List<string>> _defaultRequestHeaders;

        public const string ObjectParameterSet = "ByObjectParameterSet";
        public const string FieldsParameterSet = "ByFieldsParameterSet";
        public const string ResourceIdParameterSet = "ByResourceIdParameterSet";
        public const string CacheExpirationActionParameterSet = "CacheExpirationActionParameterSet";
        public const string HeaderActionParameterSet = "HeaderActionParameterSet";
        public const string UrlRedirectActionParameterSet = "UrlRedirectActionParameterSet";
        public const string CacheKeyQueryStringActionParameterSet = "CacheKeyQueryStringActionParameterSet";
        public const string UrlRewriteActionParameterSet = "UrlRewriteActionParameterSet";
        public const string ByFieldsPrivateLinkParameterSet = "ByFieldsPrivateLinkParameterSet";


        /// <summary>
        /// Gets or sets the Cdn management client.
        /// </summary>
        public ICdnManagementClient CdnManagementClient
        {
            get
            {
                return _cdnManagementClient ??
                       (_cdnManagementClient =
                           AzureSession.Instance.ClientFactory.CreateArmClient<CdnManagementClient>(DefaultProfile.DefaultContext,
                               AzureEnvironment.Endpoint.ResourceManager));
            }
            set { _cdnManagementClient = value; }
        }

        /// <summary>
        /// Gets or sets the default headers send with rest requests.
        /// </summary>
        public Dictionary<string, List<string>> DefaultRequestHeaders
        {
            get
            {
                return _defaultRequestHeaders ??
                       (_defaultRequestHeaders =
                           new Dictionary<string, List<string>> { { "UserAgent", new List<string> { "PowerShell" } } });
            }
            set { _defaultRequestHeaders = value; }
        }

        public void ConfirmAction(bool force, string actionMessage, Action action)
        {
            if (force || ShouldContinue(actionMessage, ""))
            {
                action();
            }
        }
    }
}