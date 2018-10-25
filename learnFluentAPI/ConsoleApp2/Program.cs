using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderEngine
                .Initialize()
                .Using(new TaxCalculator())
                .Customer(555)
                .AddLineItem(new OrderLineItem())
                .AddValidateFunction((o) => { return true; })
                .Process();
        }
    }

    public class OrderEngine
    {
        public ITaxCalculator TaxCalculator { get; internal set; }
        public IOrderProcessor OrderProcessor { get; internal set; }
        public IOrder Order { get; internal set; }
        public List<Func<IOrder, bool>> ValidateFunctions { get; internal set; }

        public static OrderEngine Initialize()
        {
            // Instantiate dependencies
            var orderEngine = new OrderEngine()
            {
                Order = new Order(),
                TaxCalculator = new TaxCalculator(),
                OrderProcessor = new OrderProcessor(),
                ValidateFunctions = new List<Func<IOrder, bool>>()
            };
            orderEngine.Order.OrderLineItems = new List<IOrderLineItem>();

            // Set the order Id
            orderEngine.Order.OrderId = (new Random()).Next();

            // Add a simplistic null check
            orderEngine.ValidateFunctions.Add(o => o != null && o.OrderLineItems != null);

            return orderEngine;
        }
    }

    public static class OrderEngineEntensions
    {
        public static OrderEngine Customer(this OrderEngine @orderEngine, int customerId)
        {
            @orderEngine.Order.CustomerId = customerId;

            return @orderEngine;
        }

        public static OrderEngine AddLineItem(
           this OrderEngine @orderEngine, IOrderLineItem orderLineItem)
        {
            if (@orderEngine.Order == null)
                @orderEngine.Order = new Order();

            if (@orderEngine.Order.OrderLineItems == null)
                @orderEngine.Order.OrderLineItems = new List<IOrderLineItem>();

            @orderEngine.Order.OrderLineItems.Add(orderLineItem);

            return @orderEngine;
        }

        public static OrderEngine Process(this OrderEngine @orderEngine)
        {
            // Can't instantiate an order for processing; need an order with details.
            if (@orderEngine == null || @orderEngine.Order == null)
                throw new InvalidOperationException("Processing not provided an Order.");

            if (@orderEngine.TaxCalculator != null
                && @orderEngine.TaxCalculator.Apply(@orderEngine.Order))
                @orderEngine.TaxCalculator.Calculate(@orderEngine.Order);

            // Run thru any validation checks
            @orderEngine.Order.Valid = true;

            if (@orderEngine.ValidateFunctions != null)
            {
                foreach (var validateFunction in @orderEngine.ValidateFunctions)
                {
                    @orderEngine.Order.Valid =
                        @orderEngine.Order.Valid
                            && validateFunction(@orderEngine.Order);
                }
            }

            // Process the order
            @orderEngine.Order.DateProcessed = null;

            if (@orderEngine.Order.Valid)
                @orderEngine.OrderProcessor.Process(@orderEngine.Order);

            return @orderEngine;
        }

        public static OrderEngine Using(
          this OrderEngine @orderEngine, ITaxCalculator taxCalculator)
        {
            @orderEngine.TaxCalculator = taxCalculator;
            return @orderEngine;
        }

        public static OrderEngine AddValidateFunction(
           this OrderEngine @orderEngine, Func<IOrder, bool> validationFunction)
        {
            if (validationFunction == null) throw new ArgumentNullException("validationFunction");

            if (@orderEngine.ValidateFunctions == null)
                @orderEngine.ValidateFunctions = new List<Func<IOrder, bool>>();

            @orderEngine.ValidateFunctions.Add(validationFunction);

            return @orderEngine;
        }
    }

    internal class OrderProcessor : IOrderProcessor
    {
        public void Process(IOrder order)
        {
            throw new NotImplementedException();
        }
    }

    internal class TaxCalculator : ITaxCalculator
    {
        public bool Apply(IOrder order)
        {
            throw new NotImplementedException();
        }

        public void Calculate(IOrder order)
        {
            throw new NotImplementedException();
        }
    }

    internal class Order : IOrder
    {
        public List<IOrderLineItem> OrderLineItems { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int OrderId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CustomerId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Valid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object DateProcessed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    internal class OrderLineItem : IOrderLineItem
    { }
}
