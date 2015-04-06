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
        
        private Encoder _encoderUnderTest = new Encoder(10);

        private List<byte> _inputList = new List<byte>();
        private List<byte> _refList = new List<byte>();

        // --------------------------------------------------------------------

        private bool CompareLists(List<byte> list1, List<byte> list2)
        {
            int firstNotSecond = list2.Except(list1).ToList().Count;
            int secondNotFirst = list1.Except(list2).ToList().Count;
            return ((firstNotSecond + secondNotFirst) == 0);
        }

        private bool EncodeAndCompare(List<byte> inputList, List<byte> refList)
        {
            List<byte> outputList = _encoderUnderTest.Encode(inputList);
            return CompareLists(outputList, refList);
        }

        // --------------------------------------------------------------------
        
        [Test]
        public void ZerosInTheMiddle()
        {
            _inputList = new List<byte>() { 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 };
            _refList = new List<byte>() { 0x00, 0x01, 0x00, 0x01, 0x05, 0x01 };
            Assert.AreEqual(true, EncodeAndCompare(_inputList, _refList));
        }

        [Test]
        public void OnesInTheMiddle()
        {
            _inputList = new List<byte>() { 0x01, 0x00, 0x01, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00 };
            _refList = new List<byte>() { 0x01, 0x00, 0x01, 0x00, 0x85, 0x00 };
            Assert.AreEqual(true, EncodeAndCompare(_inputList, _refList));
        }

        [Test]
        public void ZerosOnTheEdge()
        {
            _inputList = new List<byte>() { 0x00, 0x00, 0x00, 0x00, 0x01, 0x00 };
            _refList = new List<byte>() { 0x04, 0x01, 0x00 };
            Assert.AreEqual(true, EncodeAndCompare(_inputList, _refList));
        }

        [Test]
        public void OnesOnTheEdge()
        {
            _inputList = new List<byte>() { 0x00, 0x01, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01 };
            _refList = new List<byte>() { 0x00, 0x01, 0x00, 0x86 };
            Assert.AreEqual(true, EncodeAndCompare(_inputList, _refList));
        }

        [Test]
        public void AllZeros()
        {
            _inputList = new List<byte>() { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            _refList = new List<byte>() { 0x08 };
            Assert.AreEqual(true, EncodeAndCompare(_inputList, _refList));
        }

        [Test]
        public void TwoSequencesConcatenated()
        {
            _inputList = new List<byte>() { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x01, 0x01 };
            _refList = new List<byte>() { 0x08, 0x83 };
            Assert.AreEqual(true, EncodeAndCompare(_inputList, _refList));
        }

        [Test]
        public void SequenceTooLong()
        {
            _inputList = new List<byte>() { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 };
            _refList = new List<byte>() { 0x0A, 0x05, 0x01 };
            Assert.AreEqual(true, EncodeAndCompare(_inputList, _refList));
        }
    }
}
