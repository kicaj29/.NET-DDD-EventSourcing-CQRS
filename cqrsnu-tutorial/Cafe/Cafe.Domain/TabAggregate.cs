using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe;
using Cafe.Domain.Commands;
using Cafe.Domain.Events;
using Cafe.Domain.Exceptions;

namespace Cafe.Domain
{
    public class TabAggregate : Aggregate, 
        IHandleCommand<OpenTab>, 
        IHandleCommand<PlaceOrder>,
        IHandleCommand<MarkDrinksToServe>,
        IHandleCommand<CloseTab>,
        IHandleCommand<MarkFoodPrepared>,
        IHandleCommand<MarkFoodServed>,
        IApplyEvent<TabOpened>,
        IApplyEvent<DrinksOrdered>,
        IApplyEvent<DrinksServed>,
        IApplyEvent<FoodOrdered>,
        IApplyEvent<FoodPrepared>,
        IApplyEvent<FoodServed>
    {
        private decimal servedItemsValue = 0M;
        private bool open = false;
        private List<OrderedItem> outstandingDrinks = new List<OrderedItem>();
        private List<OrderedItem> outstandingFood = new List<OrderedItem>();
        private List<OrderedItem> preparedFood = new List<OrderedItem>();

        public IEnumerable<dynamic> Handle(OpenTab c)
        {
            yield return new TabOpened
            {
                Id = c.Id,
                TableNumber = c.TableNumber,
                Waiter = c.Waiter
            };
        }

        public IEnumerable<dynamic> Handle(PlaceOrder c)
        {
            if (!this.open)
                throw new TabNotOpen();

            var drink = c.Items.Where(i => i.IsDrink).ToList();
            if (drink.Any())
                yield return new DrinksOrdered
                {
                    Id = c.Id,
                    Items = drink
                };

            var food = c.Items.Where(i => !i.IsDrink).ToList();
            if (food.Any())
                yield return new FoodOrdered
                {
                    Id = c.Id,
                    Items = food
                };
        }

        public IEnumerable<dynamic> Handle(MarkDrinksToServe c)
        {
            if (!this.AreDrinksOutstanding(c.MenuNumbers))
            {
                throw new DrinksNotOutstanding();
            }

            yield return new DrinksServed
            {
                Id = c.Id,
                MenuNumbers = c.MenuNumbers
            };
        }

        private static bool AreAllInList(List<int> want, List<OrderedItem> have)
        {
            var curHave = new List<int>(have.Select(i => i.MenuNumber));
            foreach (var num in want)
                if (curHave.Contains(num))
                    curHave.Remove(num);
                else
                    return false;
            return true;
        }

        private bool AreDrinksOutstanding(List<int> menuNumbers)
        {
            return AreAllInList(want: menuNumbers, have: outstandingDrinks);
        }

        private bool IsFoodOutstanding(List<int> menuNumbers)
        {
            return AreAllInList(want: menuNumbers, have: outstandingFood);
        }

        private bool IsFoodPrepared(List<int> menuNumbers)
        {
            return AreAllInList(want: menuNumbers, have: preparedFood);
        }
        /// <summary>
        /// The Apply should update the aggregate's state based on the event and its data.
        /// For example if we want materialize the aggregate in an application or in tests to satisfy the GIVEN part.
        /// </summary>
        /// <param name="e"></param>
        public void Apply(TabOpened e)
        {
            this.open = true;
        }

        public void Apply(DrinksOrdered e)
        {
            this.outstandingDrinks.AddRange(e.Items);
        }

        public void Apply(DrinksServed e)
        {
            foreach (var num in e.MenuNumbers)
            {
                var item = outstandingDrinks.First(d => d.MenuNumber == num);
                this.outstandingDrinks.Remove(item);
                this.servedItemsValue += item.Price;
            }
        }

        public IEnumerable<dynamic> Handle(CloseTab c)
        {
            yield return new TabClosed
            {
                Id = c.Id,
                AmountPaid = c.AmountPaid,
                OrderValue = this.servedItemsValue,
                TipValue = c.AmountPaid - this.servedItemsValue
            };
        }

        public IEnumerable<dynamic> Handle(MarkFoodPrepared c)
        {
            if (!this.IsFoodOutstanding(c.MenuNumbers))
                throw new FoodNotOutstanding();

            yield return new FoodPrepared
            {
                Id = c.Id,
                MenuNumbers = c.MenuNumbers
            };
        }

        public IEnumerable<dynamic> Handle(MarkFoodServed c)
        {
            if (!IsFoodPrepared(c.MenuNumbers))
                throw new FoodNotPrepared();

            yield return new FoodServed
            {
                Id = c.Id,
                MenuNumbers = c.MenuNumbers
            };
        }

        public void Apply(FoodOrdered e)
        {
            this.outstandingFood.AddRange(e.Items);
        }

        public void Apply(FoodPrepared e)
        {
            foreach (var num in e.MenuNumbers)
            {
                var item = outstandingFood.First(f => f.MenuNumber == num);
                outstandingFood.Remove(item);
                preparedFood.Add(item);
            }
        }

        public void Apply(FoodServed e)
        {
            foreach (var num in e.MenuNumbers)
            {
                var item = preparedFood.First(f => f.MenuNumber == num);
                preparedFood.Remove(item);
                servedItemsValue += item.Price;
            }
        }
    }
}
