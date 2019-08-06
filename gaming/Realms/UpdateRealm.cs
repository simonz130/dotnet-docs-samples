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

// [START cloud_game_servers_realm_update]

using System;
using Google.Cloud.Gaming.V1Alpha;
using Google.Protobuf.WellKnownTypes;

namespace Gaming.Realms
{
    class UpdateRealmsSamples
    {
        /// <summary>
        /// Updates a realm
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="regionId">Region in which the cluster will be created</param>
        /// <param name="realmId"></param>
        /// <returns>Update a realm</returns>
        public string UpdateRealm(
            string projectId = "YOUR-PROJECT-ID",
            string regionId = "us-central1-f",
            string realmId = "YOUR-REALM-ID")
        {
            // Initialize the client
            var client = RealmsServiceClient.Create();

            // Construct the request
            string realmName = $"projects/{projectId}/locations/{regionId}/realms/{realmId}";
            var realm = new Realm
            {
                Name = realmName,
                TimeZone = "America/New_York"
            };
            var fieldMask = new FieldMask
            {
                Paths = { "time_zone" }
            };
            
            // Call the API
            var updated = client.UpdateRealm(realm, fieldMask);

            // Inspect the result
            Console.WriteLine($"Realm updated: {updated.Name}");
            return updated.Name;
        }
    }
}

// [END cloud_game_servers_realm_update]
