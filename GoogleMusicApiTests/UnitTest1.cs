﻿using System;
using System.Diagnostics;
using GoogleMusicApi;
using GoogleMusicApi.Requests;
using GoogleMusicApi.Structure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace GoogleMusicApiTests
{
    [TestClass]
    
    public class UnitTest1
    {
        public Session Session { get; set; }

        [TestMethod]
        public void TestLogin()
        {
            Session = new MobileSession();
            Session.Login("dev.lvelden@gmail.com", "197355p!");
            Assert.IsTrue(Session.IsAuthenticated);
        }

        [TestMethod]
        public void TestGetPromotedTracks()
        {
            TestLogin();
            Assert.IsTrue(Session.IsAuthenticated);

            var thumbsUpRequest = new ListPromotedTracks().Get(new ResultListRequest(Session));
            Assert.IsNotNull(thumbsUpRequest);
        }


        [TestMethod]
        public void TestConfig()
        {
            TestLogin();
            Assert.IsTrue(Session.IsAuthenticated);

            var configRequest = new ConfigRequest().Get(new GetRequest(Session));
            Assert.IsNotNull(configRequest);
        }

        [TestMethod]
        public void TestSearch()
        {
            TestLogin();
            Assert.IsTrue(Session.IsAuthenticated);

            var searchRequest = new SearchRequest("eminem").Get(new GetRequest(Session));
            Assert.IsNotNull(searchRequest);
        }

        [TestMethod]
        public void TestTrackFeed()
        {
            TestLogin();
            Assert.IsTrue(Session.IsAuthenticated);

            var trackFeedRequest = new ListTrackFeed().Get(new ResultListRequest(Session));
            Assert.IsNotNull(trackFeedRequest);
        }

        [TestMethod]
        public void TestPlaylistList()
        {
            TestLogin();
            Assert.IsTrue(Session.IsAuthenticated);

            var feedRequest = new ListPlaylists().Get(new ResultListRequest(Session));
            Assert.IsNotNull(feedRequest);
        }

        [TestMethod]
        public void TestListListenNowTracks()
        {
            TestLogin();
            Assert.IsTrue(Session.IsAuthenticated);

            var feedRequest = new ListListenNowTracks().Get(new GetRequest(Session));
            Assert.IsNotNull(feedRequest);
        }

    }
}