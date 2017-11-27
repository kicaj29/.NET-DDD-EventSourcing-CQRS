using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cafe.Domain;
using Cafe.ReadModel;


namespace WebFrontend
{
    public static class Domain
    {
        public static MessageDispatcher Dispatcher;
        public static IOpenTabQueries OpenTabQueries;
        public static IChefTodoListQueries ChefTodoListQueries;

        public static void Setup()
        {
            Dispatcher = new MessageDispatcher(new InMemoryEventStore());
            
            Dispatcher.ScanInstance(new TabAggregate());

            OpenTabQueries = new OpenTabs();
            Dispatcher.ScanInstance(OpenTabQueries);

            ChefTodoListQueries = new ChefTodoList();
            Dispatcher.ScanInstance(ChefTodoListQueries);
        }
    }
}