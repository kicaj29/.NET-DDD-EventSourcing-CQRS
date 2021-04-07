﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontDesk.SharedKernel.Interfaces;
using StructureMap;

namespace FrontDesk.SharedKernel
{
    /// <summary>
    /// http://msdn.microsoft.com/en-gb/magazine/ee236415.aspx#id0400046
    /// This name is misleading because it takes care also application events!
    /// </summary>
    public static class DomainEvents
    {
        [ThreadStatic]
        private static List<Delegate> actions;

        static DomainEvents()
        {
            Container = StructureMap.Container.For<BasicScanning>();
        }

        public static IContainer Container { get; set; }
        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
            {
                actions = new List<Delegate>();
            }
            actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            actions = null;
        }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            // Debug.WriteLine(Container.WhatDidIScan());
            // Debug.WriteLine(Container.WhatDoIHave());

            foreach (var handler in Container.GetAllInstances<IHandle<T>>())
            {
                handler.Handle(args);
            }

            if (actions != null)
            {
                foreach (var action in actions)
                {
                    if (action is Action<T>)
                    {
                        ((Action<T>)action)(args);
                    }
                }
            }
        }
    }
}
