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

// [START cloud_game_servers_deployment_list]

using System;
using System.Collections.Generic;
using Google.Cloud.Gaming.V1Alpha;

namespace Gaming.Deployments
{
    class DeploymentListSamples
    {
        /// <summary>
        /// Gets game deployment
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="deploymentId">Deployment Id</param>
        /// <returns>Deployment name</returns>
        public List<string> ListGameDeployments(
            string projectId = "YOUR-PROJECT-ID")
        {
            // Initialize the client
            var client = GameServerDeploymentsServiceClient.Create();

            // Construct the request
            string parent = $"projects/{projectId}/locations/global";

            // Call the API
            var gameDeployments = client.ListGameServerDeployments(parent);

            // Inspect the result
            var res = new List<string>();
            foreach (var gameDeployment in gameDeployments)
            {
                Console.WriteLine($"Game server deployment found: {gameDeployment.Name}");
                res.Add(gameDeployment.Name);
            }

            return res;
        }
    }
}

// [END cloud_game_servers_deployment_list]
