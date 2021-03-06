// <auto-generated/>
using System.Linq;
using NUnit.Framework.Interfaces;

namespace AutoDataConnector
{
    public class ParameterValueProvider : IParameterValueProvider
    {
        private readonly ITypedDataProvider _typedDataProvider;

        public ParameterValueProvider(ITypedDataProvider typedDataProvider)
        {
            _typedDataProvider = typedDataProvider;
        }

        public object Get(IParameterInfo parameterInfo)
        {
            return IsFrozen(parameterInfo)
                ? _typedDataProvider.CreateFrozenValue(parameterInfo.ParameterType)
                : _typedDataProvider.CreateValue(parameterInfo.ParameterType);
        }

        private static bool IsFrozen(IReflectionInfo reflectionInfo)
        {
            return reflectionInfo.GetCustomAttributes<FrozenAttribute>(true).Any();
        }
    }
}