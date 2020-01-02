using AutoFixture.Xunit2;
using Blacksmith.Exceptions;
using FluentAssertions;
using System;
using Xunit;

namespace Blacksmith.ResultTests
{
    public class ResultTests
    {
        [Fact]
        public void when_create_default_instance_Success_should_be_true()
        {
            Result result;

            result = new Result();

            result.Success.Should().BeTrue();
            result.Exceptions.Should().BeEmpty();
        }

        [Fact]
        public void when_create_instance_with_exceptions_Success_should_be_false()
        {
            Result result;

            result = new Result(new Exception());

            result.Success.Should().BeFalse();
            result.Exceptions.Should().HaveCount(1);
        }

        [Fact]
        public void when_create_success_valued_instance_can_get_value()
        {
            Result<int> result;

            result = new Result<int>(34);

            result.Value.Should().Be(34);
            result.Success.Should().BeTrue();
            result.Exceptions.Should().BeEmpty();
        }

        [Fact]
        public void when_create_unsuccess_valued_instance_and_check_for_value_it_throws_exception()
        {
            Result<int> result;

            result = new Result<int>(new Exception());

            result.Success.Should().BeFalse();
            result.Exceptions.Should().HaveCount(1);

            result
                .Invoking(r => r.Value)
                .Should()
                .ThrowExactly<ValueRequestedOnUnsuccessResultException>();
        }

        [Fact]
        public void can_cast_exception_to_result_T()
        {
            Result<int> result;

            result = new Exception();

            result.Success.Should().BeFalse();
            result.Exceptions.Should().HaveCount(1);

            result
                .Invoking(r => r.Value)
                .Should()
                .ThrowExactly<ValueRequestedOnUnsuccessResultException>();
        }

        [Theory]
        [AutoData]
        public void can_cast_value_to_Result_T(double value)
        {
            Result<double> result;

            result = value;

            result.Success.Should().BeTrue();
            result.Exceptions.Should().BeEmpty();
            result.Value.Should().Be(value);
        }

        [Fact]
        public void can_cast_exception_to_Result()
        {
            Result result;

            result = new Exception();

            result.Success.Should().BeFalse();
            result.Exceptions.Should().HaveCount(1);
        }
    }
}
