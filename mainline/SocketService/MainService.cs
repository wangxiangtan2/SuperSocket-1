using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using SuperSocket.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using SuperSocket.SocketEngine.Configuration;

namespace SuperSocket.SocketService
{
    partial class MainService : ServiceBase
    {
        private IBootstrap m_Bootstrap;

        public MainService()
        {
            InitializeComponent();
            m_Bootstrap = new DefaultBootstrap();
        }

        protected override void OnStart(string[] args)
        {
            var serverConfig = ConfigurationManager.GetSection("socketServer") as SocketServiceConfig;
            if (!m_Bootstrap.Initialize(serverConfig))
                return;

            m_Bootstrap.Start();
        }

        protected override void OnStop()
        {
            m_Bootstrap.Stop();
            base.OnStop();
        }

        protected override void OnShutdown()
        {
            m_Bootstrap.Stop();
            base.OnShutdown();
        }
    }
}
