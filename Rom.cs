using System;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Diagnostics;



namespace PS4Editor
{
    public enum ByteOrder : int
    {
        HighByteFirst = 0,
        LowByteFirst = 1
    }

    class Rom
    {


        public static string endOfNameByte = "F7";
        public static int maxMonsters = 153;
        public static int maxMonsterSkills = 111;
        public static int maxPlayerTechs = 40;
        public static int maxPlayerSkills = 54;
        public static int maxChars = 11;
        public static int maxItems = 160;
        
        private RomDataParser parser;


        public int[] charProfession = new int[maxChars];
        public int[] charStartLevel = new int[maxChars];
        public int[] charStartExp = new int[maxChars];
        public int[] charStartHp = new int[maxChars];
        public int[] charStartTp = new int[maxChars];
        public int[] charStartStr = new int[maxChars];
        public int[] charStartMen = new int[maxChars];
        public int[] charStartAgi = new int[maxChars];
        public int[] charStartDex = new int[maxChars];
        public int[] charPhysDef = new int[maxChars];
        public int[] charEnergyDef = new int[maxChars];
        public int[] charFireDef = new int[maxChars];
        public int[] charGravityDef = new int[maxChars];
        public int[] charWaterDef = new int[maxChars];
        public int[] charAntiEvilDef = new int[maxChars];
        public int[] charElectricDef = new int[maxChars];
        public int[] charHolywordDef = new int[maxChars];
        public int[] charBroseDef = new int[maxChars];
        public int[] charBiologicalDef = new int[maxChars];
        public int[] charPsychicDef = new int[maxChars];
        public int[] charMechanicalDef = new int[maxChars];
        public int[] charEfessDef = new int[maxChars];
        public int[] charDestroyDef = new int[maxChars];
        public int[] charRightHandEq = new int[maxChars];
        public int[] charLeftHandEq = new int[maxChars];
        public int[] charHeadEq = new int[maxChars];
        public int[] charBodyEq = new int[maxChars];
        public int[][] charStartTech = new int[maxChars][];
        public int[][] charStartSkill = new int[maxChars][];
        public int[][] charStartSkillUse = new int[maxChars][];



        private string[] charGrowthOffset = new string[maxChars];
        public int[][] charGrowthHp = new int[maxChars][];
        public int[][] charGrowthTp = new int[maxChars][];
        public int[][] charGrowthStr = new int[maxChars][];
        public int[][] charGrowthMen = new int[maxChars][];
        public int[][] charGrowthAgi = new int[maxChars][];
        public int[][] charGrowthDex = new int[maxChars][];
        public int[][] charGrowthExp = new int[maxChars][];
        public int[][] charGrowthTech = new int[maxChars][];
        public int[][] charGrowthSkill = new int[maxChars][];
        public int[][] charGrowthSk1 = new int[maxChars][];
        public int[][] charGrowthSk2 = new int[maxChars][];
        public int[][] charGrowthSk3 = new int[maxChars][];
        public int[][] charGrowthSk4 = new int[maxChars][];
        public int[][] charGrowthSk5 = new int[maxChars][];
        public int[][] charGrowthSk6 = new int[maxChars][];
        public int[][] charGrowthSk7 = new int[maxChars][];



