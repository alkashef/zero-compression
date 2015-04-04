using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ZeroCompression;

namespace Unit_Tests
{
    [TestFixture]
    class EncoderTest
    {
        // --------------------------------------------------------------------
        
        private Encoder _encoderUnderTest = new Encoder();

        private List<int> _inputList = new List<int>();
        private List<int> _refList = new List<int>();

        // --------------------------------------------------------------------
        
        private bool CompareLists(List<int> list1, List<int> list2)
        {
            int firstNotSecond = list2.Except(list1).ToList().Count;
            int secondNotFirst = list1.Except(list2).ToList().Count;
            return ((firstNotSecond + secondNotFirst) == 0);
        }

        private bool EncodeAndCompare(List<int> inputList, List<int> refList)
        {
            List<int> outputList = _encoderUnderTest.Encode(inputList);
            return CompareLists(outputList, refList);
        }

        // --------------------------------------------------------------------
        
        [Test]
        public void ZerosInTheMiddle()
        {
            _inputList = new List<int>() { 0, 1, 0, 1, 0, 0, 0, 0, 0, 1 };
            _refList = new List<int>() { 0, 1, 0, 1, 5, 1 };
            Assert.AreEqual(true, EncodeAndCompare(_inputList, _refList));
        }

        [Test]
        public void OnesInTheMiddle()
        {
            _inputList = new List<int>() { 1, 0, 1, 0, 1, 1, 1, 1, 1, 0 };
            _refList = new List<int>() { 1, 0, 1, 0, 5 + 128, 0 };
            Assert.AreEqual(true, EncodeAndCompare(_inputList, _refList));
        }

        [Test]
        public void ZerosOnTheEdge()
        {
            _inputList = new List<int>() {0,0,0,0,1,0};
            _refList = new List<int>() {4,1,0};
            Assert.AreEqual(true, EncodeAndCompare(_inputList, _refList));
        }

        [Test]
        public void OnesOnTheEdge()
        {
            _inputList = new List<int>() {0,1,0,1,1,1,1,1,1};
            _refList = new List<int>() {0,1,0,6+128};
            Assert.AreEqual(true, EncodeAndCompare(_inputList, _refList));
        }

        [Test]
        public void AllZeros()
        {
            _inputList = new List<int>() {0,0,0,0,0,0,0,0};
            _refList = new List<int>() {8};
            Assert.AreEqual(true, EncodeAndCompare(_inputList, _refList));
        }

        [Test]
        public void TwoSequencesConcatenated()
        {
            _inputList = new List<int>() {0,0,0,0,0,0,0,0,1,1,1};
            _refList = new List<int>() {8,3+128};
            Assert.AreEqual(true, EncodeAndCompare(_inputList, _refList));
        }

        [Test]
        public void SequenceTooLong()
        {
            _inputList = new List<int>() {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1};
            _refList = new List<int>() {10,5,1};
            Assert.AreEqual(true, EncodeAndCompare(_inputList, _refList));
        }
    }
}
