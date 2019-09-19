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

// [START cloud_game_servers_scaling_policy_create]

using System;
using Google.Cloud.Gaming.V1Alpha;

namespace Gaming.ScalingPolicies
{
    class CreateScalingPolicySamples
    {
        /// <summary>
        /// Create a new scaling policy
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="policyId">Id of the scaling policy</param>
        /// <param name="deploymentId">Id of the deployment to scale</param>
        public string CreateScalingPolicy(
            string projectId = "YOUR-PROJECT-ID",
            string policyId = "YOUR-SCALING-POLICY-ID",
            string deploymentId = "YOUR-DEPLOYMENT-ID")
        {
            // Initialize the client
            var client = ScalingPoliciesServiceClient.Create();

            // Construct the request
            string parent = $"projects/{projectId}/locations/global";
            string policyName = $"{parent}/scalingPolicies/{policyId}";
            string deploymentName = $"{parent}/gameServerDeployments/{deploymentId}";

            var autoscalerSettings = new FleetAutoscalerSettings
            {
                BufferSizeAbsolute = 1,
                MinReplicas = 1,
                MaxReplicas = 2
            };
            var scalingPolicy = new ScalingPolicy
            {
                Name = policyName,
                Priority = 1,
                FleetAutoscalerSettings = autoscalerSettings,
                GameServerDeployment = deploymentName
            };
            var scalingPolicyRequest = new CreateScalingPolicyRequest
            {
                Parent = parent,
                ScalingPolicyId = policyId,
                ScalingPolicy = scalingPolicy
            };

            // Call the API
            try
            {
                var created = client.CreateScalingPolicy(scalingPolicyRequest);

                // Inspect the result
                return $"Scaling policy created for {policyName}. Operation Id {created.Name}";
            }
            catch (Exception e)
            {
                Console.WriteLine($"CreateScalingPolicy error:");
                Console.WriteLine($"{e.Message}");
                throw;
            }
        }
    }
}

// [END cloud_game_servers_scaling_policy_create]
