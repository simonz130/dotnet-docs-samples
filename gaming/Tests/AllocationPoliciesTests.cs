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
using Gaming.AllocationPolicies;
using GoogleCloudSamples;
using Xunit;

namespace Gaming.Tests
{
    public class AllocationPoliciesTestsFixture : IDisposable
    {
        private static string _projectId = Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ID");
        private const string _policyId = "policy-test";

        public AllocationPoliciesTestsFixture()
        {
            ProjectId = _projectId;

            Assert.False(string.IsNullOrEmpty(ProjectId));

            PolicyId = _policyId + TestUtil.RandomName();
            string parent = $"projects/{ProjectId}/locations/global";
            PolicyName = $"{parent}/allocationPolicies/{PolicyId}";

            // Setup
            var snippet = new CreateAllocationPolicySamples();
            Assert.Contains($"Allocation Policy created for {PolicyName}.", snippet.CreateAllocationPolicy(ProjectId, PolicyId));
        }

        public void Dispose()
        {
            try
            {
                // This serves as delete allocation policy test
                var deleteAllocationPolicyUtils = new DeleteAllocationPolicySamples();
                Assert.Contains(
                     $"Allocation Policy deleted: {PolicyName}.",
                    deleteAllocationPolicyUtils.DeleteAllocationPolicy(ProjectId, PolicyId));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to delete allocation policy {PolicyName}");
                Console.WriteLine(e);
            }
        }

        public string PolicyName { get; private set; }

        public string ProjectId { get; private set; }

        public string PolicyId { get; private set; }
    }

    public class AllocationPoliciesTest : IClassFixture<AllocationPoliciesTestsFixture>
    {
        private readonly AllocationPoliciesTestsFixture fixture;

        private static string _projectId = Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ID");
        private const string _policyId = "12345";
        private static string _policyName = 
            $"projects/{_projectId}/locations/global/allocationPolicies/{_policyId}";

        public AllocationPoliciesTest(AllocationPoliciesTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void TestCreateAllocationPolicy()
        {
            var snippet = new CreateAllocationPolicySamples();
            Assert.Equal(_policyName,
                snippet.CreateAllocationPolicy(_projectId, _policyId));
        }

        [Fact]
        public void TestGetAllocationPolicy()
        {
            var snippet = new GetAllocationPolicySamples();
            Assert.Equal(_policyName,
                snippet.GetAllocationPolicy(_projectId, _policyId));
        }

        [Fact]
        public void TestUpdateAllocationPolicy()
        {
            var snippet = new UpdateAllocationPolicySamples();
            Assert.Equal(_policyName,
                snippet.UpdateAllocationPolicy(_projectId, _policyId));
        }

        [Fact]
        public void TestListAllocationPolicy()
        {
            var snippet = new ListAllocationPolicySamples();
            Assert.Collection(
                snippet.ListAllocationPolicy(_projectId, _policyId),
                el => Assert.NotNull(el));
        }
    }
}