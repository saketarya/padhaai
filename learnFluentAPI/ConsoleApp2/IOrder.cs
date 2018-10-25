namespace ConsoleApp2
{
    public interface IOrder
    {
        System.Collections.Generic.List<IOrderLineItem> OrderLineItems { get; set; }
        int OrderId { get; set; }
        int CustomerId { get; set; }
        bool Valid { get; set; }
        object DateProcessed { get; set; }
    }
}