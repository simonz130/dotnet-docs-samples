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

// [START cloud_game_servers_cluster_get]

using System;
using Google.Cloud.Gaming.V1Alpha;

namespace Gaming.Clusters
{
    class GetClusterSamples
    {
        /// <summary>
        /// Gets a game server cluster
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="regionId">Region in which the cluster will be created</param>
        /// <param name="realmId"></param>
        /// <param name="clusterId">The id of the game server cluster</param>
        /// <returns>Game server cluster name</returns>
        public string GetGameServerCluster(
            string projectId = "YOUR-PROJECT-ID",
            string regionId = "us-central1-f",
            string realmId = "YOUR-REALM-ID",
            string clusterId = "YOUR-GAME-SERVER-CLUSTER-ID")
        {
            // Initialize the client
            var client = GameServerClustersServiceClient.Create();

            // Construct the request
            string parent = $"projects/{projectId}/locations/{regionId}/realms/{realmId}";
            string clusterName = $"{parent}/gameServerClusters/{clusterId}";

            // Call the API
            var created = client.GetGameServerCluster(clusterName);

            // Inspect the result
            Console.WriteLine($"Game server cluster returned: {created.Name}");
            return created.Name;
        }
    }
}

// [END cloud_game_servers_cluster_get]