        public string[] monsterName = new string[maxMonsters];
        public int[] monsterNameSize = new int[maxMonsters];
        public int[] monsterMaxHP = new int[maxMonsters];
        public int[] monsterAtkElem = new int[maxMonsters];
        public int[] monsterAtkStat = new int[maxMonsters];
        public int[] monsterStr = new int[maxMonsters];
        public int[] monsterMen = new int[maxMonsters];
        public int[] monsterAgi = new int[maxMonsters];
        public int[] monsterDex = new int[maxMonsters];
        public int[] monsterAtk = new int[maxMonsters];
        public int[] monsterDef = new int[maxMonsters];
        public int[] monsterMagDef = new int[maxMonsters];
        public int[] monsterPhysDef = new int[maxMonsters];
        public int[] monsterEnergyDef= new int[maxMonsters];
        public int[] monsterFireDef = new int[maxMonsters];
        public int[] monsterGravityDef = new int[maxMonsters];
        public int[] monsterWaterDef = new int[maxMonsters];
        public int[] monsterAntiEvilDef = new int[maxMonsters];
        public int[] monsterElectricDef = new int[maxMonsters];
        public int[] monsterHolywordDef = new int[maxMonsters];
        public int[] monsterBroseDef = new int[maxMonsters];
        public int[] monsterBiologicalDef = new int[maxMonsters];
        public int[] monsterPsychicDef = new int[maxMonsters];
        public int[] monsterMechanicalDef = new int[maxMonsters];
        public int[] monsterEfessDef = new int[maxMonsters];
        public int[] monsterDestroyDef = new int[maxMonsters];
        public int[] monsterZeroByte1 = new int[maxMonsters];
        public int[] monsterZeroByte2 = new int[maxMonsters];
        public int[] monsterAICond1 = new int[maxMonsters];
        public int[] monsterAICond2 = new int[maxMonsters];
        public int[] monsterAICond3 = new int[maxMonsters];
        public int[] monsterAICond4 = new int[maxMonsters];
        public int[] monsterAISkill1 = new int[maxMonsters];
        public int[] monsterAISkill2 = new int[maxMonsters];
        public int[] monsterAISkill3 = new int[maxMonsters];
        public int[] monsterAISkill4 = new int[maxMonsters];
        public int[] monsterRegSkill1 = new int[maxMonsters];
        public int[] monsterRegSkill2 = new int[maxMonsters];
        public int[] monsterRegSkill3 = new int[maxMonsters];
        public int[] monsterRegSkill4 = new int[maxMonsters];
        public int[] monsterRegSkill5 = new int[maxMonsters];
        public int[] monsterRegSkill6 = new int[maxMonsters];
        public int[] monsterRegSkill7 = new int[maxMonsters];
        public int[] monsterRegSkill8 = new int[maxMonsters];
        public int[] monsterExp = new int[maxMonsters];
        public int[] monsterMeseta = new int[maxMonsters];


        public string[] monsterSkillName = new string[maxMonsterSkills];
        public int[] monsterSkillNameSize = new int[maxMonsterSkills];
        public int[] monsterSkillEffect = new int[maxMonsterSkills];
        public int[] monsterSkillOffense = new int[maxMonsterSkills];
        public int[] monsterSkillTarget = new int[maxMonsterSkills];
        public int[] monsterSkillPower = new int[maxMonsterSkills];
        public int[] monsterSkillDefense = new int[maxMonsterSkills];
        public int[] monsterSkillElement = new int[maxMonsterSkills];
        public int[] monsterSkillZeroByte1 = new int[maxMonsterSkills];
        public int[] monsterSkillZeroByte2 = new int[maxMonsterSkills];

        public string[] playerTechName = new string[maxPlayerTechs + maxPlayerSkills];
        public int[] playerTechNameSize = new int[maxPlayerTechs + maxPlayerSkills];
        public int[] playerTechEffect = new int[maxPlayerTechs + maxPlayerSkills];
        public int[] playerTechTPCost = new int[maxPlayerTechs + maxPlayerSkills];
        public int[] playerTechTarget = new int[maxPlayerTechs + maxPlayerSkills];
        public int[] playerTechPower = new int[maxPlayerTechs + maxPlayerSkills];
        public int[] playerTechDefense = new int[maxPlayerTechs + maxPlayerSkills];
        public int[] playerTechElement = new int[maxPlayerTechs + maxPlayerSkills];
        public int[] playerTechZeroByte1 = new int[maxPlayerTechs + maxPlayerSkills];
        public int[] playerTechZeroByte2 = new int[maxPlayerTechs + maxPlayerSkills];

        public string[] itemName = new string[maxItems];
        public int[] itemNameSize = new int[maxItems];
        

        private Dictionary<string, string> characterMap = new Dictionary<string, string>();
        private byte[] Data;
        private bool _header;

        public bool Header
        {
            get
            {
                return _header;
            }
        }

