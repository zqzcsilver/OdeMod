using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace OdeMod.Utils
{
    internal class BinaryProcessed
    {
        private Dictionary<Type, Action<BinaryWriter, object, Type>> binaryWriterProcessed;
        private Dictionary<Type, Func<BinaryReader, Type, object>> binaryReaderProcessed;

        public BinaryProcessed()
        {
            binaryWriterProcessed = new Dictionary<Type, Action<BinaryWriter, object, Type>>();
            binaryReaderProcessed = new Dictionary<Type, Func<BinaryReader, Type, object>>();
        }

        public void LoadProcessed()
        {
            AddOrUpdateBinaryWriterProcessed(typeof(bool), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                binaryWriter.Write((bool)o);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(byte), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                binaryWriter.Write((byte)o);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(byte[]), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                var bytes = (byte[])o;
                binaryWriter.Write(bytes.Length);
                binaryWriter.Write(bytes);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(char), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                binaryWriter.Write((char)o);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(char[]), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                var chars = (char[])o;
                binaryWriter.Write(chars.Length);
                binaryWriter.Write(chars);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(decimal), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                binaryWriter.Write((decimal)o);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(double), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                binaryWriter.Write((double)o);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(float), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                binaryWriter.Write((float)o);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(int), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                binaryWriter.Write((int)o);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(long), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                binaryWriter.Write((long)o);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(sbyte), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                binaryWriter.Write((sbyte)o);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(short), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                binaryWriter.Write((short)o);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(string), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                binaryWriter.Write((string)o);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(uint), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                binaryWriter.Write((uint)o);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(ulong), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                binaryWriter.Write((ulong)o);
            });
            AddOrUpdateBinaryWriterProcessed(typeof(ushort), (binaryWriter, o, type) =>
            {
                binaryWriter.Write(type.FullName);
                binaryWriter.Write((ushort)o);
            });

            AddOrUpdateBinaryReaderProcessed(typeof(bool), (binaryReader, type) =>
            {
                return binaryReader.ReadBoolean();
            });
            AddOrUpdateBinaryReaderProcessed(typeof(byte), (binaryReader, type) =>
            {
                return binaryReader.ReadByte();
            });
            AddOrUpdateBinaryReaderProcessed(typeof(byte[]), (binaryReader, type) =>
            {
                int length = binaryReader.ReadInt32();
                return binaryReader.ReadBytes(length);
            });
            AddOrUpdateBinaryReaderProcessed(typeof(char), (binaryReader, type) =>
            {
                return binaryReader.ReadChar(); ;
            });
            AddOrUpdateBinaryReaderProcessed(typeof(char[]), (binaryReader, type) =>
            {
                int length = binaryReader.ReadInt32();
                return binaryReader.ReadChars(length);
            });
            AddOrUpdateBinaryReaderProcessed(typeof(decimal), (binaryReader, type) =>
            {
                return binaryReader.ReadDecimal();
            });
            AddOrUpdateBinaryReaderProcessed(typeof(double), (binaryReader, type) =>
            {
                return binaryReader.ReadDouble();
            });
            AddOrUpdateBinaryReaderProcessed(typeof(float), (binaryReader, type) =>
            {
                return binaryReader.ReadSingle();
            });
            AddOrUpdateBinaryReaderProcessed(typeof(int), (binaryReader, type) =>
            {
                return binaryReader.ReadInt32();
            });
            AddOrUpdateBinaryReaderProcessed(typeof(long), (binaryReader, type) =>
            {
                return binaryReader.ReadInt64();
            });
            AddOrUpdateBinaryReaderProcessed(typeof(sbyte), (binaryReader, type) =>
            {
                return binaryReader.ReadSByte();
            });
            AddOrUpdateBinaryReaderProcessed(typeof(short), (binaryReader, type) =>
            {
                return binaryReader.ReadInt16();
            });
            AddOrUpdateBinaryReaderProcessed(typeof(string), (binaryReader, type) =>
            {
                return binaryReader.ReadString();
            });
            AddOrUpdateBinaryReaderProcessed(typeof(uint), (binaryReader, type) =>
            {
                return binaryReader.ReadUInt32();
            });
            AddOrUpdateBinaryReaderProcessed(typeof(ulong), (binaryReader, type) =>
            {
                return binaryReader.ReadUInt64();
            });
            AddOrUpdateBinaryReaderProcessed(typeof(ushort), (binaryReader, type) =>
            {
                return binaryReader.ReadUInt16();
            });
        }

        public void AddOrUpdateBinaryWriterProcessed(Type key, Action<BinaryWriter, object, Type> processed)
        {
            if (!binaryWriterProcessed.ContainsKey(key))
                binaryWriterProcessed.Add(key, processed);
            else
                binaryWriterProcessed[key] = processed;
        }

        public void AddOrUpdateBinaryReaderProcessed(Type key, Func<BinaryReader, Type, object> processed)
        {
            if (!binaryReaderProcessed.ContainsKey(key))
                binaryReaderProcessed.Add(key, processed);
            else
                binaryReaderProcessed[key] = processed;
        }

        public void Save(BinaryWriter binaryWriter, Type type, object o)
        {
            if (binaryWriterProcessed.ContainsKey(type))
                binaryWriterProcessed[type](binaryWriter, o, type);
            else
            {
                binaryWriter.Write(type.FullName);
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var f in fields)
                {
                    Save(binaryWriter, f.FieldType, f.GetValue(o));
                }
            }
        }

        public void SafeSave(BinaryWriter binaryWriter, Type type, object o)
        {
            if (binaryWriterProcessed.ContainsKey(type))
                binaryWriterProcessed[type](binaryWriter, o, type);
            else
            {
                binaryWriter.Write(type.FullName);
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                Save(binaryWriter, typeof(int), fields.Length);
                foreach (var f in fields)
                {
                    Save(binaryWriter, typeof(string), f.Name);
                    Save(binaryWriter, f.FieldType, f.GetValue(o));
                }
            }
        }

        public void Load(BinaryReader binaryReader, Action<Type, object> processed)
        {
            object o = Load(binaryReader);
            processed(o.GetType(), o);
        }

        public object Load(BinaryReader binaryReader)
        {
            object op = null;
            Type type = Type.GetType(binaryReader.ReadString());
            if (binaryReaderProcessed.ContainsKey(type))
                op = binaryReaderProcessed[type](binaryReader, type);
            else
            {
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                op = Activator.CreateInstance(type);
                foreach (var f in fields)
                {
                    f.SetValue(op, Load(binaryReader));
                }
            }
            return op;
        }

        public object SafeLoad(BinaryReader binaryReader)
        {
            object op = null;
            Type type = Type.GetType(binaryReader.ReadString());
            if (binaryReaderProcessed.ContainsKey(type))
                op = binaryReaderProcessed[type](binaryReader, type);
            else
            {
                op = Activator.CreateInstance(type);
                int length = (int)Load(binaryReader);
                Dictionary<string, object> loaders = new Dictionary<string, object>();
                for (int i = 0; i < length; i++)
                {
                    loaders.Add((string)Load(binaryReader), Load(binaryReader));
                }

                var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var field in fields)
                {
                    if (loaders.ContainsKey(field.Name))
                    {
                        field.SetValue(op, loaders[field.Name]);
                    }
                }
            }
            return op;
        }
    }
}