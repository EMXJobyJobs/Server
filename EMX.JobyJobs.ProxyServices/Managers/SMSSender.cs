using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using EMX.JobyJobs.ProxyServices.GoldmanSMSService;

namespace EMX.JobyJobs.ProxyServices.Managers
{
    /// <summary>
    /// proxy class for sending sms.
    /// </summary>
    public static class SMSSender
    {
        /// <summary>
        /// Sends regular sms (single)
        /// </summary>
        /// <param name="phoneNnumber"></param>
        /// <param name="msg"></param>
        public static void SendRegular(string phoneNnumber, string msg)
        {
            try
            {
                using (var client = new GoldmanSMSService.SendSMSSoapClient())
                {
                    client.SUBMITSMSRegEx(phoneNnumber, msg);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Sends regular sms (many)
        /// </summary>
        /// <param name="phoneNnumber"></param>
        /// <param name="msg"></param>
        public static void SendRegularMany(List<string> phoneNnumber, string msg)
        {
            try
            {
                using (var client = new GoldmanSMSService.SendSMSSoapClient())
                {
                    client.SUBMITSMSRegManyEx(phoneNnumber, msg);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// sends personal sms
        /// </summary>
        /// <param name="data">K: phoneNumber, V: phoneNumberMSG</param>
        public static void SendPersonal(Dictionary<string, string> data)
        {
            try
            {
                using (var client = new GoldmanSMSService.SendSMSSoapClient())
                {
                    client.SUBMITSMSPerEx(data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }




        private static List<RegMobileList> CreateRegMobileList(List<string> phoneNumbers)
        {
            return phoneNumbers.Select(item => new RegMobileList() { MobileNumber = item }).ToList();
        }
        static List<PerMobileList> CreatePerMobileList(Dictionary<string, string> data)
        {
            return data.Select(kp => new PerMobileList() { MobileNumber = kp.Key, PersonalSMS = kp.Value }).ToList();
        }

        private class RegMobileList
        {
            public string MobileNumber { get; set; }
        }
        private class PerMobileList
        {
            public string MobileNumber { get; set; }
            public string PersonalSMS { get; set; }
        }
        class Config
        {
            public static string sUser = "MyUser";
            public static string sPass = "MyPass";
            public static string SenderPrefix = "054";
            public static string SenderSuffix = "5448750";
        }

        private static string GenerateRegXML(List<string> phoneNumbers, string msg)
        {
            List<RegMobileList> MobList = CreateRegMobileList(phoneNumbers);
            XElement xmlSendSMS = new XElement("SMS",
                new XElement("USERNAME", new XCData(Config.sUser)),
                new XElement("PASSWORD", new XCData(Config.sPass)),
                new XElement("SENDER_PREFIX", Config.SenderPrefix),
                new XElement("SENDER_SUFFIX", new XCData(Config.SenderSuffix)),
                new XElement("MSGLNG", "HEB"),
                new XElement("MSG", new XCData(msg + "\n" + "ירידת שורה")));

            xmlSendSMS.Add(new XElement("MOBILE_LIST",
                from mob in MobList
                select new XElement("MOBILE_NUMBER", mob.MobileNumber)));

            xmlSendSMS.Add(new XElement("UNICODE", "Fasle"),
                new XElement("USE_PERSONAL", "False"));

            string sXML = xmlSendSMS.ToString();
            return sXML;
        }
        private static string GeneratePerXML(Dictionary<string, string> data)
        {
            List<PerMobileList> MobList = CreatePerMobileList(data);

            XElement xmlSendSMS = new XElement("SMS",
                new XElement("USERNAME", new XCData(Config.sUser)),
                new XElement("PASSWORD", new XCData(Config.sPass)),
                new XElement("SENDER_PREFIX", Config.SenderPrefix),
                new XElement("SENDER_SUFFIX", new XCData(Config.SenderSuffix)),
                new XElement("MSGLNG", "HEB"));

            xmlSendSMS.Add(from mob in MobList
                           select new XElement("MOBILE_LIST",
                               new XElement("MOBILE_NUMBER",
                                   new XElement("NUMBER", mob.MobileNumber),
                                   new XElement("PER_SMS", new XCData(mob.PersonalSMS)))));

            xmlSendSMS.Add(new XElement("MSG", new XCData("dummy")),
                new XElement("UNICODE", "Fasle"),
                new XElement("USE_PERSONAL", "True"));

            string sXML = xmlSendSMS.ToString();
            return sXML;
        }

        //Extensions:
        private static void SUBMITSMSRegEx(this SendSMSSoapClient obj, string phoneNumber, string msg)
        {
            string xml = SMSSender.GenerateRegXML(new List<string>() { phoneNumber }, msg);
            obj.SUBMITSMS(xml);
        }
        private static void SUBMITSMSRegManyEx(this SendSMSSoapClient obj, List<string> phoneNumbers, string msg)
        {
            string xml = SMSSender.GenerateRegXML(phoneNumbers, msg);
            obj.SUBMITSMS(xml);
        }
        private static void SUBMITSMSPerEx(this SendSMSSoapClient obj, Dictionary<string, string> data)
        {
            string xml = SMSSender.GeneratePerXML(data);
            obj.SUBMITSMS(xml);
        }
    }
}
