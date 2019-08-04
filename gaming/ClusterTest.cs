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
using Xunit;

public class ClusterTest
{
    private static string _projectId =
        Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ID");
    private const string _realmId = "123";
    private const string _clusterId = "123124";
    private static string _parent = $"projects/{_projectId}/locations/us-central1-f/realms/{_realmId}";
    private static string _clusterName = $"{_parent}/gameServerClusters/{_clusterId}";


    [Fact]
    public void TestCreateCluster()
    {
        var snippet = new CreateClusterSamples();
        Assert.Equal(_clusterName,
            snippet.CreateGameServerCluster(_projectId));
    }

    [Fact]
    public void TestDeleteCluster()
    {
        var snippet = new DeleteClusterSamples();
        Assert.Equal(_clusterName,
            snippet.DeleteGameServerCluster(_projectId));
    }

    [Fact]
    public void TestGetCluster()
    {
        var snippet = new GetClusterSamples();
        Assert.Equal(_clusterName,
            snippet.GetGameServerCluster(_projectId));
    }

    [Fact]
    public void TestUpdateCluster()
    {
        var snippet = new UpdateClusterSamples();
        Assert.Equal(_clusterName,
            snippet.UpdateGameServerCluster(_projectId));
    }

    [Fact]
    public void TestListClusters()
    {
        var snippet = new ListClusterSamples();
        Assert.Collection(
            snippet.ListGameServerClusters(_projectId),
            el => Assert.NotNull(el));
    }
}