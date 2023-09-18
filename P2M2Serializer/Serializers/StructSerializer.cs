using System;
using System.Runtime.InteropServices;

namespace P2M2Serializer.Serializers
{
    public class StructSerializer : IStructSerializer
    {
        /// <summary>
        /// Deserializes a byte array into a defined struct
        /// </summary>
        /// <typeparam name="T">Struct type that the data should be deserialized into.</typeparam>
        /// <param name="structBytes">Raw byte data for the struct.</param>
        /// <param name="startIndex">The byte index that the deserialization should start on.</param>
        /// <param name="endIndex">The byte index that the deserialization should end on.</param>
        /// <returns>The passed bytes as a struct.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T ToStruct<T>(byte[] structBytes, int startIndex, int endIndex)
            where T : struct
        {
            if (startIndex < 0 || endIndex > structBytes.Length || startIndex > endIndex)
            {
                throw new ArgumentOutOfRangeException("Invalid start and end indices.");
            }

            var typeOfGeneric = typeof(T);
            byte[] bytes;

            //If generic type is array, deserialize each individual struct of type T within the specified range.
            if(typeOfGeneric.IsArray)
            {
                var structSize = Marshal.SizeOf(typeOfGeneric.GetElementType());
                var arrayElements = (endIndex - startIndex) / structSize;
                var arrayByteSize = endIndex - startIndex;
                bytes = new byte[arrayByteSize];

                for(var i = 0; i < arrayElements; i++)
                {
                    Array.Copy(structBytes, structSize * i, bytes, structSize * i, structSize);
                }
            }
            //Not an array so deserialize as a single struct
            else
            {
                var length = endIndex - startIndex;
                bytes = new byte[length];

                Array.Copy(structBytes, startIndex, bytes, 0, length);
            }

            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                return Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            }
            finally
            {
                handle.Free();
            }
        }

        /// <summary>
        /// Serialize the passed item to raw byte data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="structure"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public byte[] ToBytes<T>(T structure)
            where T : struct
        {
            if(!IsValueType<T>())
            {
                throw new ArgumentException($"{typeof(T).Name} is not a value type or an array of a value type.");
            }

            var size = Marshal.SizeOf<T>();
            var bytes = new byte[size];

            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structure, ptr, true);
            Marshal.Copy(ptr, bytes, 0, size);
            Marshal.FreeHGlobal(ptr);

            return bytes;
        }

        /// <summary>
        /// Serialize the passed items into raw byte data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public byte[] ArrayToBytes<T>(T[] items)
            where T: struct
        {
            int structSize;
            byte[] bytes;

            structSize = Marshal.SizeOf<T>();
            var length = items.Length; 
            bytes = new byte[structSize * length]; 

            for (var i = 0; i < length; i++)
            {
                var itemBytes = ToBytes<T>(items[i]);
                Buffer.BlockCopy(itemBytes, 0, bytes, structSize * i, structSize);
            }

            return bytes;
        }

        /// <summary>
        /// Determine if the passed generic type is a Value Type or an array of a Value Type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>True if passed generic type is Value Type or a Value Type Array.</returns>
        private static bool IsValueType<T>()
        {
            var typeOfGeneric = typeof(T);
            return typeOfGeneric.IsValueType || typeOfGeneric.GetElementType().IsValueType;
        }
    }
}
