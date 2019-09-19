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

using Gaming.Clusters;
using Gaming.Realms;
using GoogleCloudSamples;
using System;
using Xunit;

namespace Gaming.Tests
{
    public class ClusterTestsFixture : IDisposable
    {
        private static string _projectId = Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ID");
        private static string _gkeName = Environment.GetEnvironmentVariable("GKE_CLUSTER");

        private const string _realmId = "realm-1";
        private const string _clusterId = "gke-cluster";
        private const string _regionId = "us-west2";

        public ClusterTestsFixture()
        {
            ProjectId = _projectId;
            RegionId = _regionId;
            GKEName = _gkeName;

            Assert.False(string.IsNullOrEmpty(GKEName));
            Assert.False(string.IsNullOrEmpty(ProjectId));

            ClusterId = _clusterId + TestUtil.RandomName();
            RealmId = _realmId + TestUtil.RandomName();
            RegionId = _regionId;

            string parent = $"projects/{_projectId}/locations/{RegionId}/realms/{RealmId}";
            ClusterName = $"{parent}/gameServerClusters/{ClusterId}";

            // Setup
            var createRealmUtils = new CreateRealmSamples();
            createRealmUtils.CreateRealm(_projectId, _regionId, RealmId);

            var createClusterUtils = new CreateClusterSamples();
            var clusterCreateOutput = createClusterUtils.CreateGameServerCluster(ProjectId, RegionId, RealmId, ClusterId, GKEName);
            Assert.Contains($"Game server cluster created: {ClusterName}", clusterCreateOutput);
        }

        public void Dispose()
        {
            try
            {
                // This serves as delete cluster test
                var deleteClusterUtils = new DeleteClusterSamples();
                Assert.Contains(
                    $"Game server cluster deleted: {ClusterName}",
                    deleteClusterUtils.DeleteGameServerCluster(ProjectId, RegionId, RealmId, ClusterId));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to delete Game Server {ClusterName}");
                Console.WriteLine(e);
            }

            try
            {
                // Delete realm used for tests
                var deletedRealmUtils = new DeleteRealmSamples();
                deletedRealmUtils.DeleteRealm(ProjectId, RegionId, RealmId);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to delete Realm {RealmId}");
                Console.WriteLine(e);
            }
        }

        public string ClusterName { get; private set; }

        public string ClusterId { get; private set; }

        public string ProjectId { get; private set; }

        public string GKEName { get; private set; }

        public string RealmId { get; private set; }

        public string RegionId { get; private set; }
    }

    /// <summary>
    /// Tests for Game Cluster CRUD operations (except for deletion, which is in separate class
    /// </summary>
    public class ClusterTests : IClassFixture<ClusterTestsFixture>
    {
        private readonly ClusterTestsFixture fixture;

        public ClusterTests(ClusterTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void TestCreateCluster()
        {
            // Create API is implicitly used in test fixture setup
            Assert.NotNull(fixture.ClusterName);
        }

        [Fact]
        public void TestGetCluster()
        {
            var snippet = new GetClusterSamples();
            Assert.Contains($"Game server cluster returned: {fixture.ClusterName}",
                snippet.GetGameServerCluster(
                    fixture.ProjectId, 
                    fixture.RegionId, 
                    fixture.RealmId, 
                    fixture.ClusterId));
        }

        [Fact]
        public void TestUpdateCluster()
        {
            var snippet = new UpdateClusterSamples();
            Assert.Contains($"Game server cluster updated: {fixture.ClusterName}.",
                snippet.UpdateGameServerCluster(
                    fixture.ProjectId,
                    fixture.RegionId,
                    fixture.RealmId,
                    fixture.ClusterId));
        }

        [Fact]
        public void TestListClusters()
        {
            var snippet = new ListClusterSamples();
            Assert.Collection(
                snippet.ListGameServerClusters(
                    fixture.ProjectId,
                    fixture.RegionId,
                    fixture.RealmId,
                    fixture.ClusterId),
                el => Assert.NotNull(el));
        }
    }
}