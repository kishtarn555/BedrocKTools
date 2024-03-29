﻿namespace BedrockTools.Nbt {
    public enum NbtTag : byte {
        TAG_End = 0x00,
        TAG_Byte = 0x01,
        TAG_Short = 0x02,
        TAG_Int = 0x03,
        TAG_Long = 0x04,
        TAG_Float = 0x05,
        TAG_Double = 0x06,
        TAG_Byte_Array = 0x07,
        TAG_String = 0x08,
        TAG_List = 0x09,
        TAG_Compound = 0x0A,
        TAG_Int_Array = 0x0B,
        TAG_Long_Array = 0x0C
    }
}

