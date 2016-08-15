using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;
using System.Web;
using ProgramAnalysis.Helper;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ProgramAnalysis.Gateway
{
    public class Gateway
    {
        public string clientID = "0000000AAAAAAAA";
        public MqttClient client;
        public Timer TimerTick;
        public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                string log = Helper.Helper.ByteToString(e.Message);
                string clientID = (string)Helper.Helper.GetPropertyValue(sender, "ClientId");
                bool isConnected = (bool)Helper.Helper.GetPropertyValue(sender, "IsConnected");

                if(e.Topic == ConstParam.PrefixTopic.Ping.ToString())
                {
                    CustomLog.LogPing(Encoding.UTF8.GetString(e.Message));
                }
                else
                {
                    List<string> topicList = e.Topic.Split(new char[] { '/' }).ToList();
                    if (topicList.Count >= 3)
                    {
                        if (topicList[2].ToString() == ConstParam.TypeTopic.Periodic.ToString())
                        {
                            string houseCode = topicList[0].ToString();
                            string device = topicList[1].ToString();
                            MessModel messModel = Helper.Helper.ParseToMessModel(e.Message.ToList());
                        }
                        else if (topicList[2].ToString() == ConstParam.TypeTopic.Action.ToString())
                        {

                        }
                    }
                    CustomLog.LogDevice(log);

                }
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                throw;
            }

        }

        public void client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            string clientID = (string)Helper.Helper.GetPropertyValue(sender, "ClientId");
            string result = "MqttMsgUnsubscribed------ ClientID: " + clientID + " --- MessageId: " + e.MessageId;
            CustomLog.LogError(result);
        }

        public void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            string clientID = (string)Helper.Helper.GetPropertyValue(sender, "ClientId");
            string result = "MqttMsgSubscribed----- ClientID: " + clientID + " --- MessageId: " + e.MessageId + " --- GrantedQoSLevels: " + Encoding.UTF8.GetString(e.GrantedQoSLevels);
            CustomLog.LogError(result);
        }

        public void ConfigConnect()
        {
            this.client = new MqttClient(IPAddress.Parse("45.117.80.39"));
            this.client.Connect(clientID);
            CustomLog.LogError("connect thanh cong");
            string[] topic = { "#", "Test/#" };

            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE };
            this.client.Subscribe(topic, qosLevels);

            this.client.MqttMsgPublishReceived += this.client_MqttMsgPublishReceived;
            this.client.MqttMsgSubscribed += this.client_MqttMsgSubscribed;
            //gateway.client.MqttMsgUnsubscribed += gateway.client_MqttMsgUnsubscribed;
        }


        public Gateway()
        {
            int interval = 60000;
            if(!string.IsNullOrEmpty(ConfigurationManager.AppSettings["TimePingInterval"].ToString())){
                Int32.Parse(ConfigurationManager.AppSettings["TimePingInterval"].ToString());
            }
            this.TimerTick = new Timer();
            this.TimerTick.Interval = interval;
            this.TimerTick.Elapsed += new ElapsedEventHandler(Time_Elapsed);
            this.TimerTick.AutoReset = true;
        }
        public void Time_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (this.client.IsConnected)
                {
                    byte[] ping = new byte[] { 0x03, 0x01, 0x01 };
                    this.client.Publish(ConstParam.PrefixTopic.Ping.ToString(), Encoding.UTF8.GetBytes("ping"));
                }
                else
                {
                    #region Config
                    this.ConfigConnect()
                    #endregion
                }
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                throw;
            }
        }
    }

    public class ModelMess
    {
        public byte[] CommandType { get; set; }
        public byte[] CommandId { get; set; }
        public byte[] CommandAction { get; set; }
        public byte[] Status { get; set; }
        public byte[] States { get; set; }
        public byte[] Time { get; set; }
        public byte[] Sequence { get; set; }
        public byte[] GPSByte { get; set; }
        public byte[] LatitudeByte { get; set; }
        public byte[] LongitudeByte { get; set; }
        public byte[] Length { get; set; }

        public ModelMess()
        {
            this.CommandType = null;
            this.CommandId = null;
            this.CommandAction = null;
            this.Status = null;
            this.States = null;
            this.Time = null;
            this.Sequence = null;
            this.GPSByte = null;
            this.Length = null;
        }
        public string WriteByteLog()
        {
            string result = "";
            if (this.CommandType != null)
            {
                string hex = BitConverter.ToString(this.CommandType);
                string text = hex.Replace("-", "");
                result += "     CommandType:" + text;
            }
            if (this.CommandId != null)
            {
                string hex = BitConverter.ToString(this.CommandId);
                string text = hex.Replace("-", "");
                result += "     CommandId:" + text;
            }
            if (this.CommandAction != null)
            {
                string hex = BitConverter.ToString(this.CommandAction);
                string text = hex.Replace("-", "");
                result += "     CommandAction:" + text;
            }
            if (this.Status != null)
            {
                string hex = BitConverter.ToString(this.Status);
                string text = hex.Replace("-", "");
                result += "     Status:" + text;
            }
            if (this.States != null)
            {
                string hex = BitConverter.ToString(this.States);
                string text = hex.Replace("-", "");
                result += "     States:" + text;
            }
            if (this.Time != null)
            {
                string hex = BitConverter.ToString(this.Time);
                string text = hex.Replace("-", "");
                result += "     Time:" + text;
            }
            if (this.Sequence != null)
            {
                string hex = BitConverter.ToString(this.Sequence);
                string text = hex.Replace("-", "");
                result += "     Sequence:" + text;
            };
            if (this.GPSByte != null)
            {
                string hex = BitConverter.ToString(this.GPSByte);
                string text = hex.Replace("-", "");
                result += "     GPSByte:" + text;
            }
            if (this.Length != null)
            {
                string hex = BitConverter.ToString(this.Length);
                string text = hex.Replace("-", "");
                result += "     Length:" + text;
            }
            return result;
        }
    }

    public class ThietBiStatusMess
    {
        public DateTime ThoiGian { get; set; }

        public string CommandType { get; set; }
        public string CommandId { get; set; }
        public string CommandAction { get; set; }
        public string ThietBiID { get; set; }
        public int? Loai { get; set; }
        public int? StatusMay { get; set; }
        public int? TrangThai { get; set; }
        public int? Time { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }


        public string WriteLog()
        {
            string result = "";
            if (this.CommandType != null)
            {
                result += "     CommandType:" + string.Join(" ", this.CommandType);
            }
            if (this.CommandId != null)
            {
                result += "     CommandId:" + string.Join(" ", this.CommandId);
            }
            if (this.CommandAction != null)
            {
                result += "     CommandAction:" + string.Join(" ", this.CommandAction);
            }
            if (this.ThietBiID != null)
            {
                result += "     ThietBiID:" + this.ThietBiID;
            }
            if (this.Loai != null)
            {
                result += "     Loai:" + this.Loai.Value.ToString();
            }
            if (this.StatusMay != null)
            {
                result += "     StatusMay:" + this.StatusMay.Value.ToString();
            }
            if (this.TrangThai != null)
            {
                result += "     TrangThai:" + this.TrangThai.Value.ToString();
            }
            if (this.Time != null)
            {
                result += "     Time:" + this.Time.Value.ToString();
            }
            if (this.Latitude != null)
            {
                result += "     Latitude:" + this.Latitude.Value.ToString();
            }
            if (this.Longitude != null)
            {
                result += "     Longitude:" + this.Longitude.Value.ToString();
            }
            return result;
        }
    }
    
}