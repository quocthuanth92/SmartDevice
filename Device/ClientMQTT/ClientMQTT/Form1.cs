using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Web.Script.Serialization;

namespace ClientMQTT
{
    public partial class Form1 : Form
    {
        MqttClient client;
        Gateway gateway;
        string dcuSerial;
        string DPM;
        public Form1()
        {
            InitializeComponent();
            #region Config
            gateway = new Gateway();
            gateway.client = new MqttClient(IPAddress.Parse("45.117.80.39"));
            //gateway.client = new MqttClient("tcp://test.mosquitto.org:1883");
            
            dcuSerial = "sdsdsdsd";
            gateway.client.Connect(dcuSerial);
            CustomLog.LogError("connect thanh cong");
            string[] topic = { "Home/DeviceTest1/Periodic", "PingTest" };

            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE };
            gateway.client.Subscribe(topic, qosLevels);

            gateway.client.MqttMsgPublishReceived += gateway.client_MqttMsgPublishReceived;
            gateway.client.MqttMsgSubscribed += gateway.client_MqttMsgSubscribed;
            gateway.client.MqttMsgUnsubscribed += gateway.client_MqttMsgUnsubscribed;
            #endregion
            
        }
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string result = "Nhan client_2: " + System.Text.Encoding.UTF8.GetString(e.Message);
            CustomLog.LogError(result);
        }

        void client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            string result = "client_MqttMsgUnsubscribed_ 2: " + e.MessageId.ToString();
            CustomLog.LogError(result);
        }

        void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            string result = "client_MqttMsgSubscribed_ 2: " + e.MessageId.ToString();
            CustomLog.LogError(result);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] textA = new byte[] { 0x01, 0x0A, 0x02, 0x01 };
                gateway.client.Publish("Home/DeviceTest1/Action/Line1", textA);
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] textA = new byte[] { 0x01, 0x0A, 0x02, 0x00 };
            gateway.client.Publish("Home/DeviceTest1/Action/Line1", textA);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] textA = new byte[] { 0x03, 0x01, 0x0B, 0x05, 0x01, 0x00, 0x24, 0x01, 0x03, 0x06, 0x01, 0x00 };
                gateway.client.Publish("info/001/7000000001", textA);
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                throw;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] textTatB = new byte[] { 0x03, 0x01, 0x0B, 0x05, 0x01, 0x01, 0x24, 0x01, 0x23, 0x06, 0x01, 0x01, 0x07, 0x04, 0x00, 0x00, 0x00, 0x01, 0x08, 0x18, 0x32, 0x31, 0x30, 0x31, 0x2E, 0x30, 0x30, 0x35, 0x33, 0x2C, 0x4E, 0x2C, 0x31, 0x30, 0x35, 0x34, 0x37, 0x2E, 0x30, 0x33, 0x37, 0x31, 0x2C, 0x45 };
                gateway.client.Publish("info/001/7000000001", textTatB);
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                throw;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] textC = new byte[] { 0x03, 0x01, 0x0D, 0x05, 0x01, 0x00, 0x24, 0x01, 0x03, 0x06, 0x01, 0x00 };
                gateway.client.Publish("info/001/7000000009", textC);
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                throw;
            }
        }

        private void DKNoMay_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] textDKNomay = new byte[] { 
                        0x03, 0x01, 0x02, 
                        0x05, 0x01, 0x01,
                        0x1F, 0x21, 0x18,    0x32, 0x31, 0x30, 0x31, 0x2E, 0x30, 0x30, 0x35, 0x33, 0x2C, 0x4E, 0x2C, 0x31, 0x30, 0x35, 0x34, 0x37, 0x2E, 0x30, 0x33, 0x37, 0x31, 0x2C, 0x45,
                        0x1F, 0x23, 0x04, 0x00, 0x00, 0x00, 0x18,
                        0x1F, 0x24, 0x04, 0x0E, 0x05, 0x01, 0x00
                };

                gateway.client.Publish("info/001/7000000009", textDKNomay);
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                throw;
            }
        }

        private void DKTatMay_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] textDKNomay = new byte[] { 
                        0x03, 0x01, 0x02, 
                        0x05, 0x01, 0x01,
                        0x1F, 0x21, 0x18,    0x32, 0x31, 0x30, 0x31, 0x2E, 0x30, 0x30, 0x35, 0x33, 0x2C, 0x4E, 0x2C, 0x31, 0x30, 0x35, 0x34, 0x37, 0x2E, 0x30, 0x33, 0x37, 0x31, 0x2C, 0x45,
                        0x1F, 0x23, 0x04, 0x00, 0x00, 0x00, 0x18,
                        0x1F, 0x24, 0x04, 0x0E, 0x05, 0x01, 0x01
                };

                gateway.client.Publish("info/001/7000000009", textDKNomay);
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                throw;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                byte[] textDKNomay = new byte[] { 
                        0x03, 0x01, 0x02, 
                        0x05, 0x01, 0x01,
                        0x1F, 0x21, 0x18,    0x32, 0x31, 0x30, 0x31, 0x2E, 0x30, 0x30, 0x35, 0x33, 0x2C, 0x4E, 0x2C, 0x31, 0x30, 0x35, 0x34, 0x37, 0x2E, 0x30, 0x33, 0x37, 0x31, 0x2C, 0x45,
                        0x1F, 0x23, 0x04, 0x00, 0x00, 0x00, 0x18,
                        0x1F, 0x24, 0x04, 0x0C, 0x05, 0x01, 0x00
                };

                gateway.client.Publish("info/001/7000000009", textDKNomay);
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                throw;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] textDKNomay = new byte[] { 
                        0x03, 0x01, 0x02, 
                        0x05, 0x01, 0x01,
                        0x1F, 0x21, 0x18,    0x32, 0x31, 0x30, 0x31, 0x2E, 0x30, 0x30, 0x35, 0x33, 0x2C, 0x4E, 0x2C, 0x31, 0x30, 0x35, 0x34, 0x37, 0x2E, 0x30, 0x33, 0x37, 0x31, 0x2C, 0x45,
                        0x1F, 0x23, 0x04, 0x00, 0x00, 0x00, 0x18,
                        0x1F, 0x24, 0x04, 0x0C, 0x05, 0x01, 0x01
                };

                gateway.client.Publish("info/001/7000000009", textDKNomay);
            }
            catch (Exception ex)
            {
                CustomLog.LogError(ex);
                throw;
            }
        }

        

    }
}
