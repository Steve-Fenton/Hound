using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace Hound.Tests
{
    public class LogHoundTests
    {
        private readonly string _testApiKey;

        public LogHoundTests()
        {
            ApiKey key = new TestApiKey();
            _testApiKey = key.GetApiKey();
        }

        [Fact]
        public void TestExceptionShouldBeSuccess()
        {
            try
            {
                throw new TestException("Hound-001", "Sheridan Le Fanu");
            }
            catch (HoundException ex)
            {
                HoundResult result = LogHound.LogException(_testApiKey, ex);
                result.IsSuccess.ShouldBeTrue();
            }
        }

        [Fact]
        public void TestExceptionWithTagsShouldBeSuccess()
        {
            try
            {
                throw new TestException("Hound-001", "Sheridan Le Fanu");
            }
            catch (HoundException ex)
            {
                HoundResult result = LogHound.LogException(_testApiKey, ex, new List<string>() { "test", "exception" });
                result.IsSuccess.ShouldBeTrue();
            }
        }


        [Fact]
        public void TestWarningExceptionShouldBeSuccess()
        {
            try
            {
                throw new TestWarningException("Hound-001", "In a Glass Darkly");
            }
            catch (HoundException ex)
            {
                HoundResult result = LogHound.LogException(_testApiKey, ex);
                result.IsSuccess.ShouldBeTrue();
            }
        }

        [Fact]
        public void GeneralExceptionWrappedShouldBeSuccess()
        {
            try
            {
                throw new Exception("An example of a general exception");
            }
            catch (Exception ex)
            {
                HoundResult result = LogHound.LogException(_testApiKey, new TestUnexpectedException("Hound-001", ex));
                result.IsSuccess.ShouldBeTrue();
            }
        }

        [Fact]
        public void GeneralExceptionNotWrappedShouldBeSuccess()
        {
            try
            {
                throw new Exception("An example of a general exception");
            }
            catch (Exception ex)
            {
                HoundResult result = LogHound.LogException(_testApiKey, ex);
                result.IsSuccess.ShouldBeTrue();
            }
        }

        [Fact]
        public void MultipleCallsShouldAllBeSuccess()
        {
            LogHound.LogException(_testApiKey, new Exception("An example of a general exception"));
            LogHound.LogException(_testApiKey, new Exception("An example of a general exception"));
        }

        [Fact]
        public void InvalidApiKeyShouldBeFailureNotException()
        {
            HoundResult result = LogHound.LogException("NULL", new TestException("Hound-001", "Mary Shelley"));
            result.IsSuccess.ShouldBeFalse();
        }
    }
}