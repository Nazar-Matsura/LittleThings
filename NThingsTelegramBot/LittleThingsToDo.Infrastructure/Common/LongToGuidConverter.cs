using System;
using LittleThingsToDo.Application.Interfaces.Infrastructure;
using LittleThingsToDo.Infrastructure.Exceptions;

namespace LittleThingsToDo.Infrastructure.Common
{
    public class LongToGuidConverter : ILongToGuidConverter
    {
        public Guid Convert(long val)
        {
            var bytes = new byte[16];
            BitConverter.GetBytes(val).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public long Convert(Guid val)
        {
            var guidBytes = val.ToByteArray();
            if (ArrayIsTooBigForLong(guidBytes))
                throw new CantConvertGuidToLongException();

            return BitConverter.ToInt64(guidBytes, 0);
        }

        private bool ArrayIsTooBigForLong(byte[] guidBytes)
        {
            return BitConverter.ToInt64(guidBytes, 8) != 0;
        }
    }
}
