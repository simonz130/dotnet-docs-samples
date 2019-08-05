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

// [START cloud_game_servers_deployment_start_rollout]

using System;
using Google.Cloud.Gaming.V1Alpha;
using Newtonsoft.Json.Linq;

namespace Gaming.Deployments
{
    class DeploymentStartRolloutSamples
    {
        /// <summary>
        /// Starts a rollout
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="regionId">Region in which the cluster will be created</param>
        /// <param name="deploymentId">Deployment Id</param>
        /// <returns>Rollout name</returns>
        public string StartRollout(
            string projectId = "YOUR-PROJECT-ID",
            string regionId = "us-central1-f",
            string deploymentId = "YOUR-DEPLOYMENT-ID",
            string templateId = "YOUR-GAME-SERVER-TEMPLATE-ID")
        {
            // Initialize the client
            var client = GameServerDeploymentsServiceClient.Create();

            // Construct the request
            string deploymentName = $"projects/{projectId}/locations/{regionId}/gameServerDeployments/{deploymentId}";

            // Build a spec as shown at https://agones.dev/site/docs/reference/gameserver/
            var container = new JObject();
            container.Add("name", "default");
            container.Add("image", "gcr.io/agones-images/default:1.0");

            var containers = new JArray();
            containers.Add(container);

            var spec = new JObject();
            spec.Add("containers", containers);

            var template = new JObject();
            template.Add("spec", spec);

            var port = new JObject();
            port.Add("name", "default");

            var ports = new JArray();
            ports.Add(port);

            var specObject = new JObject();
            specObject.Add("ports", ports);
            specObject.Add("template", template);

            var gameServerTemplate = new GameServerTemplate
            {
                TemplateId = templateId,
                Spec = specObject.ToString(),
            };

            var startRolloutRequest = new StartRolloutRequest
            {
                Name = deploymentName,
                NewGameServerTemplate = gameServerTemplate
            };

            // Call the API
            var result = client.StartRollout(startRolloutRequest);

            // Inspect the result
            Console.WriteLine($"Game server cluster created: {result.Name}");
            return result.Name;
        }
    }
}

// [END cloud_game_servers_deployment_start_rollout]
