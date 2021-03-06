﻿using IFramework.Command;
using IFramework.Infrastructure;
using IFramework.Message;
using IFramework.SingleSignOn;
using Sample.CommandService.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sample.ApiService.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        // GET: /Test/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mailbox()
        {
            var test = new CommandBusTests();
            test.CommandBusPressureTest();
            return View();
        }

        public ActionResult SignOut()
        {
            var signOutRequest = SingleSignOnContext<object>.SignOut("Account/SignOut");
            var signOutUrl = signOutRequest.WriteQueryString();
            return Redirect(signOutUrl);
        }

        [HttpGet]
        public Task<string> CommandDistributorStatus()
        {
            return Task.Factory.StartNew(() =>
            {
                //var commandDistributor = IoCFactory.Resolve<IMessageConsumer>("CommandDistributor");
                //var domainEventConsumer = IoCFactory.Resolve<IMessageConsumer>("DomainEventConsumer");
                //var distributorStatus = commandDistributor.GetStatus() +
                //    "event consumer:" + domainEventConsumer.GetStatus();
                //return distributorStatus;
                return "";
            });

        }
    }
}
