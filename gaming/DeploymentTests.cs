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
        private const string _regionId = "us-central1-f";
        private const string _deploymentId = "test_deploymentId";

        public DeploymentTestsFixture()
        {
            ProjectId = _projectId;
            RegionId = _regionId;
            DeploymentId = _deploymentId + TestUtil.RandomName();

            // Setup
            var createDeploymentUtils = new CreateDeploymentSamples();
            DeploymentName = createDeploymentUtils.CreateDeployment(ProjectId, DeploymentId);

            // Make sure deployment was created
            Assert.NotNull(DeploymentName);
        }

        public void Dispose()
        {
            try
            {
                var deleteDeploymentUtils = new DeploymentDeleteSamples();
                deleteDeploymentUtils.DeleteDeployment(ProjectId, DeploymentId);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to delete Deployment {DeploymentName}");
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
        private DeploymentTestsFixture fixture;

        public DeploymentTests(DeploymentTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void TestCreateDeployment()
        {
            // Create API is implicitly used in text fixture setup
            Assert.NotNull(fixture.DeploymentName);
        }

        [Fact]
        public void TestGetDeployment()
        {
            var snippet = new DeploymentGetSamples();
            Assert.Equal(fixture.DeploymentName,
                snippet.GetDeployment(fixture.ProjectId, fixture.DeploymentId));
        }

        [Fact]
        public void TestGetDeploymentTarget()
        {
            var snippet = new DeploymentGetTargetSamples();
            Assert.True(snippet.GetDeploymentTarget(fixture.ProjectId, fixture.DeploymentId) > 0);
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
            Assert.NotNull(
                snippet.SetRolloutTargetTarget(fixture.ProjectId, fixture.DeploymentId));
        }

        [Fact]
        public void TestStartRollout()
        {
            var snippet = new DeploymentSetRolloutTargetSamples();
            Assert.NotNull(
                snippet.SetRolloutTargetTarget(fixture.ProjectId, fixture.DeploymentId));
        }

        [Fact]
        public void TestUpdateDeployment()
        {
            var snippet = new UpdateDeploymentSamples();
            Assert.NotNull(
                snippet.UpdateDeployment(fixture.ProjectId, fixture.DeploymentId));
        }

        [Fact]
        public void TestCommitRollout()
        {
            var snippet = new DeploymentCommitSamples();
            Assert.NotNull(
                snippet.CommitRollout(fixture.ProjectId, fixture.RegionId, fixture.DeploymentId));
        }
    }
}