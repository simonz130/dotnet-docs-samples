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

using Gaming.ScalingPolicies;
using GoogleCloudSamples;
using System;
using Xunit;

namespace Gaming.Tests
{
    public class ScalingPoliciesTestsFixture : IDisposable
    {
        private static string _projectId = Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ID");
        private const string _regionId = "us-central1";
        private const string _policyId = "test-policy";
        private const string _deploymentId = "test-deployment";

        public ScalingPoliciesTestsFixture()
        {
            ProjectId = _projectId;
            Assert.False(string.IsNullOrEmpty(ProjectId));

            RegionId = _regionId;
            PolicyId = _policyId + TestUtil.RandomName();
            DeploymentId = _deploymentId + TestUtil.RandomName();
            string parent = $"projects/{ProjectId}/locations/global";
            PolicyName = $"{parent}/scalingPolicies/{PolicyId}";


            // Setup
            var createScalingPolicyUtils = new CreateScalingPolicySamples();
            var policyNameOperation = createScalingPolicyUtils.CreateScalingPolicy(ProjectId, PolicyId, DeploymentId);
            Assert.Contains($"Scaling policy created for {PolicyName}.", policyNameOperation);
        }

        public string ProjectId { get; private set; }
        public string RegionId { get; private set; }
        public string PolicyId { get; private set; }
        public string DeploymentId { get; private set; }

        public string PolicyName { get; private set; }

        public void Dispose()
        {
            try
            {
                var deletedScalingPolicyUtils = new DeleteScalingPolicySamples();
                Assert.Contains(
                    $"Scaling policy deleted: {PolicyName}.",
                    deletedScalingPolicyUtils.DeleteScalingPolicy(ProjectId, PolicyId));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to delete policy {PolicyId}");
                Console.WriteLine(e);
            }
        }
    }

    public class ScalingPoliciesTests : IClassFixture<ScalingPoliciesTests>
    {
        private readonly ScalingPoliciesTestsFixture fixture;

        public ScalingPoliciesTests(ScalingPoliciesTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void TestCreateScalingPolicies()
        {
            // Create API is implicitly used in test fixture setup
            Assert.NotNull(fixture.PolicyName);
        }

        [Fact]
        public void TestGetScalingPolicies()
        {
            var snippet = new GetScalingPolicySamples();
            Assert.Contains($"Scaling policy found: {fixture.PolicyName}",
                snippet.GetScalingPolicy(
                    fixture.ProjectId,
                    fixture.PolicyId));
        }

        [Fact]
        public void TestUpdateScalingPolicies()
        {
            var snippet = new UpdateScalingPoliciesSamples();
            Assert.Contains(
                $"Scaling policy updated: {fixture.PolicyName} updated.",
                snippet.UpdateScalingPolicy(
                    fixture.ProjectId,
                    fixture.PolicyId));
        }

        [Fact]
        public void TestListScalingPolicies()
        {
            var snippet = new ListScalingPoliciesSamples();
            Assert.Collection(
                snippet.ListScalingPolicies(fixture.ProjectId),
                el => Assert.NotNull(el));
        }
    }
}
