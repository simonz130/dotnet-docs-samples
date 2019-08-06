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

// [START cloud_game_servers_deployment_create]

using System;
using Google.Cloud.Gaming.V1Alpha;
using Newtonsoft.Json.Linq;

namespace Gaming.Deployments
{
    class CreateDeploymentSamples
    {
        /// <summary>
        /// Creates a new game deployment
        /// </summary>
        /// <param name="projectId">Your Google Cloud Project Id</param>
        /// <param name="deploymentId">Deployment Id</param>
        /// <returns>Game server deployment name</returns>
        public string CreateDeployment(
            string projectId = "YOUR-PROJECT-ID",
            string deploymentId = "YOUR-DEPLOYMENT-ID")
        {
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

            // Initialize the client
            var client = GameServerDeploymentsServiceClient.Create();

            // Construct the request
            string parent = $"projects/{projectId}/locations/global";
            string deploymentName = $"{parent}/gameServerDeployments/{deploymentId}";
            var gameServerTemplate = new GameServerTemplate
            {
                Spec = specObject.ToString(),
                TemplateId = "default"
            };
            var gameServerDeployment = new GameServerDeployment
            {
                Name = deploymentName,
                NewGameServerTemplate = gameServerTemplate,
            };
            var request = new CreateGameServerDeploymentRequest
            {
                Parent = parent,
                DeploymentId = deploymentId,
                GameServerDeployment = gameServerDeployment,
            };

            // Call the API
            var created = client.CreateGameServerDeployment(request);

            // Inspect the result
            Console.WriteLine($"Game server cluster created: {created.Name}");
            return created.Name;
        }
    }
}

// [END cloud_game_servers_deployment_create]
