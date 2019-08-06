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

// [START cloud_game_servers_realm_list]

using System;
using System.Collections.Generic;
using Google.Cloud.Gaming.V1Alpha;

namespace Gaming.Realms
{
    class ListRealmsSamples
    {
        /// <summary>
        /// List realms
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="regionId">Region in which the cluster will be created</param>
        /// <returns>List all realms for project and region</returns>
        public List<string> ListRealms(
            string projectId = "YOUR-PROJECT-ID",
            string regionId = "us-central1-f")
        {
            // Initialize the client
            var client = RealmsServiceClient.Create();

            // Construct the request
            string parent = $"projects/{projectId}/locations/{regionId}";

            // Call the API
            var response = client.ListRealms(parent);

            // Inspect the result
            List<string> result = new List<string>();
            foreach (var realm in response)
            {
                Console.WriteLine($"Realm returned: {realm.Name}");
                result.Add(realm.Name);
            }

            return result;
        }
    }
}

// [END cloud_game_servers_realm_list]
