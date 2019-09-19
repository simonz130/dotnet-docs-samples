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

// [START cloud_game_servers_cluster_create]

using System;
using Google.Cloud.Gaming.V1Alpha;

namespace Gaming.Clusters
{
    class CreateClusterSamples
    {
        /// <summary>
        /// Initialize client that will be used to send requests.This client only needs to be created
        /// once, and can be reused for multiple requests. After completing all of your requests, call
        /// the "close" method on the client to safely clean up any remaining background resources.
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="regionId">Region in which the cluster will be created</param>
        /// <param name="realmId"></param>
        /// <param name="clusterId">The id of the game server cluster</param>
        /// <param name="gkeName">The name of Google Kubernetes Engine cluster</param>
        public string CreateGameServerCluster(
            string projectId = "YOUR-PROJECT-ID",
            string regionId = "us-central1",
            string realmId = "YOUR-REALM-ID",
            string clusterId = "YOUR-GAME-SERVER-CLUSTER-ID",
            string gkeName = "projects/YOUR-PROJECT-ID/locations/us-central1/clusters/test")
        {
            // Initialize the client
            var client = GameServerClustersServiceClient.Create();

            // Construct the request
            string parent = $"projects/{projectId}/locations/{regionId}/realms/{realmId}";
            string clusterName = $"{parent}/gameServerClusters/{clusterId}";
            var gameServerCluster = new GameServerCluster
            {
                Name = clusterName,
                ConnectionInfo = new GameServerClusterConnectionInfo
                {
                    GkeName = gkeName,
                    Namespace = "default"
                }
            };
            var request = new CreateGameServerClusterRequest
            {
                Parent = parent,
                GameServerClusterId = clusterId,
                GameServerCluster = gameServerCluster
            };

            // Call the API
            try
            {
                var created = client.CreateGameServerCluster(request);

                // Inspect the result
                return $"Game server cluster created: {created.Name}";
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

// [END cloud_game_servers_cluster_create]
