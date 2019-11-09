﻿using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyComparer
    {
        public CombineKeyComparer(Func<Employee, string> firstKeySelector, IComparer<string> firstKeyComparer)
        {
            FirstKeySelector = firstKeySelector;
            FirstKeyComparer = firstKeyComparer;
        }

        public Func<Employee, string> FirstKeySelector { get; private set; }
        public IComparer<string> FirstKeyComparer { get; private set; }

        public int Compare(Employee employee, Employee minElement)
        {
            return FirstKeyComparer.Compare(FirstKeySelector(employee), FirstKeySelector(minElement));
        }
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        //[Test]
        //public void orderBy_lastName()
        //{
        //    var employees = new[]
        //    {
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //    };

        //    var actual = JoeyOrderByLastName(employees);

        //    var expected = new[]
        //    {
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //    };

        //    expected.ToExpectedObject().ShouldMatch(actual);
        //}

        [Test]
        public void orderBy_lastName_and_firstName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = JoeyOrderBy(employees,
                new CombineKeyComparer(employee => employee.LastName, Comparer<string>.Default),
                employee => employee.FirstName,
                Comparer<string>.Default);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderBy(
            IEnumerable<Employee> employees,
            CombineKeyComparer combineKeyComparer,
            Func<Employee, string> secondKeySelector,
            IComparer<string> secondKeyComparer)
        {
            //bubble sort
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var employee = elements[i];

                    if (combineKeyComparer.Compare(employee, minElement) < 0)
                    {
                        minElement = employee;
                        index = i;
                    }
                    else if (combineKeyComparer.FirstKeyComparer.Compare(combineKeyComparer.FirstKeySelector(employee), combineKeyComparer.FirstKeySelector(minElement)) == 0)
                    {
                        if (secondKeyComparer.Compare(secondKeySelector(employee), secondKeySelector(minElement)) < 0)
                        {
                            minElement = employee;
                            index = i;
                        }
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }

        //private IEnumerable<Employee> JoeyOrderByLastName(IEnumerable<Employee> employees)
        //{
        //    //bubble sort
        //    var stringComparer = Comparer<string>.Default;
        //    var elements = employees.ToList();
        //    while (elements.Any())
        //    {
        //        var minElement = elements[0];
        //        var index = 0;
        //        for (int i = 1; i < elements.Count; i++)
        //        {
        //            if (stringComparer.Compare(elements[i].LastName, minElement.LastName) < 0)
        //            {
        //                minElement = elements[i];
        //                index = i;
        //            }
        //        }

        //        elements.RemoveAt(index);
        //        yield return minElement;
        //    }
        //}
    }
}