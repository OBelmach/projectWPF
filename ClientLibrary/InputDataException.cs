using System;

namespace ClientLibrary
{
    public class InputDataException : Exception
    {
        public InputDataException(string inputMsg) : base(inputMsg)
        {
            
        }
    }
}
