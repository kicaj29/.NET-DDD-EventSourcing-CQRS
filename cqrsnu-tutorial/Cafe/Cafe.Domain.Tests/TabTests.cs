using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Domain.Commands;
using Cafe.Domain.Events;
using Cafe.Domain.Exceptions;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Cafe.Domain.Tests
{
    [TestFixture]
    public class TabTests : BDDTest<TabAggregate>
    {
        private Guid testId;
        private int testTable;
        private string testWaiter;

        private OrderedItem testDrink1;
        private OrderedItem testDrink2;

        private OrderedItem testFood1;
        private OrderedItem testFood2;

        [SetUp]
        public void Setup()
        {
            testId = Guid.NewGuid();
            testTable = 42;
            testWaiter = "Derek";

            testDrink1 = new OrderedItem
            {
                MenuNumber = 4,
                Description = "Sprite",
                Price = 1.50M,
                IsDrink = true
            };

            testDrink2 = new OrderedItem
            {
                MenuNumber = 10,
                Description = "Beer",
                Price = 2.50M,
                IsDrink = true
            };

            testFood1 = new OrderedItem
            {
                MenuNumber = 16,
                Description = "Beef Noodles",
                Price = 7.50M,
                IsDrink = false
            };
            testFood2 = new OrderedItem
            {
                MenuNumber = 25,
                Description = "Vegetable Curry",
                Price = 6.00M,
                IsDrink = false
            };
        }

        [Test]
        public void GivenNoEventHistory_WhenOpenTabIssued_ThenTabOpenedProduced()
        {
            Test(
                Given(),
                When(new OpenTab
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                }),
                Then(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                })
                );
        }

        [Test]
        public void GivenNoTabOpened_WhenPlaceOrderIssued_ThenFailWithTabNotOpenException()
        {
            Test(
                Given(),
                When (new PlaceOrder
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testDrink1 },
                }),
                ThenFailWith<TabNotOpen>()
                );
        }

        [Test]
        public void GivenTabOpened_WhenPlaceOrderDrinksIssued_ThenDrinksOrderedProduced()
        {
            Test(
                Given(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = this.testWaiter
                }),
                When(new PlaceOrder
                {
                    Id = testId,
                    Items = new List<OrderedItem> { this.testDrink1, this.testDrink2 }
                }),
                Then(new DrinksOrdered
                {
                    Id = this.testId,
                    Items = new List<OrderedItem> { this.testDrink1, this.testDrink2 }
                })
                );
        }

        [Test]
        public void GivenTabOpened_WhenPlaceOrderFoodIssued_ThenFoodOrderedProduced()
        {
            Test(
                Given(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = this.testWaiter
                }),
                When(new PlaceOrder
                {
                    Id = testId,
                    Items = new List<OrderedItem> { this.testFood1, this.testFood2 }
                }),
                Then(new FoodOrdered
                {
                    Id = this.testId,
                    Items = new List<OrderedItem> { this.testFood1, this.testFood2 }
                })
            );
        }

        [Test]
        public void GivenTabOpened_WhenPlaceOrderDrinksAndFoodIssued_ThenDrinksOrderedAndFoodOrderedProduced()
        {
            Test(
                Given(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = this.testWaiter
                }),
                When(new PlaceOrder
                {
                    Id = testId,
                    Items = new List<OrderedItem> { this.testFood1, this.testDrink2 }
                }),
                Then(new DrinksOrdered
                {
                    Id = this.testId,
                    Items = new List<OrderedItem> { this.testDrink2 }
                },
                new FoodOrdered
                {
                    Id = this.testId,
                    Items = new List<OrderedItem> { this.testFood1 }
                }
                )
            );
        }

        [Test]
        public void GivenTabOpenedDrinksOrdered_WhenMarkDrinksToServeIssued_ThenDrinksServed()
        {
            Test(
                Given(
                    new TabOpened
                    {
                        Id = this.testId,
                        TableNumber = this.testTable,
                        Waiter = this.testWaiter,
                    },
                    new DrinksOrdered
                    {
                        Id = this.testId,
                        Items = new List<OrderedItem> { testDrink1, testDrink2 }
                    }
                    ),
                When(
                    new MarkDrinksToServe
                    {
                        Id = this.testId,
                        MenuNumbers = new List<int> { this.testDrink1.MenuNumber, this.testDrink2.MenuNumber }
                    }
                    ),
                Then(
                    new DrinksServed
                    {
                        Id = this.testId,
                        MenuNumbers = new List<int> { this.testDrink1.MenuNumber, this.testDrink2.MenuNumber }
                    }
                    )
                );
        }

        [Test]
        public void GivenTabOpenedDrinksOrdered_WhenMarkDrinksToServeNotContainsDrink2Issued_ThenFailDrinksNotOutstandingException()
        {
            //we can not mark our second test drink as served if the order was for our first test drink
            Test(
                    Given(
                        new TabOpened
                        {
                            Id = this.testId,
                            TableNumber = this.testTable,
                            Waiter = this.testWaiter
                        },
                        new DrinksOrdered
                        {
                            Id = this.testId,
                            Items = new List<OrderedItem> { this.testDrink1 }
                        }
                        ),
                    When(
                        new MarkDrinksToServe
                        {
                            Id = this.testId,
                            MenuNumbers = new List<int> { this.testDrink2.MenuNumber }
                        }
                        ),
                    ThenFailWith<DrinksNotOutstanding>()
                );
        }

        [Test]
        public void GivenTabOpenedDrinksOrderedDrinksServed_WhenMarkDrinksToServeIssued_ThenFailDrinksNotOutstanding()
        {
            //CanNotServeAnOrderedDrinkTwice
            Test(
                Given(
                        new TabOpened
                        {
                            Id = this.testId,
                            TableNumber = this.testTable,
                            Waiter = this.testWaiter
                        },
                        new DrinksOrdered
                        {
                            Id = this.testId,
                            Items = new List<OrderedItem> { this.testDrink1 }
                        },
                        new DrinksServed
                        {
                            Id = this.testId,
                            MenuNumbers = new List<int> { this.testDrink1.MenuNumber }
                        }
                    ),
                When(
                        new MarkDrinksToServe
                        {
                            Id = this.testId,
                            MenuNumbers = new List<int> { this.testDrink1.MenuNumber }
                        }
                ),
                ThenFailWith<DrinksNotOutstanding>()
                );

        }

        [Test]
        public void GivenTabOpenedDrinksOrderedDrinksServed_WhenCloseTabIssued_ThenTabClosed()
        {
            //CanCloseTabWithTip
            Test(
                    Given(
                            new TabOpened
                            {
                                Id = this.testId,
                                TableNumber = this.testTable,
                                Waiter = this.testWaiter
                            },
                            new DrinksOrdered
                            {
                                Id = this.testId,
                                Items = new List<OrderedItem> { this.testDrink2 }
                            },
                            new DrinksServed
                            {
                                Id = this.testId,
                                MenuNumbers = new List<int> { this.testDrink2.MenuNumber }
                            }
                        ),
                    When(
                            new CloseTab
                            {
                                Id = this.testId,
                                AmountPaid = this.testDrink2.Price + 0.50M
                            }
                        ),
                    Then(
                            new TabClosed
                            {
                                Id = this.testId,
                                AmountPaid = this.testDrink2.Price + 0.50M,
                                OrderValue = this.testDrink2.Price,
                                TipValue = 0.50M
                            }
                        )
                );
        }
    }
}   
