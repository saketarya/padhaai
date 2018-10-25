namespace ConsoleApp2
{
    public interface ITaxCalculator
    {
        bool Apply(IOrder order);
        void Calculate(IOrder order);
    }
}