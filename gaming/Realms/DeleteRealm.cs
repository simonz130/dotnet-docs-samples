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

// [START cloud_game_servers_realm_delete]

using System;
using Google.Cloud.Gaming.V1Alpha;

namespace Gaming.Realms
{
    class DeleteRealmSamples
    {
        /// <summary>
        /// Deletes a realm
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="regionId">Region in which the cluster will be created</param>
        /// <param name="realmId"></param>
        /// <returns>Deleted Realm name</returns>
        public string DeleteRealm(
            string projectId = "YOUR-PROJECT-ID",
            string regionId = "us-central1-f",
            string realmId = "YOUR-REALM-ID")
        {
            // Initialize the client
            var client = RealmsServiceClient.Create();

            // Construct the request
            string parent = $"projects/{projectId}/locations/{regionId}/realms/{realmId}";
            string realmName = $"{parent}/realms/{realmId}";

            // Call the API
            var deleted = client.DeleteRealm(realmName);

            // Inspect the result
            Console.WriteLine($"Realm deleted: {deleted.Name}");
            return deleted.Name;
        }
    }
}

// [END cloud_game_servers_realm_delete]
