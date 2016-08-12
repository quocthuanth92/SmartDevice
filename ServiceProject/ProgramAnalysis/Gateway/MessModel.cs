using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramAnalysis.Gateway
{
    public class MessModel
    {
        public List<byte> CommandType { get; set; }
        public List<byte> Type { get; set; }
        public List<byte> Value { get; set; }
        public List<byte> GPSByte { get; set; }

        public MessModel()
        {
            this.CommandType = null;
            this.Type = null;
            this.GPSByte = null;
        }

        #region Function 

        public MessModelValue ToModelValue(){
            MessModelValue result = new MessModelValue();
            if (Helper.Helper.ByteListCompare(this.CommandType, ConstParam.CmdTypeOnOff))
            {

            }
            else if (Helper.Helper.ByteListCompare(this.CommandType, ConstParam.CmdTypeAdjust))
            {

            }
            return result;
        }

        public string ToStringLogHex()
        {
            string result = "";
            if (this.CommandType != null)
            {
                string hex = BitConverter.ToString(this.CommandType.ToArray());
                string text = hex.Replace("-", "");
                result += "     CommandType:" + text;
            }
            if (this.Type != null)
            {
                string hex = BitConverter.ToString(this.Type.ToArray());
                string text = hex.Replace("-", "");
                result += "     Type:" + text;
            }
            if (this.GPSByte != null)
            {
                string hex = BitConverter.ToString(this.GPSByte.ToArray());
                string text = hex.Replace("-", "");
                result += "     GPSByte:" + text;
            }
            return result;
        }
        #endregion
    }

    public class MessModelValue {
        public string CommandType { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }
    }

    public static class ConstParam
    {
        #region CommandType
        public static List<byte> CmdTypeOnOff = new List<byte>() { 0x01, 0x0A };
        public static List<byte> CmdTypeAdjust = new List<byte>() { 0x01, 0x0B };
        #endregion

        #region Type
        //public static List<byte> On = new List<byte>() { 0x02, 0x00 };
        //public static List<byte> Off = new List<byte>() { 0x02, 0x01 };
        #endregion

        #region Value
        public static List<byte> On = new List<byte>() { 0x02, 0x00 };
        public static List<byte> Off = new List<byte>() { 0x02, 0x01 };
        #endregion
        public enum TypeTopic
        {
            Periodic,
            Action,
            Line
        }





        public static byte[] DinhKyGPS = new byte[] { 0x03, 0x01, 0x02 };
        public static byte[] BaoDuong = new byte[] { 0x03, 0x01, 0x0A };
        public static byte[] DiChuyen = new byte[] { 0x03, 0x01, 0x0B };
        public static byte[] Khoan = new byte[] { 0x03, 0x01, 0x0C };
        public static byte[] Cau = new byte[] { 0x03, 0x01, 0x0D };
        public static byte[] HoatDong = new byte[] { 0x03, 0x01, 0x0E };
        public static byte[] NoMay = new byte[] { 0x05, 0x01, 0x00 };
        public static byte[] TatMay = new byte[] { 0x05, 0x01, 0x01 };

        public static byte[] Dang = new byte[] { 0x06, 0x01, 0x00 };
        public static byte[] Khong = new byte[] { 0x06, 0x01, 0x01 };

        public static byte[] DangBaoDuong = new byte[] { 0x06, 0x01, 0x00 };
        public static byte[] KoBaoDuong = new byte[] { 0x06, 0x01, 0x01 };
        public static byte[] DangDiChuyen = new byte[] { 0x07, 0x01, 0x00 };
        public static byte[] KoDiChuyen = new byte[] { 0x07, 0x01, 0x01 };
        public static byte[] DangKhoan = new byte[] { 0x08, 0x01, 0x00 };
        public static byte[] KoKhoan = new byte[] { 0x08, 0x01, 0x01 };
        public static byte[] DangCau = new byte[] { 0x09, 0x01, 0x00 };
        public static byte[] KoCau = new byte[] { 0x09, 0x01, 0x01 };

        public static byte[] Value = new byte[] { 0x1F, 0x21, 0x18 };

    }
}