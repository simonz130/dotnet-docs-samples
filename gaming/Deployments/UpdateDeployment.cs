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

// [START cloud_game_servers_deployment_update]

using System;
using Google.Cloud.Gaming.V1Alpha;
using Google.Protobuf.WellKnownTypes;

namespace Gaming.Deployments
{
    class UpdateDeploymentSamples
    {
        /// <summary>
        /// Updates game deployment
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="deploymentId">Deployment Id</param>
        /// <returns>Game server deployment name</returns>
        public string UpdateDeployment(
            string projectId = "YOUR-PROJECT-ID",
            string deploymentId = "YOUR-DEPLOYMENT-ID")
        {
            string parent = $"projects/{projectId}/locations/global";
            string deploymentName = $"{parent}/gameServerDeployments/{deploymentId}";

            // Construct the request
            var deployment = new GameServerDeployment
            {
                Name = deploymentName,
                Labels = { { "key", "value" } }
            };
            var fieldMask = new FieldMask { Paths = { "labels" } };

            // Initialize the client
            var client = GameServerDeploymentsServiceClient.Create();

            // Call the API
            try
            {
                var updated = client.UpdateGameServerDeployment(deployment, fieldMask);

                // Inspect the result
                return $"Game server deployment updated for deployment {deploymentId}. Operation Id: {updated.Name}";
            }
            catch (Exception e)
            {
                Console.WriteLine($"UpdateGameServerDeployment error:");
                Console.WriteLine($"{e.Message}");
                throw;
            }
        }
    }
}

// [END cloud_game_servers_deployment_update]
