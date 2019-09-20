// Copyright (c) 2018 Google LLC.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not
// use this file except in compliance with the License. You may obtain a copy of
// the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations under
// the License.

// [START cloud_game_servers_scaling_policy_list]

using System;
using System.Collections.Generic;
using Google.Api.Gax;
using Google.Cloud.Gaming.V1Alpha;

namespace Gaming.ScalingPolicies
{
    class ListScalingPoliciesSamples
    {
        /// <summary>
        /// List scaling policies
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        public List<string> ListScalingPolicies(string projectId = "YOUR-PROJECT-ID")
        {
            // Initialize the client
            var client = ScalingPoliciesServiceClient.Create();

            // Construct the request
            string parent = $"projects/{projectId}/locations/global";

            // Call the API
            try
            {
                var response = client.ListScalingPolicies(parent);

                // Inspect the result
                List<string> result = new List<string>();
                bool hasMore = true;
                Page<ScalingPolicy> currentPage;
                while (hasMore)
                {
                    currentPage = response.ReadPage(pageSize: 10);

                    // Read the result in a given page
                    foreach (var policy in currentPage)
                    {
                        Console.WriteLine($"Scaling policy found: {policy.Name}");
                        result.Add(policy.Name);
                    }
                    hasMore = currentPage != null; 
                };

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ListScalingPolicies error:");
                Console.WriteLine($"{e.Message}");
                throw;
            }
        }
    }
}

// [END cloud_game_servers_scaling_policy_list]
