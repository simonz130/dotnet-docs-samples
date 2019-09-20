﻿// Copyright (c) 2018 Google LLC.
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

// [START cloud_game_servers_deployment_commit_rollout]

using System;
using Google.Cloud.Gaming.V1Alpha;

namespace Gaming.Deployments
{
    class DeploymentCommitSamples
    {
        /// <summary>
        /// Commit a rollout for deployment
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="regionId">Region in which the cluster will be created</param>
        /// <param name="deploymentId">Deployment Id</param>
        public string CommitRollout(
            string projectId = "YOUR-PROJECT-ID",
            string regionId = "us-central1",
            string deploymentId = "YOUR-DEPLOYMENT-ID")
        {
            // Initialize the client
            var client = GameServerDeploymentsServiceClient.Create();

            // Construct the request
            string deploymentName = $"projects/{projectId}/locations/{regionId}/gameServerDeployments/{deploymentId}";

            // Call the API
            try
            {
                var result = client.CommitRollout(deploymentName);

                // Inspect the result
                return $"Committed rollout for deployment: {deploymentId}. Operation Id: {result.Name}";
            }
            catch (Exception e)
            {
                Console.WriteLine($"CommitRollout error:");
                Console.WriteLine($"{e.Message}");
                throw;
            }
        }
    }
}

// [END cloud_game_servers_deployment_commit_rollout]
