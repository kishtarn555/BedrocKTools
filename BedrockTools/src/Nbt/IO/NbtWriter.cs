using System;
using BedrockTools.Nbt.Elements;

namespace BedrockTools.Nbt.IO {
    public abstract class NbtWriter {
        public abstract void Write(NbtByte nbt);
        public abstract void Write(NbtShort nbt);
        public abstract void Write(NbtInt nbt);
        public abstract void Write(NbtLong nbt);
        public abstract void Write(NbtFloat nbt);
        public abstract void Write(NbtDouble nbt);
        public abstract void Write(NbtString nbt);
        public abstract void Write(NbtCompound nbt);
        public abstract void Write(NbtList nbt);

        public void WriteNbt(NbtElement element) {
            if (element is NbtByte _byte)
                Write(_byte);
            else if (element is NbtShort _short)
                Write(_short);
            else if (element is NbtInt _int)
                Write(_int);
            else if (element is NbtLong _long)
                Write(_long);
            else if (element is NbtFloat _float)
                Write(_float);
            else if (element is NbtDouble _double)
                Write(_double);
            else if (element is NbtString _string)
                Write(_string);
            else if (element is NbtCompound _compound)
                Write(_compound);
            else if (element is NbtList _list)
                Write(_list);
            else
                throw new ArgumentException("Unsupported Nbtelement");
        }

        public abstract void Close();
    }
}
