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

// [START cloud_game_servers_allocation_policy_list]

using System;
using System.Collections.Generic;
using Google.Api.Gax;
using Google.Cloud.Gaming.V1Alpha;

namespace Gaming.AllocationPolicies
{
    class ListAllocationPolicySamples
    {
        /// <summary>
        /// List Allocation Policies
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="policyId">The id of the game policy</param>
        public List<string> ListAllocationPolicy(
            string projectId = "YOUR-PROJECT-ID",
            string policyId = "YOUR-ALLOCATION-POLICY")
        {
            // Initialize the client
            AllocationPoliciesServiceClient client = AllocationPoliciesServiceClient.Create();

            // Construct the request
            string parent = $"projects/{projectId}/locations/global";
            var request = new ListAllocationPoliciesRequest
            {
                Parent = parent,
            };

            // Call the API
            try
            {
                var response = client.ListAllocationPolicies(parent);

                // Inspect the result
                List<string> result = new List<string>();
                bool hasMore = true;
                Page<AllocationPolicy> currentPage;
                while (hasMore)
                {
                    currentPage = response.ReadPage(pageSize: 10);

                    // Read the result in a given page
                    foreach (var policy in currentPage)
                    {
                        Console.WriteLine($"Allocation policy found: {policy.Name}");
                        result.Add(policy.Name);
                    }
                    hasMore = currentPage != null; 
                };

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ListAllocationPolicies error:");
                Console.WriteLine($"{e.Message}");
                throw;
            }
        }
    }
}

// [END cloud_game_servers_allocation_policy_list]