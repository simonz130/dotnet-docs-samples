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
using Gaming.Clusters;
using Gaming.Realms;
using GoogleCloudSamples;
using Xunit;

class ClusterTestsFixture : IDisposable
{
    private static string _projectId = Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ID");
    private static string _gkeName = Environment.GetEnvironmentVariable("GKE_CLUSTER");

    private const string _realmId = "realm-1";
    private const string _clusterId = "123124";
    private const string _regionId = "us-central1-f";

    public ClusterTestsFixture()
    {
        ProjectId = _projectId;
        RegionId = _regionId;
        GKEName = _gkeName;

        ClusterId = _clusterId + TestUtil.RandomName();
        RealmId = _realmId + TestUtil.RandomName();
        RegionId = _regionId;

        string parent = $"projects/{_projectId}/locations/{_regionId}/realms/{_realmId}";
        string clusterName = $"{parent}/gameServerClusters/{_clusterId}";

        // Setup
        var createRealmUtils = new CreateRealmSamples();
        createRealmUtils.CreateRealm(_projectId, _regionId, _realmId);
        var createClusterUtils = new CreateClusterSamples();
        ClusterName = createClusterUtils.CreateGameServerCluster(ProjectId, RegionId, RealmId, ClusterId, GKEName);
        
        // Make sure created cluster name is right
        Assert.Equal(clusterName, ClusterName);
    }

    public void Dispose()
    {
        try
        {
            var deleteClusterUtils = new DeleteClusterSamples();
            deleteClusterUtils.DeleteGameServerCluster(ProjectId, RegionId, RealmId, ClusterId);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to delete Game Server {ClusterName}");
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
/// Delete test is in a separate class so we can optimize on test execution time:
/// Initialization fixture creates a cluster that can be shared between all the tests
/// that manipulate the cluster but not deleting it.
/// </summary>
public class DeleteClusterTests : IClassFixture<ClusterTestsFixture>
{
    private ClusterTestsFixture fixture;

    [Fact]
    public void TestDeleteCluster()
    {
        var snippet = new DeleteClusterSamples();
        Assert.Equal(fixture.ClusterName,
            snippet.DeleteGameServerCluster(
                fixture.ProjectId,
                fixture.RegionId,
                fixture.RealmId,
                fixture.ClusterId));
    }
}


/// <summary>
/// Tests for Game Cluster CRUD operations (except for deletion, which is in separate class
/// </summary>
public class ClusterTests : IClassFixture<ClusterTestsFixture>
{
    private ClusterTestsFixture fixture;

    private static string _projectId =
        Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ID");
    private const string _realmId = "realm-1";
    private const string _clusterId = "123124";
    private static string _parent = $"projects/{_projectId}/locations/us-central1-f/realms/{_realmId}";
    private static string _clusterName = $"{_parent}/gameServerClusters/{_clusterId}";

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
        Assert.Equal(fixture.ClusterName,
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
        Assert.Equal(fixture.ClusterName,
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