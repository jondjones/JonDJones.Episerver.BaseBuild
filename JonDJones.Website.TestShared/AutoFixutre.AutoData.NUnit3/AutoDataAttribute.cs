﻿// <auto-generated/>
using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;
using ITestCaseData = NUnit.Framework.Interfaces.ITestCaseData;

namespace AutoDataConnector
{
    /// <summary>
    /// Provide auto-generated values to parameters.
    /// Code is based on TestCaseAttribute of NUnit3
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class AutoDataAttribute : NUnitAttribute, ITestBuilder, ITestCaseData, IImplyFixture
    {
        public AutoDataAttribute() : this(new ParameterValueProvider(new AutoFixtureDataProvider()))
        {
        }

        public AutoDataAttribute(IParameterValueProvider parameterValueProvider)
        {
            RunState = RunState.Runnable;
            Properties = new PropertyBag();

            _parameterValueProvider = parameterValueProvider;
        }

        public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
        {
            var test = new NUnitTestCaseBuilder().BuildTestMethod(method, suite, GetParametersForMethod(method));

            yield return test;
        }

        private TestCaseParameters GetParametersForMethod(IMethodInfo method)
        {
            try
            {
                var parameterValues = CreateParameterValues(method.GetParameters());

                return new TestCaseParameters(parameterValues);
            }
            catch (Exception ex)
            {
                return new TestCaseParameters(ex);
            }
        }

        private object[] CreateParameterValues(IEnumerable<IParameterInfo> parameters)
        {
            return parameters.Select(_parameterValueProvider.Get).ToArray();
        }

        public string TestName { get; set; }

        public RunState RunState { get; private set; }

        public object[] Arguments { get; private set; }

        public IPropertyBag Properties { get; private set; }

        public object ExpectedResult
        {
            get
            {
                return _expectedResult;
            }
            set
            {
                _expectedResult = value;
                HasExpectedResult = true;
            }
        }

        private object _expectedResult;

        public bool HasExpectedResult { get; private set; }

        public string Description
        {
            get
            {
                return Properties.Get(PropertyNames.Description) as string;
            }
            set
            {
                Properties.Set(PropertyNames.Description, value);
            }
        }

        public string Author
        {
            get
            {
                return Properties.Get(PropertyNames.Author) as string;
            }
            set
            {
                Properties.Set(PropertyNames.Author, value);
            }
        }

        public Type TestOf
        {
            get
            {
                return _testOf;
            }
            set
            {
                _testOf = value;
                Properties.Set(PropertyNames.TestOf, value.FullName);
            }
        }

        private Type _testOf;

        private readonly IParameterValueProvider _parameterValueProvider;

        public string Ignore
        {
            get
            {
                return IgnoreReason;
            }
            set
            {
                IgnoreReason = value;
            }
        }

        public bool Explicit
        {
            get
            {
                return RunState == RunState.Explicit;
            }
            set
            {
                RunState = value ? RunState.Explicit : RunState.Runnable;
            }
        }

        public string Reason
        {
            get
            {
                return Properties.Get(PropertyNames.SkipReason) as string;
            }
            set
            {
                Properties.Set(PropertyNames.SkipReason, value);
            }
        }

        public string IgnoreReason
        {
            get
            {
                return Reason;
            }
            set
            {
                RunState = RunState.Ignored;
                Reason = value;
            }
        }

#if !PORTABLE

        /// <summary>
        ///     Comma-delimited list of platforms to run the test for
        /// </summary>
        public string IncludePlatform { get; set; }

        /// <summary>
        ///     Comma-delimited list of platforms to not run the test for
        /// </summary>
        public string ExcludePlatform { get; set; }
#endif

        public string Category
        {
            get
            {
                return Properties.Get(PropertyNames.Category) as string;
            }
            set
            {
                foreach (var cat in value.Split(','))
                {
                    Properties.Add(PropertyNames.Category, cat);
                }
            }
        }
    }
}