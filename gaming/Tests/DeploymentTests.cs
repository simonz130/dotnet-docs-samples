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

using System;
using Gaming.Deployments;
using GoogleCloudSamples;
using Xunit;

namespace Gaming.Tests
{
    public class DeploymentTestsFixture : IDisposable
    {
        private static string _projectId = Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ID");
        private const string _regionId = "us-central1";
        private const string _deploymentId = "test_deployment_";

        public DeploymentTestsFixture()
        {
            ProjectId = _projectId;
            Assert.False(string.IsNullOrEmpty(ProjectId));

            RegionId = _regionId;
            DeploymentId = _deploymentId + TestUtil.RandomName();

            string parent = $"projects/{ProjectId}/locations/global";
            DeploymentName = $"{parent}/gameServerDeployments/{DeploymentId}";

            // Setup
            var createDeploymentUtils = new CreateDeploymentSamples();
            Assert.Contains(
                $"Game server deployment created for {DeploymentId}.",
                createDeploymentUtils.CreateDeployment(ProjectId, DeploymentId));
        }

        public void Dispose()
        {
            try
            {
                var deleteDeploymentUtils = new DeploymentDeleteSamples();
                Assert.Contains(
                    $"Game server deployment {DeploymentId} deleted.",
                    deleteDeploymentUtils.DeleteDeployment(ProjectId, DeploymentId));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to delete Deployment {DeploymentId}");
                Console.WriteLine(e);
            }
        }

        public string ProjectId { get; private set; }
        public string RegionId { get; private set; }
        public string DeploymentId { get; private set; }
        public string DeploymentName { get; private set; }
    }

    public class DeploymentTests : IClassFixture<DeploymentTests>
    {
        private readonly DeploymentTestsFixture fixture;

        public DeploymentTests(DeploymentTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void TestCreateDeployment()
        {
            // Create API is implicitly used in text fixture setup
            Assert.NotNull(fixture.DeploymentId);
        }

        [Fact]
        public void TestGetDeployment()
        {
            var snippet = new DeploymentGetSamples();
            Assert.Contains(
                $"Game server deployment found: {fixture.DeploymentName}" ,
                snippet.GetDeployment(fixture.ProjectId, fixture.DeploymentId));
        }

        [Fact]
        public void TestGetDeploymentTarget()
        {
            var snippet = new DeploymentGetTargetSamples();
            Assert.Contains(
                $"Found target for {fixture.DeploymentName}",
                snippet.GetDeploymentTarget(fixture.ProjectId, fixture.DeploymentId));
        }

        [Fact]
        public void TestListDeployments()
        {
            var snippet = new DeploymentListSamples();
            Assert.Collection(
                snippet.ListGameDeployments(fixture.ProjectId),
                el => Assert.NotNull(el));
        }

        [Fact]
        public void TestSetRolloutTarget()
        {
            var snippet = new DeploymentSetRolloutTargetSamples();
            Assert.Contains(
                $"Rollout target set for {fixture.DeploymentId}.",
                snippet.SetRolloutTargetTarget(fixture.ProjectId, fixture.DeploymentId));
        }

        [Fact]
        public void TestStartRollout()
        {
            var snippet = new DeploymentSetRolloutTargetSamples();
            Assert.Contains(
                $"Deployment rollout started for {fixture.DeploymentId}.",
                snippet.SetRolloutTargetTarget(fixture.ProjectId, fixture.DeploymentId));
        }

        [Fact]
        public void TestUpdateDeployment()
        {
            var snippet = new UpdateDeploymentSamples();
            Assert.Contains(
                $"Game server deployment updated for deployment {fixture.DeploymentId}.",
                snippet.UpdateDeployment(fixture.ProjectId, fixture.DeploymentId));
        }

        [Fact]
        public void TestCommitRollout()
        {
            var snippet = new DeploymentCommitSamples();
            Assert.Contains(
                $"Committed rollout for deployment: {fixture.DeploymentId}.",
                snippet.CommitRollout(fixture.ProjectId, fixture.RegionId, fixture.DeploymentId));
        }
    }
}