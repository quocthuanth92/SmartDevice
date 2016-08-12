using ProgramAnalysis.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramAnalysis.Helper
{
    public static class Helper
	{
        public static object GetPropertyValue(object ob, string propertyName)
        {
            return ob.GetType().GetProperties().Single(pi => pi.Name == propertyName).GetValue(ob, null);
        }
        public static ModelMess ParseMessToModel(List<byte> data)
        {
            ModelMess modelMess = new ModelMess();
            if (data != null)
            {
                List<byte> value = new List<byte>();
                if (data[0] == 0x01)
                {
                    modelMess.CommandType = data.Skip(0).Take(3).ToArray();
                    modelMess.CommandId = data.Skip(3).Take(6).ToArray();
                    modelMess.CommandAction = data.Skip(9).Take(3).ToArray();
                    value = data.Skip(12).ToList();
                }
                else if (data[0] == 0x02)
                {
                    modelMess.CommandId = data.Skip(0).Take(6).ToArray();
                    modelMess.CommandAction = data.Skip(6).Take(3).ToArray();
                    value = data.Skip(9).ToList();
                }
                else if (data[0] == 0x03)
                {
                    modelMess.CommandAction = data.Skip(0).Take(3).ToArray();
                    value = data.Skip(3).ToList();
                }

                if (value != null)
                {
                    if (value[0] == 0x05)
                    {
                        modelMess.Status = value.Skip(0).Take(3).ToArray();
                        if (value.Count >= 6)
                        {
                            if (value[3] == 0x24)
                            {
                                modelMess.Length = value.Skip(3).Take(3).ToArray();
                            }

                        }
                        if (value.Count >= 9)
                        {
                            if (value[6] == 0x06)
                            {
                                modelMess.States = value.Skip(6).Take(3).ToArray();
                            }
                        }
                        if (value.Count >= 15)
                        {
                            if (value[9] == 0x07)
                            {
                                modelMess.Time = value.Skip(11).Take(4).ToArray();
                            }
                        }
                        if (value.Count >= 40)
                        {
                            if (value[15] == 0x08)
                            {
                                modelMess.GPSByte = value.Skip(17).Take(26).ToArray();
                                modelMess.LatitudeByte = modelMess.GPSByte.Skip(0).Take(9).ToArray();
                                modelMess.LongitudeByte = modelMess.GPSByte.Skip(12).Take(10).ToArray();
                            }
                        }
                        if (value.Count >= 30)
                        {
                            if (value[3] == 0x1F)
                            {
                                if (Helper.ByteArrayCompare(value.Skip(3).Take(3).ToArray(), ConstParam.Value))
                                {
                                    modelMess.GPSByte = value.Skip(6).Take(24).ToArray();
                                    modelMess.LatitudeByte = modelMess.GPSByte.Skip(0).Take(9).ToArray();
                                    modelMess.LongitudeByte = modelMess.GPSByte.Skip(12).Take(10).ToArray();
                                }
                            }
                        }
                    }
                    else if (value[0] == 0x24)
                    {
                        modelMess.Sequence = value.Skip(0).Take(3).ToArray();
                    }
                }
            }

            return modelMess;
        }

        public static ThietBiStatusMess ParseMessToValue(ModelMess data, string thietBiId)
        {
            ThietBiStatusMess model = new ThietBiStatusMess();
            try
            {
                model.ThoiGian = DateTime.Now;
                model.ThietBiID = thietBiId;
                if (data.CommandType != null)
                {
                    string hex = BitConverter.ToString(data.CommandType);
                    model.CommandType = hex.Replace("-", " ");
                }
                if (data.CommandId != null)
                {
                    string hex = BitConverter.ToString(data.CommandId);
                    model.CommandId = hex.Replace("-", " ");
                }
                if (data.CommandAction != null)
                {
                    string hex = BitConverter.ToString(data.CommandAction);
                    model.CommandAction = hex.Replace("-", " ");
                }
                if (data.Time != null)
                {
                    model.Time = BitConverter.ToInt32(data.Time, 0);
                }
                if (data.LatitudeByte != null)
                {
                    model.Latitude = Helper.ConvertLatitude(data.LatitudeByte);
                }
                if (data.LongitudeByte != null)
                {
                    model.Longitude = Helper.ConvertLongitude(data.LongitudeByte);
                }


                /*
                 Loại:
                 1 - GPS định kỳ
                 2 - Bảo Dưỡng
                 3 - Di chuyển
                 4 - Khoan
                 5 - Cẩu
                */
                /*
                 Trang Thai:
                 0 - Nổ máy
                 1 - Tắt máy
                */
                if (data.Status.Count() > 0)
                {
                    if (Helper.ByteArrayCompare(data.Status, ConstParam.NoMay))
                    {
                        model.StatusMay = 0;
                    }
                    else if (Helper.ByteArrayCompare(data.Status, ConstParam.TatMay))
                    {
                        model.StatusMay = 1;
                    }
                }


                if (Helper.ByteArrayCompare(data.CommandAction, ConstParam.DinhKyGPS))
                {
                    model.Loai = 1;
                }
                else if (Helper.ByteArrayCompare(data.CommandAction, ConstParam.HoatDong))
                {
                    model.Loai = 100;
                    if (Helper.ByteArrayCompare(data.States, ConstParam.Dang))
                    {
                        model.TrangThai = 1;  //đang hoạt động
                    }
                    else if (Helper.ByteArrayCompare(data.States, ConstParam.Khong))
                    {
                        model.TrangThai = 2;
                    }
                }
                else if (Helper.ByteArrayCompare(data.CommandAction, ConstParam.BaoDuong))
                {
                    model.Loai = 2;
                    if (Helper.ByteArrayCompare(data.States, ConstParam.Dang))
                    {
                        model.TrangThai = 3;  //đang bảo dưỡng
                    }
                    else if (Helper.ByteArrayCompare(data.States, ConstParam.Khong))
                    {
                        model.TrangThai = 4;
                    }
                }
                else if (Helper.ByteArrayCompare(data.CommandAction, ConstParam.DiChuyen))
                {
                    model.Loai = 3;
                    if (Helper.ByteArrayCompare(data.States, ConstParam.Dang))
                    {
                        model.TrangThai = 5;  //đang Di chuyển
                    }
                    else if (Helper.ByteArrayCompare(data.States, ConstParam.Khong))
                    {
                        model.TrangThai = 6;
                    }
                }
                else if (Helper.ByteArrayCompare(data.CommandAction, ConstParam.Khoan))
                {
                    model.Loai = 4;
                    if (Helper.ByteArrayCompare(data.States, ConstParam.Dang))
                    {
                        model.TrangThai = 7;  //đang bảo dưỡng
                    }
                    else if (Helper.ByteArrayCompare(data.States, ConstParam.Khong))
                    {
                        model.TrangThai = 8;
                    }
                }
                else if (Helper.ByteArrayCompare(data.CommandAction, ConstParam.Cau))
                {
                    model.Loai = 5;
                    if (Helper.ByteArrayCompare(data.States, ConstParam.Dang))
                    {
                        model.TrangThai = 9;  //đang bảo dưỡng
                    }
                    else if (Helper.ByteArrayCompare(data.States, ConstParam.Khong))
                    {
                        model.TrangThai = 10;
                    }
                }
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                throw;
            }
            return model;
        }
        public static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            try
            {
                if (a1.Length == a2.Length)
                {
                    for (int i = 0; i < a1.Length; i++)
                        if (a1[i] != a2[i])
                            return false;
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                return false;
                throw;
            }
        }

        public static bool ByteListCompare(List<byte> a1, List<byte> a2)
        {
            try
            {
                if (a1 != null && a2 != null)
                {
                    if (a1.Count == a2.Count)
                    {
                        for (int i = 0; i < a1.Count; i++)
                            if (a1[i] != a2[i])
                                return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                return false;
                throw;
            }
        }

        public static decimal ConvertLatitude(byte[] arrayData)
        {
            decimal result = 0;
            try
            {
                if (arrayData[3] != 0x00)
                {
                    string strData = System.Text.Encoding.UTF8.GetString(arrayData);
                    string strToaDo = strData.Substring(0, 2);
                    string strPhut = strData.Substring(2, 7);
                    strPhut = strPhut.Replace(',', '.');
                    decimal toado = Decimal.Parse(strToaDo);
                    decimal phut = Decimal.Parse(strPhut);
                    result = toado + (phut / 60);
                }
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                return 0;
                throw;
            }
            return result;
        }
        public static decimal ConvertLongitude(byte[] arrayData)
        {
            decimal result = 0;
            try
            {
                if (arrayData[4] != 0x00)
                {
                    string strData = System.Text.Encoding.UTF8.GetString(arrayData);
                    string strToaDo = strData.Substring(0, 3);
                    string strPhut = strData.Substring(3, 7);
                    strPhut = strPhut.Replace(',', '.');
                    decimal toado = Decimal.Parse(strToaDo);
                    decimal phut = Decimal.Parse(strPhut);
                    result = toado + (phut / 60);
                }
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                return 0;
                throw;
            }
            return result;
        }

        public static string ByteToString(byte[] mess)
        {
            string hex = BitConverter.ToString(mess);
            string text = hex.Replace("-", "");
            return text;
        }

        #region MessModel
        public static MessModel ParseToMessModel(List<byte> data)
        {
            MessModel messModel = new MessModel();
            if (data != null)
            {
                if (data[0] == 0x01)
                {
                    messModel.CommandType = data.Skip(0).Take(2).ToList();
                    messModel.Type = data.Skip(2).Take(2).ToList();
                    messModel.Value = data.Skip(2).Take(2).ToList();
                    messModel.GPSByte = data.Skip(4).ToList();
                }
            }
            return messModel;
        }
        #endregion



    }
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }
    }
    public class ImageInfoMark
    {
        public bool IsImgReal { get; set; }
        public int Result { get; set; }
        public double Rate { get; set; }
        public double RunningTime { get; set; }
        public string ErrorDesc { get; set; }

        public ImageInfoMark()
        {
            this.IsImgReal = false;
            this.ErrorDesc = string.Empty;
        }

    }
    public class MatLabConfig
    {
        public MLApp.MLApp MatlabObj = new MLApp.MLApp();
        public string matlabFuncPath { get; set; }
        public string matlabDataPath { get; set; }
        private string BagPath { get; set; }
        private string MeanMhistRGBPath { get; set; }
        private string RealSamplePath { get; set; }
        private string VLFeatLibPath { get; set; }
        private string ProductImagePath { get; set; }

        public MatLabConfig()
        {
            this.matlabFuncPath = Utility.pathMatlab + "AutoEvaluationFunction";
            this.matlabDataPath = Utility.pathMatlab + "AutoEvaluationData";
            this.BagPath = matlabDataPath + "\\Bag.mat";
            this.MeanMhistRGBPath = matlabDataPath + "\\MeanMhistRGB.mat";
            this.RealSamplePath = matlabDataPath + "\\RealImage_Sample.jpg";
            this.VLFeatLibPath = matlabDataPath + "\\VLFeat_Lib\\toolbox\\vl_setup.m";
            this.ProductImagePath = matlabDataPath + "\\SuaHop_Sample.jpg";
            //this.ProductImagePath = matlabDataPath + "\\sanpham1.jpg";

            this.MatlabObj.Execute("clc; clear");
            this.MatlabObj.Execute("cd " + matlabFuncPath);

        }

        public ImageInfoMark ImageReal(string ImagePath)
        {
            ImageInfoMark info = new ImageInfoMark();
            object output;
            object[] result;
            MatlabObj.Feval("CheckRealImage", 4, out output, ImagePath, BagPath, MeanMhistRGBPath, RealSamplePath);
            result = output as object[];

            info.Result = Convert.ToInt16(result[0]);
            if (info.Result == 1)
            {
                info.IsImgReal = true;
            }
            info.Rate = Convert.ToDouble(result[1]);
            info.RunningTime = Convert.ToDouble(result[2]);
            info.ErrorDesc = Convert.ToString(result[3]);
            //Console.WriteLine("CheckRealImage: \n\t Ket Qua That Gia : " + Result + "\n\t Thoi Gian Chay :" + RunningTime + "\n\t Trang Thai Loi : " + ErrorDesc);
            return info;
        }
        public ImageInfoMark ItemExistImage(string ImagePath, string ItemInImage = "")
        {
            ImageInfoMark info = new ImageInfoMark();
            object output;
            object[] result;
            MatlabObj.Feval("CheckProductImage", 4, out output, ImagePath, ProductImagePath);
            result = output as object[];

            info.Result = Convert.ToInt16(result[0]);
            if (info.Result == 1)
            {
                info.IsImgReal = true;
            }
            info.Rate = Convert.ToDouble(result[1]);
            info.RunningTime = Convert.ToDouble(result[2]);
            info.ErrorDesc = Convert.ToString(result[3]);
            //Console.WriteLine("CheckRealImage: \n\t Ket Qua That Gia : " + Result + "\n\t Thoi Gian Chay :" + RunningTime + "\n\t Trang Thai Loi : " + ErrorDesc);
            return info;
        }
    }
}