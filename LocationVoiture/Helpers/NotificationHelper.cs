using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Script.Serialization;
namespace LocationVoiture.Helpers
{
    public class NotificationHelper
    {
        public static string SendFCMNotificationWithBadgeIOS(string deviceToken, string title, string message, int type, int badge, string postDataContentType = "application/json")
        {
            // from here:
            // http://stackoverflow.com/questions/11431261/unauthorized-when-calling-google-gcm
            //
            // original:
            // http://www.codeproject.com/Articles/339162/Android-push-notification-implementation-using-ASP

            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

            //
            //  MESSAGE CONTENT

            string postData = "{ \"registration_ids\": [ ";

            postData += "\"" + deviceToken + "\",";

            // remove last ","
            postData = postData.Remove(postData.Length - 1);

            postData += "], \"priority\": \"High\",\"content_available\": true,\"notification\": { \"badge\":\"" 
                + badge + "\", \"sound\":\"default\",   \"body\":\"" + message + "\" , \"title\":\"" 
                + title + "\" ,  \"message\":\"" + message + "\" , \"type\":\"" 
                + type + "\"} ,\"data\": { \"badge\":\"" + badge 
                + "\", \"sound\":\"default\",   \"body\":\"" + message + "\" , \"title\":\"" 
                + title + "\" ,  \"message\":\"" + message + "\", \"type\":\"" + type + "\"}}";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //
            //  CREATE REQUEST
            // HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
            //HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://gcm-http.googleapis.com/gcm/send");
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            Request.Method = "POST";
            Request.KeepAlive = false;
            Request.ContentType = postDataContentType;
            Request.Headers.Add(string.Format("Authorization: key={0}", ConfigurationManager.AppSettings["FCMServerKey"]));
            Request.ContentLength = byteArray.Length;

            Stream dataStream = Request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            //
            //  SEND MESSAGE
            try
            {
                WebResponse Response = Request.GetResponse();
                HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
                if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
                {
                    // var text = "Unauthorized - need new token";

                }
                else if (!ResponseCode.Equals(HttpStatusCode.OK))
                {
                    //  var text = "Response from web service isn't OK";
                }

                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                string responseLine = Reader.ReadToEnd();
                Reader.Close();

                return responseLine;
            }
            catch (Exception)
            {

            }
            return "error";
        }

