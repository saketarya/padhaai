using System;
using System.ComponentModel;

namespace SampleFluentLib
{
    public class PropertyChangedExpectation<T> where T : INotifyPropertyChanged
    {
        private readonly string propertyName;
        public PropertyChangedExpectation(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public void When(Action action)
        {
            throw new ArgumentException($"Expected PropertyChanged event to fire for {typeof(T)}.{this.propertyName}");
        }
    }
}