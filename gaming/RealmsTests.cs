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
using System.Text;
using Xunit;

namespace Gaming
{
    class RealmsTestsFixture : IDisposable
    {
        private static string _projectId = Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ID");
        private const string _realmId = "realm-test-1";
        private const string _regionId = "us-central1-f";

        public RealmsTestsFixture()
        {
            ProjectId = _projectId;
            RegionId = _regionId;
            RealmId = _realmId + TestUtil.RandomName();

            // Setup (realm creation test)
            var createRealmUtils = new CreateRealmSamples();
            string createdRealm = createRealmUtils.CreateRealm(_projectId, _regionId, RealmId);
            Assert.Equal(RealmId, createdRealm);
        }

        public string ProjectId { get; set; }
        public string RegionId { get; set; }
        public string RealmId { get; set; }

        public void Dispose()
        {
            try
            {
                var deletedRealmUtils = new DeleteRealmSamples();
                deletedRealmUtils.DeleteRealm(ProjectId, RegionId, RealmId);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to delete Realm {RealmId}");
                Console.WriteLine(e);
            }
        }
    }

    public class RealmsTests : IClassFixture<RealmsTestsFixture>
    {
        private RealmsTestsFixture fixture;

        [Fact]
        public void TestCreateRealm()
        {
            // Create API is implicitly used in test fixture setup. We do light check here
            // for the purposes of visibility in test results reporting.
            Assert.NotNull(fixture.RealmId);
        }

        [Fact]
        public void TestGetRealm()
        {
            var snippet = new GetRealmSamples();
            Assert.Equal(fixture.RealmId,
                snippet.GetRealm(
                    fixture.ProjectId,
                    fixture.RegionId, 
                    fixture.RealmId));
        }

        [Fact]
        public void TestUpdateRealm()
        {
            var snippet = new UpdateRealmsSamples();
            Assert.Equal(fixture.RealmId,
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
                el => Assert.NotNull(el));
        }
    }
}
