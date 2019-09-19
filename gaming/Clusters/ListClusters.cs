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

// [START cloud_game_servers_cluster_list]

using System;
using System.Collections.Generic;
using Google.Api.Gax;
using Google.Cloud.Gaming.V1Alpha;

namespace Gaming.Clusters
{
    class ListClusterSamples
    {
        /// <summary>
        /// List game server clusters
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="regionId">Region in which the cluster will be created</param>
        /// <param name="realmId"></param>
        public List<string> ListGameServerClusters(
            string projectId = "YOUR-PROJECT-ID",
            string regionId = "us-central1-f",
            string realmId = "YOUR-REALM-ID",
            string clusterId = "YOUR-GAME-SERVER-CLUSTER-ID")
        {
            // Initialize the client
            var client = GameServerClustersServiceClient.Create();

            // Construct the request
            string parent = $"projects/{projectId}/locations/{regionId}/realms/{realmId}";

            // Call the API
            try
            {
                var response = client.ListGameServerClusters(parent);

                // Inspect the result
                List<string> result = new List<string>();
                bool hasMore = true;
                Page<GameServerCluster> currentPage;
                while (hasMore)
                {
                    currentPage = response.ReadPage(pageSize: 10);

                    // Read the result in a given page
                    foreach (var cluster in currentPage)
                    {
                        Console.WriteLine($"Game server cluster returned: {cluster.Name}");
                        result.Add(cluster.Name);
                    }
                    hasMore = currentPage != null; 
                };

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ListGameServerClusters error:");
                Console.WriteLine($"{e.Message}");
                throw;
            }
        }
    }
}

// [END cloud_game_servers_cluster_list]
