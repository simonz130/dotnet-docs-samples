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

// [START cloud_game_servers_realm_get]

using System;
using Google.Cloud.Gaming.V1Alpha;

namespace Gaming.Realms
{
    class GetRealmSamples
    {
        /// <summary>
        /// Gets a game server cluster
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="regionId">Region in which the cluster will be created</param>
        /// <param name="realmId"></param>
        public string GetRealm(
            string projectId = "YOUR-PROJECT-ID",
            string regionId = "us-central1",
            string realmId = "YOUR-REALM-ID")
        {
            // Initialize the client
            var client = RealmsServiceClient.Create();

            // Construct the request
            string parent = $"projects/{projectId}/locations/{regionId}";
            string realmName = $"{parent}/realms/{realmId}";

            // Call the API
            try
            {
                var realm = client.GetRealm(realmName);

                // Inspect the result
                return $"Realm returned: {realm.Name}";
            }
            catch (Exception e)
            {
                Console.WriteLine($"CreateRealm error:");
                Console.WriteLine($"{e.Message}");
                throw;
            }
        }
    }
}

// [END cloud_game_servers_realm_get]
