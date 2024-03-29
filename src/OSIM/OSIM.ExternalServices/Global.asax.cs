﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Ninject;
using Ninject.Extensions.Wcf;
using OSIM.Core.Services;
using OSIM.ExternalServices.Modules;
using OSIM.Persistence;

namespace OSIM.ExternalServices
{
    public class Global : NinjectWcfApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected override IKernel CreateKernel()
        {
            return new StandardKernel(new PersistenceModule(), new CoreServicesModule(), new ExternalServicesModule());
        }
    }
}