        public static string SendFCMNotificationWithBadgeAndroid(string deviceToken, string title, string message, int type, int badge, string postDataContentType = "application/json")
        {
            // from here:
            // http://stackoverflow.com/questions/11431261/unauthorized-when-calling-google-gcm
            //
            // original:
            // http://www.codeproject.com/Articles/339162/Android-push-notification-implementation-using-ASP

            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

            //
            //  MESSAGE CONTENT

            string postData = "{ \"registration_ids\": [ ";

            postData += "\"" + deviceToken + "\",";

            // remove last ","
            postData = postData.Remove(postData.Length - 1);

            postData += "], \"priority\": \"High\",\"content_available\": true ,\"data\": {\"badge\":\"" + badge + "\", \"sound\":\"default\",   \"body\":\"" + message + "\" , \"title\":\"" + title + "\" ,  \"message\":\"" + message + "\", \"type\":\"" + type + "\"}}";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //
            //  CREATE REQUEST
            // HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
            //HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://gcm-http.googleapis.com/gcm/send");
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            Request.Method = "POST";
            Request.KeepAlive = false;
            Request.ContentType = postDataContentType;
            Request.Headers.Add(string.Format("Authorization: key={0}", ConfigurationManager.AppSettings["FCMServerKey"]));
            Request.ContentLength = byteArray.Length;

            Stream dataStream = Request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            //
            //  SEND MESSAGE
            try
            {
                WebResponse Response = Request.GetResponse();
                HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
                if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
                {
                    //var text = "Unauthorized - need new token";

                }
                else if (!ResponseCode.Equals(HttpStatusCode.OK))
                {
                    //var text = "Response from web service isn't OK";
                }

                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                string responseLine = Reader.ReadToEnd();
                Reader.Close();

                return responseLine;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "error";
        }

        public static string SendFCMNotificationWithBadgeIOS(string deviceToken, string title, string message, int orderId, int type, int badge, string postDataContentType = "application/json")
        {
            // from here:
            // http://stackoverflow.com/questions/11431261/unauthorized-when-calling-google-gcm
            //
            // original:
            // http://www.codeproject.com/Articles/339162/Android-push-notification-implementation-using-ASP

            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

            //
            //  MESSAGE CONTENT

            string postData = "{ \"registration_ids\": [ ";

            postData += "\"" + deviceToken + "\",";

            // remove last ","
            postData = postData.Remove(postData.Length - 1);

            postData += "], \"priority\": \"High\",\"content_available\": true,\"notification\": { \"badge\":\"" + badge + "\", \"sound\":\"default\",   \"body\":\"" + message + "\" , \"title\":\"" + title + "\" ,  \"message\":\"" + message + "\" , \"orderId\":\"" + orderId + "\", \"type\":\"" + type + "\"} ,\"data\": { \"badge\":\"" + badge + "\", \"sound\":\"default\",   \"body\":\"" + message + "\" , \"title\":\"" + title + "\" ,  \"message\":\"" + message + "\" , \"orderId\":\"" + orderId + "\", \"type\":\"" + type + "\"}}";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //
            //  CREATE REQUEST
            // HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
            //HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://gcm-http.googleapis.com/gcm/send");
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            Request.Method = "POST";
            Request.KeepAlive = false;
            Request.ContentType = postDataContentType;
            Request.Headers.Add(string.Format("Authorization: key={0}", ConfigurationManager.AppSettings["FCMServerKey"]));
            Request.ContentLength = byteArray.Length;

            Stream dataStream = Request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            //
            //  SEND MESSAGE
            try
            {
                WebResponse Response = Request.GetResponse();
                HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
                if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
                {
                    // var text = "Unauthorized - need new token";

                }
                else if (!ResponseCode.Equals(HttpStatusCode.OK))
                {
                    //  var text = "Response from web service isn't OK";
                }

                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                string responseLine = Reader.ReadToEnd();
                Reader.Close();

                return responseLine;
            }
            catch (Exception)
            {

            }
            return "error";
        }

        public static string SendFCMNotificationWithBadgeAndroid(string deviceToken, string title, string message, int orderId, int type, int badge, string postDataContentType = "application/json")
        {
            // from here:
            // http://stackoverflow.com/questions/11431261/unauthorized-when-calling-google-gcm
            //
            // original:
            // http://www.codeproject.com/Articles/339162/Android-push-notification-implementation-using-ASP

            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

            //
            //  MESSAGE CONTENT

            string postData = "{ \"registration_ids\": [ ";

            postData += "\"" + deviceToken + "\",";

            // remove last ","
            postData = postData.Remove(postData.Length - 1);

            postData += "], \"priority\": \"High\",\"content_available\": true ,\"data\": {\"badge\":\"" + badge + "\", \"sound\":\"default\",   \"body\":\"" + message + "\" , \"title\":\"" + title + "\" ,  \"message\":\"" + message + "\" , \"orderId\":\"" + orderId + "\", \"type\":\"" + type + "\"}}";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //
            //  CREATE REQUEST
            // HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
            //HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://gcm-http.googleapis.com/gcm/send");
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            Request.Method = "POST";
            Request.KeepAlive = false;
            Request.ContentType = postDataContentType;
            Request.Headers.Add(string.Format("Authorization: key={0}", ConfigurationManager.AppSettings["FCMServerKey"]));
            Request.ContentLength = byteArray.Length;

            Stream dataStream = Request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            //
            //  SEND MESSAGE
            try
            {
                WebResponse Response = Request.GetResponse();
                HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
                if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
                {
                    //var text = "Unauthorized - need new token";

                }
                else if (!ResponseCode.Equals(HttpStatusCode.OK))
                {
                    //var text = "Response from web service isn't OK";
                }

                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                string responseLine = Reader.ReadToEnd();
                Reader.Close();

                return responseLine;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "error";
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static string GetNotificationTextByLanguage(string arabicText, string englishText, string language)
        {
            if (language.ToLower().Equals("ar"))
                return arabicText;
            else
                return englishText;
        }
    }
}