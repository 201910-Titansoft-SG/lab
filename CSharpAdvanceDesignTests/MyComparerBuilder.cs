using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    public class MyComparerBuilder : IEnumerable<Employee>
    {
        private readonly IComparer<Employee> _comparer;
        private readonly IEnumerable<Employee> _employees;

        public MyComparerBuilder(IEnumerable<Employee> employees, IComparer<Employee> comparer)
        {
            _employees = employees;
            _comparer = comparer;
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            return Sort(_employees, _comparer);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static IEnumerator<Employee> Sort(IEnumerable<Employee> employees, IComparer<Employee> comparer)
        {
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var employee = elements[i];

                    var compareResult = comparer.Compare(employee, minElement);

                    if (compareResult < 0)
                    {
                        minElement = employee;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}