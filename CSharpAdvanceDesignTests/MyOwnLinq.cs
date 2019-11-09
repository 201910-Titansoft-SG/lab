using System;
using System.Collections.Generic;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    public static class MyOwnLinq
    {
        public static IMyOrderedEnumerable JoeyOrderBy<TKey>(this IEnumerable<Employee> employees,
            Func<Employee, TKey> keySelector)
        {
            var comparer = new CombineKeyComparer<TKey>(keySelector, Comparer<TKey>.Default);
            return new MyComparerBuilder(employees, comparer);
        }

        public static IMyOrderedEnumerable JoeyThenBy<TKey>(this IMyOrderedEnumerable employees,
            Func<Employee, TKey> keySelector)
        {
            return employees.CreateOrderedEnumerable(new CombineKeyComparer<TKey>(keySelector, Comparer<TKey>.Default));
        }

        public static IEnumerable<Employee> JoeyOrderByComboComparer(this IEnumerable<Employee> employees,
            IComparer<Employee> comparer)
        {
            //return MyComparerBuilder.Sort(employees, comparer);
            return new MyComparerBuilder(employees, comparer);
        }
    }
}