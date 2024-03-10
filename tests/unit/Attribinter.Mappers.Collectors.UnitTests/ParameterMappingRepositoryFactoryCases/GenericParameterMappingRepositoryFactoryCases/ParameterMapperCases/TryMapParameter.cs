namespace Attribinter.Mappers.Collectors.ParameterMappingRepositoryFactoryCases.GenericParameterMappingRepositoryFactoryCases.ParameterMapperCases;

using Moq;

using System;
using System.Collections.Generic;

using Xunit;

public sealed class TryMapParameter
{
    private static IMappedArgumentRecorder<TRecord, TData>? Target<TRecord, TData, TParameter>(IParameterMapper<TParameter, TRecord, TData> mapper, TParameter parameter) => mapper.TryMapParameter(parameter);

    [Fact]
    public void NullParameter_ThrowsArgumentNullException()
    {
        var context = MapperContext<object, object, object>.Create((_) => { }, (_) => { });

        var exception = Record.Exception(() => Target(context.Mapper, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NonExisting_ReturnsNull()
    {
        var context = MapperContext<object, object, object>.Create((_) => { }, registrator);

        var actual = Target(context.Mapper, Mock.Of<object>());

        Assert.Null(actual);

        static void registrator(IParameterMappingCollector<object, object, object> collector) => collector.AddMapping(Mock.Of<object>(), Mock.Of<IMappedArgumentRecorder<object, object>>());
    }

    [Fact]
    public void Existing_ReturnsRecorder()
    {
        var recorder = Mock.Of<IMappedArgumentRecorder<object, object>>();

        var context = MapperContext<object, object, object>.Create(setup, registrator);

        var actual = Target(context.Mapper, Mock.Of<object>());

        Assert.Same(recorder, actual);

        static void setup(Mock<IEqualityComparer<object>> comparer)
        {
            comparer.Setup(static (comparer) => comparer.GetHashCode(It.IsAny<object>())).Returns(42);
            comparer.Setup(static (comparer) => comparer.Equals(It.IsAny<object>(), It.IsAny<object>())).Returns(true);
        }

        void registrator(IParameterMappingCollector<object, object, object> collector) => collector.AddMapping(Mock.Of<object>(), recorder);
    }
}
