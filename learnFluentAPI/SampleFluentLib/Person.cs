using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace SampleFluentLib
{
    public sealed class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        public void MockMethod()
        { }
    }

    public static class NotifyPropertyChangedExtensions
    {
        public static PropertyChangedExpectation<T> ShouldNotifyFor<T, TProp>(this T subject,
            Expression<Func<T, TProp>> propertyExpression)
            where T : INotifyPropertyChanged
        {
            return new PropertyChangedExpectation<T>("");
        }

        private static string GetPropertyName<T, TProp>(Expression<Func<T,TProp>> propertyExpression)
        {
            var body = propertyExpression.Body as MemberExpression;
            return body.Member.Name;
        }
    }
}
