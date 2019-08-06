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

// [START cloud_game_servers_scaling_policy_update]

using System;
using Google.Cloud.Gaming.V1Alpha;
using Google.Protobuf.WellKnownTypes;

namespace Gaming.ScalingPolicies
{
    class UpdateScalingPoliciesSamples
    {
        /// <summary>
        /// Updates a scaling policy
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="policyId">Scaling policy to update</param>
        /// <returns>Updated scaling policy name</returns>
        public string UpdateScalingPolicy(
            string projectId = "YOUR-PROJECT-ID",
            string policyId = "YOUR-SCALING-POLICY-ID")
        {
            // Initialize the client
            var client = ScalingPoliciesServiceClient.Create();

            // Construct the request
            string policyName = $"projects/{projectId}/locations/global/scalingPolicies/{policyId}";
            var scalingPolicy = new ScalingPolicy
            {
                Name = policyName,
                Priority = 10,
            };
            var fieldMask = new FieldMask
            {
                Paths = { "priority" }
            };

            // Call the API
            var updated = client.UpdateScalingPolicy(scalingPolicy, fieldMask);

            // Inspect the result
            Console.WriteLine($"Scaling policy updated: {updated.Name}");
            return updated.Name;
        }
    }
}

// [END cloud_game_servers_scaling_policy_update]
