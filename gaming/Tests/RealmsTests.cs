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

using Gaming.Realms;
using GoogleCloudSamples;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Gaming.Tests
{
    public class RealmsTestsFixture : IDisposable
    {
        private static string _projectId = Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ID");
        private const string _realmId = "realm-test-1";
        private const string _regionId = "us-east1";
        public RealmsTestsFixture()
        {
            ProjectId = _projectId;
            Assert.False(string.IsNullOrEmpty(ProjectId));

            RegionId = _regionId;
            RealmId = _realmId + TestUtil.RandomName();
            RealmName = $"projects/{ProjectId}/locations/{RegionId}/realms/{RealmId}";

            // Setup (realm creation test)
            var createRealmUtils = new CreateRealmSamples();
            string createRealmOutput = createRealmUtils.CreateRealm(ProjectId, RegionId, RealmId);
            Assert.Contains($"Realm created for {RealmName}", createRealmOutput);
        }

        public string ProjectId { get; private set; }
        public string RegionId { get; private set; }
        public string RealmId { get; private set; }
        public string RealmName { get; private set; }

        public void Dispose()
        {
            try
            {
                var deletedRealmUtils = new DeleteRealmSamples();
                Assert.Contains(
                    $"Realm {RealmName} deleted.",
                     deletedRealmUtils.DeleteRealm(ProjectId, RegionId, RealmId));
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to delete Realm {RealmId}");
            }
        }
    }

    public class RealmsTests : IClassFixture<RealmsTestsFixture>
    {
        private readonly RealmsTestsFixture fixture;
        
        public RealmsTests(RealmsTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void TestCreateRealm()
        {
            // Create API is implicitly used in test fixture setup. We do light check here
            // for the purposes of visibility in test results reporting.
            Assert.NotNull(fixture.RealmName);
        }

        [Fact]
        public void TestGetRealm()
        {
            var snippet = new GetRealmSamples();
            Assert.Contains($"Realm returned: {fixture.RealmName}",
                snippet.GetRealm(
                    fixture.ProjectId,
                    fixture.RegionId, 
                    fixture.RealmId));
        }

        [Fact]
        public void TestUpdateRealm()
        {
            var snippet = new UpdateRealmsSamples();
            Assert.Contains(
                $"Realm {fixture.RealmName} updated.",
                snippet.UpdateRealm(
                fixture.ProjectId,
                fixture.RegionId,
                fixture.RealmId));
        }

        [Fact]
        public void TestListRealms()
        {
            var snippet = new ListRealmsSamples();
            Assert.Collection(
                snippet.ListRealms(
                    fixture.ProjectId,
                    fixture.RegionId),
                e => Assert.NotNull(e));
        }
    }
}
