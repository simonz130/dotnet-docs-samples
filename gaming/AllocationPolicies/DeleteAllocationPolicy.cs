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

// [START cloud_game_servers_allocation_policy_delete]

using System;
using Google.Cloud.Gaming.V1Alpha;

namespace Gaming.AllocationPolicies
{
    class DeleteAllocationPolicySamples
    {
        /// <summary>
        /// Delete Allocation Policy
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="policyId">The id of the game policy</param>
        public string DeleteAllocationPolicy(
            string projectId = "YOUR-PROJECT-ID",
            string policyId = "YOUR-POLICY-ID")
        {
            // Initialize the client
            AllocationPoliciesServiceClient client = AllocationPoliciesServiceClient.Create();

            // Construct the request
            string parent = $"projects/{projectId}/locations/global";
            string policyName = $"{parent}/allocationPolicies/{policyId}";

            var request = new DeleteAllocationPolicyRequest
            {
                Name = policyName
            };

            // Call the API
            try
            {
                var result = client.DeleteAllocationPolicy(request);

                // Inspect the result
                return $"Allocation Policy deleted: {policyName}. Operation Id: {result.Name}";
            }
            catch (Exception e)
            {
                Console.WriteLine($"CreateGameServerCluster error:");
                Console.WriteLine($"{e.Message}");
                throw;
            }
        }
    }
}

// [END cloud_game_servers_allocation_policy_delete]