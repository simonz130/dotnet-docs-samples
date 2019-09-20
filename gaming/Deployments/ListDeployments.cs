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
using Google.Api.Gax;
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
            try
            {
                var gameDeployments = client.ListGameServerDeployments(parent);

                // Inspect the result
                List<string> result = new List<string>();
                bool hasMore = true;
                Page<GameServerDeployment> currentPage;
                while (hasMore)
                {
                    currentPage = gameDeployments.ReadPage(pageSize: 10);

                    // Read the result in a given page
                    foreach (var gameDeployment in currentPage)
                    {
                        Console.WriteLine($"Game server deployment found: {gameDeployment.Name}");
                        result.Add(gameDeployment.Name);
                    }
                    hasMore = currentPage != null; 
                };

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ListGameServerDeployments error:");
                Console.WriteLine($"{e.Message}");
                throw;
            }
        }
    }
}

// [END cloud_game_servers_deployment_list]
