﻿using System.Collections.Generic;
using System.Linq;
using Xunit;
using log4net;
using log4net.Config;
using System.Threading;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using log4net.Repository.Hierarchy;
using log4net.Layout;
using log4net.Core;
using System.Threading.Tasks;
using System;
using log4net.Repository;
using AWS.Logger.TestUtils;

namespace AWS.Logger.Log4Net.Tests
{
    public class Log4NetTestSetup : BaseTestClass
    {
        public ILog Logger;

        public Log4NetTestSetup(TestFixture testFixture) : base(testFixture)
        {
        }

        public void GetLog4NetLogger(string fileName,string logName)
        {
            XmlConfigurator.Configure(new System.IO.FileInfo(fileName));
            Logger = LogManager.GetLogger(logName);
        }
    }
    public class Log4NetTestClass : Log4NetTestSetup
    {
        public Log4NetTestClass(TestFixture testFixture) : base(testFixture)
        {
        }

        #region Test Cases                                                        
        [Fact]
        public void Log4Net()
        {
            GetLog4NetLogger("log4net.config","Log4Net");
            SimpleLogging("AWSLog4NetGroupLog4Net");
        }

        [Fact]
        public void MultiThreadTest()
        {
            GetLog4NetLogger("log4net.config", "MultiThreadTest");
            MultiThreadTest("AWSLog4NetGroupLog4NetMultiThreadTest");
        }

        [Fact]
        public void MultiThreadBufferFullTest()
        {
            GetLog4NetLogger("log4net.config", "MultiThreadBufferFullTest");
            MultiThreadBufferFullTest("AWSLog4NetGroupMultiThreadBufferFullTest");
        }

        public override void Logging(int count)
        {
            for (int i = 0; i < count-1; i++)
            {
                Logger.Debug(string.Format("Test logging message {0} Log4Net, Thread Id:{1}", i, Thread.CurrentThread.ManagedThreadId));
            }
            Logger.Debug("LASTMESSAGE");
        }
        #endregion
    }
}