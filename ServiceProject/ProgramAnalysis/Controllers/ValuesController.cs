﻿using ProgramAnalysis.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProgramAnalysis.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get()
        {
            var userName = this.RequestContext.Principal.Identity.Name;
            return String.Format("Hello, {0}.", userName);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        #region Device Management
        // POST api/values
        public void SetOnLight(MessModelValue value)
        {
            if(value.CommandType == ConstParam.Type.OnOff.ToString())
            {
                byte[] ping = new byte[] { 0x03, 0x01, 0x01 };
                this.client.Publish(ConstParam.PrefixTopic.Ping.ToString(), Encoding.UTF8.GetBytes("ping"));
            }
        }
        #endregion
    }
}
