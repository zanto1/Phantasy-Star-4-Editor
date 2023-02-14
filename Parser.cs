using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS4Editor
{
    public class RomDataParser
    {
        private byte[] Data;
        public RomDataParser(byte[] _data)
        {
            this.Data = _data;
        }


        public int readDataInt(string offset, int length, bool lEndian)
        {
            byte[] buffer = new byte[length];

            buffer = SubData(offset, length);
            System.Text.StringBuilder hexString = new System.Text.StringBuilder(length * 2);

            for (int index = 0; index < length; index++)
            {
                byte byteValue = buffer[index];
                string hexValue = byteValue.ToString("X");
                if (hexValue.Length < 2) { hexValue = "0" + hexValue; }
                if (!lEndian)
                {
                    hexString.Append(hexValue);
                }
                else
                {
                    hexString.Insert(0, hexValue);
                }
            }
            return (int.Parse(hexString.ToString(), System.Globalization.NumberStyles.HexNumber));
        }

        public string readDataString(string offset, int length)
        {
            byte[] buffer = new byte[length];
            //MessageBox.Show(System.Text.Encoding.UTF8.GetString(Data));
            //System.Diagnostics.Debug.WriteLine("DSDSDSSDDSDSDSSDDSDSDSSDDSDSDSSDDSDSDSSDDSDSDSSD");
            buffer = SubData(offset, length);
            /*
            var enc = Encoding.Unicode;
            var chars = enc.GetChars(buffer);
            return new string(chars);
            */
            char[] chars = new char[length];
            int index = 0;
            while (index < length)
            {
                chars[index] = (char)buffer[index];
                index++;
            }


            return (new String(chars, 0, index).Trim());
        }

        public string readDataHex(string offset)
        {
            byte[] buffer = new byte[1];

            buffer = SubData(offset, 1);

            byte byteValue = buffer[0];
            return (byteValue.ToString("X2"));
        }

        public byte[] SubData(string offset, int length)
        {

            int index = int.Parse(offset, System.Globalization.NumberStyles.HexNumber);
            //index -= 1024;
            //index += 512;

            byte[] result = new byte[length];
            Array.Copy(Data, index, result, 0, length);
            return result;
        }

        public string hexAdd(string value1, string value2)
        {
            string r;
            int v1 = int.Parse(value1, System.Globalization.NumberStyles.HexNumber);
            int v2 = int.Parse(value2, System.Globalization.NumberStyles.HexNumber);
            r = (v1 + v2).ToString("X");
            return r;
        }

        public string hexSub(string value1, string value2)
        {
            string r;
            int v1 = int.Parse(value1, System.Globalization.NumberStyles.HexNumber);
            int v2 = int.Parse(value2, System.Globalization.NumberStyles.HexNumber);
            r = (v1 - v2).ToString("X");
            return r;
        }

        

    }
}
