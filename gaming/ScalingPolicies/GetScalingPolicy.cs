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

// [START cloud_game_servers_scaling_policy_get]

using System;
using Google.Cloud.Gaming.V1Alpha;

namespace Gaming.ScalingPolicies
{
    class GetScalingPolicySamples
    {
        /// <summary>
        /// Gets a scaling policy
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="policyId">Id of the scaling policy</param>
        /// <returns>Gets scaling policy name</returns>
        public string GetScalingPolicy(
            string projectId = "YOUR-PROJECT-ID",
            string policyId = "YOUR-SCALING-POLICY-ID")
        {
            // Initialize the client
            var client = ScalingPoliciesServiceClient.Create();

            // Construct the request
            string parent = $"projects/{projectId}/locations/global";
            string policyName = $"{parent}/scalingPolicies/{policyId}";

            // Call the API
            var scalingPolicy = client.GetScalingPolicy(policyName);

            // Inspect the result
            Console.WriteLine($"Scaling policy found: {scalingPolicy.Name}");
            return scalingPolicy.Name;
        }
    }
}

// [END cloud_game_servers_scaling_policy_get]
