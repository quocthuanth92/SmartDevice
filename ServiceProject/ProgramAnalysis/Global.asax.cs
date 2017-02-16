using ProgramAnalysis.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ProgramAnalysis
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            CustomLog.LogPath = HttpContext.Current.Server.MapPath("~/Logs/");


            #region Config
            //Gateway.Gateway gateway = new Gateway.Gateway();
            //gateway.ConfigConnect();

            //gateway.client = new MqttClient(IPAddress.Parse("45.117.80.39"));
            //clientID = "0000000AAAAAAAA";
            //gateway.client.Connect(clientID);
            //CustomLog.LogError("connect thanh cong");
            //string[] topic = { "#", "Test/#" };

            //byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE };
            //gateway.client.Subscribe(topic, qosLevels);

            //gateway.client.MqttMsgPublishReceived += gateway.client_MqttMsgPublishReceived;
            //gateway.client.MqttMsgSubscribed += gateway.client_MqttMsgSubscribed;
            //gateway.client.MqttMsgUnsubscribed += gateway.client_MqttMsgUnsubscribed;

            //gateway.TimerTick.Start();
            #endregion
        }
    }
}
