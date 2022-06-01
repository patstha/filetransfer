using FileTransfer.StringManipulation;
using System.Collections.Generic;
using System.Linq;
using Xunit;
namespace FileTransfer.Xunit
{
    public class StringManipulationTests
    {
        [Fact]
        public void TestFreebie()
        {
            // Arrange 
            int expected = 4;
            int input1 = 1;
            int input2 = 3;

            // Act 
            int actual = input1 + input2;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestEmptyString()
        {
            // Arrange 
            List<ChangeDetectionToken> expected = new();
            string input = "";

            // Act 
            List<ChangeDetectionToken> actual = ChangeDetectionAction.TokenizeString(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestOneToken1()
        {
            // Arrange 
            string key = "item 1";
            string oldValue = "old value 1";
            string newValue = "new value 1";
            string input = $"{key}~{oldValue}~{newValue}";

            // Act 
            List<ChangeDetectionToken> actual = ChangeDetectionAction.TokenizeString(input);

            // Assert
            Assert.Contains(actual, x => x.Key == key);
            Assert.Equal(actual.First(x => x.Key == key).OldValue, oldValue);
            Assert.Equal(actual.First(x => x.Key == key).NewValue, newValue);
        }

        [Fact]
        public void TestTwoToken1()
        {
            // Arrange 
            string key1 = "item 1";
            string oldValue1 = "old value 1";
            string newValue1 = "new value 1";
            string key2 = "item 2";
            string oldValue2 = "old value 2";
            string newValue2 = "new value 2";
            string input = $"{key1}~{oldValue1}~{newValue1}|{key2}~{oldValue2}~{newValue2}";

            // Act 
            List<ChangeDetectionToken> actual = ChangeDetectionAction.TokenizeString(input);

            // Assert
            Assert.Contains(actual, x => x.Key == key1);
            Assert.Equal(actual.First(x => x.Key == key1).OldValue, oldValue1);
            Assert.Equal(actual.First(x => x.Key == key1).NewValue, newValue1);
            Assert.Contains(actual, x => x.Key == key2);
            Assert.Equal(actual.First(x => x.Key == key2).OldValue, oldValue2);
            Assert.Equal(actual.First(x => x.Key == key2).NewValue, newValue2);
        }
    }
}
