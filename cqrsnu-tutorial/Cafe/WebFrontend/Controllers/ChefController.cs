﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Cafe.Domain.Events;
using WebFrontend.ActionFilters;

namespace WebFrontend.Controllers
{
    [IncludeLayoutData]
    public class ChefController : Controller
    {
        public ActionResult Index()
        {
            return View(Domain.ChefTodoListQueries.GetTodoList());
        }

        public ActionResult MarkPrepared(Guid id, FormCollection form)
        {
            Domain.Dispatcher.SendCommand(new MarkFoodPrepared
            {
                Id = id,
                MenuNumbers = (from entry in form.Keys.Cast<string>()
                               where form[entry] != "false"
                               let m = Regex.Match(entry, @"prepared_\d+_(\d+)")
                               where m.Success
                               select int.Parse(m.Groups[1].Value)
                              ).ToList()
            });

            return RedirectToAction("Index");
        }
    }
}