        public Rom(string path)
        {


            this.Data = File.ReadAllBytes(path);

            parser = new RomDataParser(this.Data);

            int remainder = this.Data.Length & 0x7FFF;
            this._header = remainder == 0x200;

            charGrowthOffset[0] = "2856b0";
            charGrowthOffset[1] = "285F1C";
            charGrowthOffset[2] = "286704";
            charGrowthOffset[3] = "286F70";
            charGrowthOffset[4] = "28767C";
            charGrowthOffset[5] = "287E7A";
            charGrowthOffset[6] = "2886E6";
            charGrowthOffset[7] = "288E60";
            charGrowthOffset[8] = "28952A";
            charGrowthOffset[9] = "289B86";
            charGrowthOffset[10] = "28A1B6";


            for(int i=0; i<maxChars; i++) {
                charStartTech[i] = new int[16];
                charStartSkill[i] = new int[8];
                charStartSkillUse[i] = new int[8];
            }
            
            setupCharacterMap();
            loadMonsterNames();
            loadMonsterSkillNames();
            loadPlayerSkillNames();
            loadItemNames();
            loadPlayerTechData();
            loadCharacterData();
            loadMonsterData();
            loadMonsterSkillData();
        }


        /*****************************************************
         * 
         * SubData
         * 
        *****************************************************/

        public byte[] SubData(string offset, int length)
        {

            int index = int.Parse(offset, System.Globalization.NumberStyles.HexNumber);
            //index -= 1024;
            //index += 512;

            byte[] result = new byte[length];
            Array.Copy(Data, index, result, 0, length);
            return result;
        }


        /*****************************************************
         * 
         * readDataInt
         * 
        *****************************************************/

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


        /*****************************************************
         * 
         * readDataByte
         * 
        *****************************************************/
        public string readDataByte(string offset, int length, bool lEndian)
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
            return (hexString.ToString());
        }


