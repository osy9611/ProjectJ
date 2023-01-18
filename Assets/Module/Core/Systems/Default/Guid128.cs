using System;

namespace Module.Core.Systems
{
    public struct Guid128
    {
        private long id1;
        private long id2;
        private string stringId;

        public long Id1
        {
            get => id1;
            set
            {
                id1 = value;
                stringId = null;
            }
        }

        public long Id2
        {
            get => id2;
            set
            {
                id2 = value;
                stringId = null;
            }
        }

        public string StringId
        {
            get
            {
                if(stringId == null)
                {
                    CreateStringId();
                }

                return stringId;
            }
        }

        public Guid128(long id1, long id2)
        {
            this.id1 = id1;
            this.id2 = id2;

            stringId = null;
        }

        private void CreateStringId()
        {
            byte[] byteId1 = BitConverter.GetBytes(id1);
            byte[] byteId2 = BitConverter.GetBytes(id2);

            stringId = ToString(byteId1, 0, byteId1.Length) + ToString(byteId2, 0, byteId2.Length);
        }

        public override string ToString()
        {
            return stringId;
        }

        public override bool Equals(object o)
        {
            if (((Guid128)o).id1 == id1 && ((Guid128)o).Id2 == Id2)
                return true;
            else
                return false;
        }

        public static bool operator == (Guid128 c1, Guid128 c2)
        {
            return c1.Equals(c2);
        }
        public static bool operator !=(Guid128 c1, Guid128 c2)
        {
            return ! c1.Equals(c2);
        }

        static public Guid128 Generate()
        {
            var guid = Guid.NewGuid();

            Guid128 value = new Guid128();

            var bytes = guid.ToByteArray();

            value.id1 = BitConverter.ToInt64(bytes, 0);
            value.id2 = BitConverter.ToInt64(bytes, 0);

            return value;
        }

        private static string ToString(byte[] value, int startIndex, int length)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (startIndex < 0 || startIndex >= value.Length && startIndex > 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex), "ArgumentOutOfRange_StartIndex");
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), "ArgumentOutOfRange_GenericPositive");
            if (startIndex > value.Length - length)
                throw new ArgumentException("Arg_ArrayPlusOffTooSmall");
            if (length == 0)
                return string.Empty;
            if (length > 715827882)
                throw new ArgumentOutOfRangeException(nameof(length), "ArgumentOutOfRange_LengthTooLarge");

            int length1 = length * 2;
            char[] chArray = new char[length1];
            int num1 = startIndex;

            for(int index= 0,range = length1;index<range;index+=2)
            {
                byte num2 = value[num1++];
                chArray[index] = GetHexValue((int)num2 / 16);
                chArray[index+1] = GetHexValue((int)num2 % 16);
            }

            return new string(chArray, 0, chArray.Length);
        }

        private static char GetHexValue(int i) => i < 10 ? (char)(i + 48) : (char)(i - 10 + 65);
    }
}