        /*****************************************************
         * 
         * writeDataByte
         * 
        *****************************************************/
        public void writeDataByte(string value, string offset, int length, bool lEndian)
        {
            length *= 2;

            String hexString = value;

            int index = int.Parse(offset, System.Globalization.NumberStyles.HexNumber);
            //index -= 1024;
            //index += 512;

            if (!lEndian)
            {
                for (int i = 0; i < length; i += 2)
                {
                    String substring = hexString.Substring(i, 2);
                    byte b = Convert.ToByte(substring, 16);
                    Data[index + (i / 2)] = b;
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder("0000");
                for (int i = 0; i < length; i += 2)
                {
                    String substring = hexString.Substring(i, 2);
                    byte b = Convert.ToByte(substring, 16);
                    Data[index + ((length - i - 2) / 2)] = b;
                    sb[((length - i - 2) / 2)] = substring[0];
                    sb[((length - i - 2) / 2) + 1] = substring[1];

                }
            }
        }


        /*****************************************************
         * 
         * writeDataInt
         * 
        *****************************************************/
        public void writeDataInt(int value, string offset, int length, bool lEndian)
        {
            length *= 2;

            String hexString = value.ToString("X" + (length).ToString());

            int index = int.Parse(offset, System.Globalization.NumberStyles.HexNumber);
            //index -= 1024;
            //index += 512;

            if (!lEndian)
            {
                for (int i = 0; i < length; i += 2)
                {
                    String substring = hexString.Substring(i, 2);
                    byte b = Convert.ToByte(substring, 16);
                    Data[index + (i / 2)] = b;
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder("0000");
                for (int i = 0; i < length; i += 2)
                {
                    String substring = hexString.Substring(i, 2);
                    byte b = Convert.ToByte(substring, 16);
                    Data[index + ((length - i - 2) / 2)] = b;
                    sb[((length - i - 2) / 2)] = substring[0];
                    sb[((length - i - 2) / 2) + 1] = substring[1];

                }
            }
        }



        /*****************************************************
         * 
         * SaveData
         * 
        *****************************************************/

        public void SaveData(string ROMname)
        {
            saveMonsterData();
            saveMonsterSkillData();
            savePlayerTechData();
            File.WriteAllBytes(ROMname, Data);
        }



        /*****************************************************
         * 
         * hexAdd
         * 
        *****************************************************/
        public string hexAdd(string value1, string value2)
        {

            string r;
            int v1 = int.Parse(value1, System.Globalization.NumberStyles.HexNumber);
            int v2 = int.Parse(value2, System.Globalization.NumberStyles.HexNumber);
            r = (v1 + v2).ToString("X");
            return r;
        }



        /*****************************************************
         * 
         * hexSub
         * 
        *****************************************************/

        public string hexSub(string value1, string value2)
        {
            string r;
            int v1 = int.Parse(value1, System.Globalization.NumberStyles.HexNumber);
            int v2 = int.Parse(value2, System.Globalization.NumberStyles.HexNumber);
            r = (v1 - v2).ToString("X");
            return r;
        }



        /*****************************************************
         * 
         * hexMult
         * 
        *****************************************************/

        public string hexMult(string value, int num)
        {
            string r;
            int i, total = 0;
            for (i = 0; i < num; i++)
            {
                total += int.Parse(value, System.Globalization.NumberStyles.HexNumber);
            }
            r = total.ToString("X");
            return r;
        }



        /*****************************************************
         * 
         * readDataHex
         * 
        *****************************************************/

        public string readDataHex(string offset)
        {
            byte[] buffer = new byte[1];

            buffer = SubData(offset, 1);

            byte byteValue = buffer[0];
            return (byteValue.ToString("X2"));
        }



        /*****************************************************
         * 
         * readDataString
         * 
        *****************************************************/

        public string readDataString(string offset, int length)
        {
            return parser.readDataString(offset, length);
        }


        public string convertBytesToString(byte[] bytes)
        {
            int length = bytes.Length;
            char[] chars = new char[length];
            int index = 0;
            while (index < length)
            {
                chars[index] = (char)bytes[index];
                index++;
            }


            return (new String(chars, 0, index).Trim());
        }


        /*****************************************************
         * 
         * writeDataString
         * 
        *****************************************************/


        public void writeDataString(string value, string offset, int length)
        {
            //length *= 2;
            //System.Diagnostics.Debug.WriteLine("value: " + value + "  offset: " + offset + "  length: " + length.ToString());

            int index = int.Parse(offset, System.Globalization.NumberStyles.HexNumber);
            //index -= 1024;
            //index += 512;

            char[] chars = value.ToCharArray();
            int length2 = chars.Length;
            //byte[] bytes = new byte[length];
            int i = 0;
            for (; i < length2; i++)
            {
                Data[index + i] = (byte)chars[i];
            }
            char space = ' ';
            for (; i < length; i++)
            {
                Data[index + i] = (byte)space;
            }
        }




        /*****************************************************
         * 
         * setupCharacterMap
         * 
        *****************************************************/
        private void setupCharacterMap() {
            characterMap.Add("00", " "); characterMap.Add("01", "A"); characterMap.Add("02", "B"); characterMap.Add("03", "C"); characterMap.Add("04", "D"); characterMap.Add("05", "E"); characterMap.Add("06", "F"); 
            characterMap.Add("07", "G"); characterMap.Add("08", "H"); characterMap.Add("09", "I"); characterMap.Add("0A", "J"); characterMap.Add("0B", "K"); characterMap.Add("0C", "L"); characterMap.Add("0D", "M"); 
            characterMap.Add("0E", "N"); characterMap.Add("0F", "O"); characterMap.Add("10", "P"); characterMap.Add("11", "Q"); characterMap.Add("12", "R"); characterMap.Add("13", "S"); characterMap.Add("14", "T");
            characterMap.Add("15", "U"); characterMap.Add("16", "V"); characterMap.Add("17", "W"); characterMap.Add("18", "X"); characterMap.Add("19", "Y"); characterMap.Add("1A", "Z"); characterMap.Add("1B", "0"); 
            characterMap.Add("1C", "1"); characterMap.Add("1D", "2"); characterMap.Add("1E", "3"); characterMap.Add("1F", "4"); characterMap.Add("20", "5"); characterMap.Add("21", "6"); characterMap.Add("22", "7");
            characterMap.Add("23", "8"); characterMap.Add("24", "9"); characterMap.Add("25", "0"); characterMap.Add("26", "1"); characterMap.Add("27", "2"); characterMap.Add("28", "3"); characterMap.Add("29", "4"); 
            characterMap.Add("2a", "5"); characterMap.Add("2b", "6"); characterMap.Add("2c", "7"); characterMap.Add("2d", "8"); characterMap.Add("2e", "9"); characterMap.Add("2f", "."); characterMap.Add("30", "o");
            characterMap.Add("31", "-"); characterMap.Add("32", "!"); characterMap.Add("33", "?"); characterMap.Add("39", "a"); characterMap.Add("3A", "b"); characterMap.Add("3B", "c"); characterMap.Add("3C", "d");
            characterMap.Add("3D", "e"); characterMap.Add("3E", "f"); characterMap.Add("3F", "g"); characterMap.Add("40", "h"); characterMap.Add("41", "i"); characterMap.Add("42", "j"); characterMap.Add("43", "k"); 
            characterMap.Add("44", "l"); characterMap.Add("45", "m"); characterMap.Add("46", "n"); characterMap.Add("47", "o"); characterMap.Add("48", "p"); characterMap.Add("49", "q"); characterMap.Add("4A", "r");
            characterMap.Add("4B", "s"); characterMap.Add("4C", "t"); characterMap.Add("4D", "u"); characterMap.Add("4E", "v"); characterMap.Add("4F", "w"); characterMap.Add("50", "x"); characterMap.Add("51", "y");
            characterMap.Add("52", "z"); characterMap.Add("53", "."); characterMap.Add("54", "'"); characterMap.Add("55", ","); characterMap.Add("56", " "); characterMap.Add("57", "·"); characterMap.Add("58", " "); 
            characterMap.Add("59", " ");
        }

        /*****************************************************
         * 
         * getCharacter
         * 
        *****************************************************/
        private string getCharacter(string hex)
        {
            string result;
            if (characterMap.TryGetValue(hex, out result))
            {
                return result;
            }
            else
            {
                return " ";
            }

        }



        /*****************************************************
         * 
         * buildString
         * 
        *****************************************************/

        public string buildString(string bytes)
        {
            string auxbyte, result = "", character;
            //bytes = littleEndian(bytes);
            int i;
            for (i = 0; i < bytes.Length; i += 2)
            {
                auxbyte = bytes.Substring(i, 2);
                if (auxbyte == endOfNameByte)
                    break;
                character = getCharacter(auxbyte);
                result += character;
            }
            return result;
        }


        /*****************************************************
         * 
         * loadMonsterNames
         * 
        *****************************************************/

        private void loadMonsterNames()
        {
            string startOffset = "280D42";
            string offset = startOffset;
            string _byte, bytes;
            int nameSize;
            for (var i = 0; i < maxMonsters; i += 1)
            {
                bytes = "";
                nameSize = 0;
                _byte = readDataByte(offset, 1, false);
                do
                {
                    bytes += _byte;
                    nameSize += 1;
                    offset = hexAdd(offset, "01");
                    _byte = readDataByte(offset, 1, false);

                } while (_byte != "FF");
                offset = hexAdd(offset, "01");
                monsterNameSize[i] = nameSize;
                monsterName[i] = buildString(bytes);
            }
        }







        /*****************************************************
         * 
         * loadCharacterData
         * 
        *****************************************************/

        private void loadCharacterData()
        {
            string startOffset = "2a8aca";
            string offset;
            int dataSize;


            for (var i = 0; i < maxChars; i += 1)
            {
                offset = hexAdd(startOffset, this.hexMult("42", i));    // 42 is 66 in hex

                // first byte is skipped
                dataSize = 1;
                offset = hexAdd(offset, dataSize.ToString());

                dataSize = 1;
                charProfession[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                // skip this byte 
                offset = hexAdd(offset, dataSize.ToString());

                charStartLevel[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                dataSize = 4;
                charStartExp[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                dataSize = 2;
                charStartHp[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charStartTp[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                dataSize = 1;
                charStartStr[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charStartMen[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charStartAgi[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charStartDex[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());


                charPhysDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charEnergyDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charFireDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charGravityDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charWaterDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charAntiEvilDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charElectricDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charHolywordDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charBroseDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charBiologicalDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charPsychicDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charMechanicalDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charEfessDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charDestroyDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                
                charRightHandEq[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charLeftHandEq[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charHeadEq[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                charBodyEq[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());


                for(var j=0; j<16;j++) {
                    charStartTech[i][j] = readDataInt(offset, dataSize, false);
                    offset = hexAdd(offset, dataSize.ToString());
                }

                for(var j=0; j<8;j++) {
                    charStartSkill[i][j] = readDataInt(offset, dataSize, false);
                    offset = hexAdd(offset, dataSize.ToString());
                }

                for(var j=0; j<8;j++) {
                    charStartSkillUse[i][j] = readDataInt(offset, dataSize, false);
                    offset = hexAdd(offset, dataSize.ToString());
                }



                if (charStartLevel[i] < 99)
                {

                    
                    int tableHeight = 99 - charStartLevel[i];
                    charGrowthHp[i] = new int[tableHeight];
                    charGrowthTp[i] = new int[tableHeight];
                    charGrowthStr[i] = new int[tableHeight];
                    charGrowthMen[i] = new int[tableHeight];
                    charGrowthAgi[i] = new int[tableHeight];
                    charGrowthDex[i] = new int[tableHeight];
                    charGrowthExp[i] = new int[tableHeight];
                    charGrowthTech[i] = new int[tableHeight];
                    charGrowthSkill[i] = new int[tableHeight];
                    charGrowthSk1[i] = new int[tableHeight];
                    charGrowthSk2[i] = new int[tableHeight];
                    charGrowthSk3[i] = new int[tableHeight];
                    charGrowthSk4[i] = new int[tableHeight];
                    charGrowthSk5[i] = new int[tableHeight];
                    charGrowthSk6[i] = new int[tableHeight];
                    charGrowthSk7[i] = new int[tableHeight];


                    string startGrowthOffsetGrowth = charGrowthOffset[i];
                    string growthOffset;

                    //MessageBox.Show("Character " + i.ToString() + "  >  " + charGrowthOffset[i].ToString() + "    start: " + charStartLevel[i].ToString() + "   height: " + tableHeight.ToString());

                    for (var j = 0; j < tableHeight; j += 1)
                    {
                        growthOffset = hexAdd(startGrowthOffsetGrowth, this.hexMult("16", j));


                        dataSize = 4;
                        charGrowthExp[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());


                        dataSize = 2;
                        charGrowthHp[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());
                        charGrowthTp[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());

                        System.Diagnostics.Debug.WriteLine("   char " + i.ToString() + "  >  HP in level " + (charStartLevel[i] + j + 1).ToString() + " >> " + charGrowthHp[i][j].ToString());

                        dataSize = 1;
                        charGrowthStr[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());
                        charGrowthMen[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());
                        charGrowthAgi[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());
                        charGrowthDex[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());

                        charGrowthTech[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());
                        charGrowthSkill[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());

                        charGrowthSk1[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());
                        charGrowthSk2[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());
                        charGrowthSk3[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());
                        charGrowthSk4[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());
                        charGrowthSk5[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());
                        charGrowthSk6[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());
                        charGrowthSk7[i][j] = readDataInt(growthOffset, dataSize, false);
                        growthOffset = hexAdd(growthOffset, dataSize.ToString());
                    }


                }
            }
        }







      




        /*****************************************************
         * 
         * loadMonsterData
         * 
        *****************************************************/

        private void loadMonsterData()
        {
            string startOffset = "2816bc";
            string offset;
            int dataSize;


            for (var i = 0; i < maxMonsters; i += 1)
            {
                offset = hexAdd(startOffset, this.hexMult("30", i));    // 30 is 48 in hex
                dataSize = 2;
                monsterMaxHP[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                dataSize = 1;
                monsterAtkElem[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterAtkStat[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterStr[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterMen[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterAgi[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterDex[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                dataSize = 2;
                monsterAtk[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                dataSize = 1;
                monsterDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterMagDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                
                monsterPhysDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterEnergyDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterFireDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterGravityDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterWaterDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterAntiEvilDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterElectricDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterHolywordDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterBroseDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterBiologicalDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterPsychicDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterMechanicalDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterEfessDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterDestroyDef[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                monsterZeroByte1[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterZeroByte2[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                monsterAICond1[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterAICond2[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterAICond3[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterAICond4[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterAISkill1[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterAISkill2[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterAISkill3[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterAISkill4[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                monsterRegSkill1[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterRegSkill2[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterRegSkill3[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterRegSkill4[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterRegSkill5[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterRegSkill6[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterRegSkill7[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterRegSkill8[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                
                dataSize = 2;
                monsterExp[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterMeseta[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

            }
        }



        /*****************************************************
         * 
         * loadMonsterSkillNames
         * 
        *****************************************************/

        private void loadMonsterSkillNames()
        {
            string startOffset = "2812EC";
            string offset = startOffset;
            string _byte, bytes;
            int nameSize;
            for (var i = 0; i < maxMonsterSkills; i += 1)
            {
                bytes = "";
                nameSize = 0;
                _byte = readDataByte(offset, 1, false);
                do
                {
                    bytes += _byte;
                    nameSize += 1;
                    offset = hexAdd(offset, "01");
                    _byte = readDataByte(offset, 1, false);

                } while (_byte != "FF");
                offset = hexAdd(offset, "01");
                monsterSkillNameSize[i] = nameSize;
                monsterSkillName[i] = buildString(bytes);
            }
        }




        /*****************************************************
         * 
         * saveMonsterData
         * 
        *****************************************************/

        private void saveMonsterData()
        {
            string startOffset = "2816bc";
            string offset;
            int dataSize;
            for (var i = 0; i < maxMonsters; i += 1)
            {
                offset = hexAdd(startOffset, this.hexMult("30", i));    // 30 is 48 in hex
                
                dataSize = 2;
                writeDataInt(monsterMaxHP[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                dataSize = 1;
                writeDataInt(monsterAtkElem[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterAtkStat[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterStr[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterMen[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterAgi[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterDex[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                dataSize = 2;
                writeDataInt(monsterAtk[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                dataSize = 1;
                writeDataInt(monsterDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterMagDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                writeDataInt(monsterPhysDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterEnergyDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterFireDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterGravityDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterWaterDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterAntiEvilDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterElectricDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterHolywordDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterBroseDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterBiologicalDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterPsychicDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterMechanicalDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterEfessDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterDestroyDef[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                writeDataInt(monsterZeroByte1[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterZeroByte2[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                writeDataInt(monsterAICond1[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterAICond2[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterAICond3[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterAICond4[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterAISkill1[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterAISkill2[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterAISkill3[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterAISkill4[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                writeDataInt(monsterRegSkill1[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterRegSkill2[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterRegSkill3[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterRegSkill4[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterRegSkill5[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterRegSkill6[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterRegSkill7[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterRegSkill8[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

                dataSize = 2;
                writeDataInt(monsterExp[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterMeseta[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

            }
        }






        /*****************************************************
         * 
         * loadMonsterSkillData
         * 
        *****************************************************/

        private void loadMonsterSkillData()
        {
            string startOffset = "283374";
            string offset;
            int dataSize;


            for (var i = 0; i < maxMonsterSkills; i += 1)
            {
                offset = hexAdd(startOffset, this.hexMult("8", i));    // 8 is 8 in hex
                dataSize = 1;
                monsterSkillEffect[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterSkillOffense[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterSkillTarget[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterSkillPower[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterSkillDefense[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterSkillElement[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterSkillZeroByte1[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                monsterSkillZeroByte2[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

            }
        }



        /*****************************************************
         * 
         * saveMonsterSkillData
         * 
        *****************************************************/

        private void saveMonsterSkillData()
        {
            string startOffset = "283374";
            string offset;
            int dataSize;
            for (var i = 0; i < maxMonsterSkills; i += 1)
            {
                offset = hexAdd(startOffset, this.hexMult("8", i));    // 30 is 48 in hex

                dataSize = 1;
                writeDataInt(monsterSkillEffect[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterSkillOffense[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterSkillTarget[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterSkillPower[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterSkillDefense[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterSkillElement[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterSkillZeroByte1[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(monsterSkillZeroByte2[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

            }
        }





        /*****************************************************
         * 
         * loadPlayerSkillNames
         * 
        *****************************************************/

        private void loadPlayerSkillNames()
        {
            string startOffset = "2AB622";
            string offset = startOffset;
            string _byte, bytes;
            int nameSize;
            for (var i = 0; i < maxPlayerTechs + maxPlayerSkills; i += 1)
            {
                bytes = "";
                nameSize = 0;
                _byte = readDataByte(offset, 1, false);
                do
                {
                    bytes += _byte;
                    nameSize += 1;
                    offset = hexAdd(offset, "01");
                    _byte = readDataByte(offset, 1, false);

                } while (_byte != "FE");
                offset = hexAdd(offset, "01");
                playerTechNameSize[i] = nameSize;
                playerTechName[i] = buildString(bytes);
            }
        }






        /*****************************************************
         * 
         * loadPlayerTechData
         * 
        *****************************************************/

        private void loadPlayerTechData()
        {
            string startOffset = "2a9be8";
            string offset;
            int dataSize;


            for (var i = 0; i < maxPlayerTechs + maxPlayerSkills; i += 1)
            {
                offset = hexAdd(startOffset, this.hexMult("8", i));    // 8 is 8 in hex
                dataSize = 1;
                playerTechEffect[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                playerTechTPCost[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                playerTechTarget[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                playerTechPower[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                playerTechDefense[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                playerTechElement[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                playerTechZeroByte1[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                playerTechZeroByte2[i] = readDataInt(offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

            }
        }



        /*****************************************************
         * 
         * savePlayerTechData
         * 
        *****************************************************/

        private void savePlayerTechData()
        {
            string startOffset = "2a9be8";
            string offset;
            int dataSize;
            for (var i = 0; i < maxPlayerTechs + maxPlayerSkills; i += 1)
            {
                offset = hexAdd(startOffset, this.hexMult("8", i));    // 30 is 48 in hex

                dataSize = 1;
                writeDataInt(playerTechEffect[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(playerTechTPCost[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(playerTechTarget[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(playerTechPower[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(playerTechDefense[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(playerTechElement[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(playerTechZeroByte1[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());
                writeDataInt(playerTechZeroByte2[i], offset, dataSize, false);
                offset = hexAdd(offset, dataSize.ToString());

            }
        }






        public string getByteStr(int monsterIndex)
        {
            string startOffset = "059000";
            int dataSize = 8;
            string offset, bytes;

            offset = hexAdd(startOffset, this.hexMult("20", monsterIndex));    // 20 is 32 in hex
            offset = hexAdd(offset, dataSize.ToString());
            bytes = readDataByte(offset, 24, false);

            return bytes;
        }









        /*****************************************************
         * 
         * loadMonsterSkillNames
         * 
        *****************************************************/

        private void loadItemNames()
        {
            string startOffset = "2AAFB4";
            string offset = startOffset;
            string _byte, bytes;
            int nameSize;
            for (var i = 0; i < maxItems; i += 1)
            {
                bytes = "";
                nameSize = 0;
                _byte = readDataByte(offset, 1, false);
                do
                {
                    bytes += _byte;
                    nameSize += 1;
                    offset = hexAdd(offset, "01");
                    _byte = readDataByte(offset, 1, false);

                } while (_byte != "FE");
                offset = hexAdd(offset, "01");
                itemNameSize[i] = nameSize;
                itemName[i] = buildString(bytes);
            }
        }

        

        

        /*****************************************************
         * 
         * getRealOffset
         * 
        *****************************************************/

        private string getRealOffset(string offset)
        {
            int v1 = int.Parse(offset, System.Globalization.NumberStyles.HexNumber);
            v1 = v1 & 0x0FFFFF;
            offset = v1.ToString("X");

            return offset;

        }



        /*****************************************************
         * 
         * littleEndian
         * 
        *****************************************************/
        static string littleEndian(string num)
        {
            string newnum = "";
            if (num.Length % 2 == 1) 
            {
                num = "0" + num;
            }
            for (var i = 0; i < num.Length; i += 2)
            {
                newnum = num.Substring(i, 2) + newnum;
            }
            return newnum;
        }



        /*****************************************************
         * 
         * hex2bin
         * 
        *****************************************************/

        static string hex2bin(string hexstring)
        {
            string res = "";
            res = Convert.ToString(Convert.ToInt32(hexstring, 16), 2);
            int dif = 8 - res.Length;

            for (var i = 0; i < dif; i++)
            {
                res = "0" + res;
            }
            return res;
        }

        static string hex2bin(string hexstring, int bytes)
        {
            string res = "";
            res = Convert.ToString(Convert.ToInt32(hexstring, 16), 2);
            int dif = (8 * bytes) - res.Length;

            for (var i = 0; i < dif; i++)
            {
                res = "0" + res;
            }
            return res;
        }



        /*****************************************************
         * 
         * bin2hex
         * 
        *****************************************************/

        static string bin2hex(string binstring)
        {
            string res = Convert.ToInt32(binstring, 2).ToString("X");

            if (res.Length % 2 == 1)
            {
                res = "0" + res;
            }
            return res;
        }
    }
}
