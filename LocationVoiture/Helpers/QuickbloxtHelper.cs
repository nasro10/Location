using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BaseVersion.Helpers.Quickblox
{
    public class QuickbloxtHelper
    {
        public const string quickBloxRESTAPIVersion = "0.1.0";

        public static Session GetSession(string postDataContentType = "application/x-www-form-urlencoded")
        {
            Session session = new Session();

            try
            {
                string applicationId = ConfigurationManager.AppSettings["QB-ApplicationId"];
                string authKey = ConfigurationManager.AppSettings["QB-AuthKey"];
                string unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
                string nonce = new Random().Next().ToString();
                string signature = GenerateSignature(applicationId, authKey, nonce, unixTimestamp);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.quickblox.com/session.json");
                request.Method = "POST";
                request.KeepAlive = false;
                request.ContentType = postDataContentType;
                request.Headers.Add(string.Format("QuickBlox-REST-API-Version: {0}", quickBloxRESTAPIVersion));
                // form data parameters
                NameValueCollection data = HttpUtility.ParseQueryString(String.Empty);
                data.Add("application_id", applicationId);
                data.Add("auth_key", authKey);
                data.Add("timestamp", unixTimestamp);
                data.Add("nonce", nonce);
                data.Add("signature", signature);

                string postData = data.ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                HttpStatusCode responseCode = ((HttpWebResponse)response).StatusCode;
                if (responseCode.Equals(HttpStatusCode.Unauthorized) || responseCode.Equals(HttpStatusCode.Forbidden))
                {
                    // var text = "Unauthorized - need new token";

                }
                else if (responseCode.Equals(HttpStatusCode.Created))
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    var result = reader.ReadToEnd();
                    var rootSession = JsonConvert.DeserializeObject<RootSession>(result);
                    session = rootSession.Session;

                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return session;
        }

        public static Session GetSession(string login, string password, string postDataContentType = "application/x-www-form-urlencoded")
        {
            Session session = new Session();

            try
            {
                string applicationId = ConfigurationManager.AppSettings["QB-ApplicationId"];
                string authKey = ConfigurationManager.AppSettings["QB-AuthKey"];
                string unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
                string nonce = new Random().Next().ToString();
                string signature = GenerateSignature(applicationId, authKey, nonce, unixTimestamp, login, password);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.quickblox.com/session.json");
                request.Method = "POST";
                request.KeepAlive = false;
                request.ContentType = postDataContentType;
                request.Headers.Add(string.Format("QuickBlox-REST-API-Version: {0}", quickBloxRESTAPIVersion));
                // form data parameters
                NameValueCollection data = HttpUtility.ParseQueryString(String.Empty);
                data.Add("application_id", applicationId);
                data.Add("auth_key", authKey);
                data.Add("timestamp", unixTimestamp);
                data.Add("nonce", nonce);
                data.Add("signature", signature);
                data.Add("user[login]", login);
                data.Add("user[password]", password);

                string postData = data.ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                HttpStatusCode responseCode = ((HttpWebResponse)response).StatusCode;
                if (responseCode.Equals(HttpStatusCode.Unauthorized) || responseCode.Equals(HttpStatusCode.Forbidden))
                {
                    // var text = "Unauthorized - need new token";

                }
                else if (!responseCode.Equals(HttpStatusCode.OK))
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    var result = reader.ReadToEnd();
                    var rootSession = JsonConvert.DeserializeObject<RootSession>(result);
                    session = rootSession.Session;

                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return session;
        }

        public static RootUser SignUp(RootUserRequest userRequest, string postDataContentType = "application/json")
        {
            RootUser user = new RootUser();


            try
            {
                // get session
                Session session = GetSession();
                //
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.quickblox.com/users.json");
                request.Method = "POST";
                request.KeepAlive = false;
                request.ContentType = postDataContentType;
                request.Headers.Add(string.Format("QuickBlox-REST-API-Version: {0}", quickBloxRESTAPIVersion));
                request.Headers.Add(string.Format("QB-Token: {0}", session.Token));

                string postData = JsonConvert.SerializeObject(userRequest);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = byteArray.Length;

                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                try
                {
                    requestWriter.Write(postData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    requestWriter.Close();
                    requestWriter = null;
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();
                    user = JsonConvert.DeserializeObject<RootUser>(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        public static RootUser UpdateUserByIdentifier(string login, int identifier, UpdateRootUserRequest userRequest, string postDataContentType = "application/json")
        {
            RootUser user = new RootUser();


            try
            {
                // get session
                Session session = GetSession(login, login);
                //
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://api.quickblox.com/users/{0}.json", identifier));
                request.Method = "PUT";
                request.KeepAlive = false;
                request.ContentType = postDataContentType;
                request.Headers.Add(string.Format("QuickBlox-REST-API-Version: {0}", quickBloxRESTAPIVersion));
                request.Headers.Add(string.Format("QB-Token: {0}", session.Token));

                string postData = JsonConvert.SerializeObject(userRequest);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = byteArray.Length;

                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                try
                {
                    requestWriter.Write(postData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    requestWriter.Close();
                    requestWriter = null;
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();
                    user = JsonConvert.DeserializeObject<RootUser>(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        public static RootUser GetUserByIdentifier(int identifier, string postDataContentType = "application/json")
        {
            RootUser user = new RootUser();

            try
            {
                // get session
                Session session = GetSession();
                //
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://api.quickblox.com/users/{0}.json", identifier));
                request.Method = "GET";
                request.KeepAlive = false;
                request.ContentType = postDataContentType;
                request.Headers.Add(string.Format("QuickBlox-REST-API-Version: {0}", quickBloxRESTAPIVersion));
                request.Headers.Add(string.Format("QB-Token: {0}", session.Token));

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();
                    user = JsonConvert.DeserializeObject<RootUser>(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        public static RootUser GetUserByLogin(string login, string postDataContentType = "application/json")
        {
            RootUser user = new RootUser();

            try
            {
                // get session
                Session session = GetSession();
                //
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://api.quickblox.com/users/by_login.json?login={0}", login));
                request.Method = "GET";
                request.KeepAlive = false;
                request.ContentType = postDataContentType;
                request.Headers.Add(string.Format("QuickBlox-REST-API-Version: {0}", quickBloxRESTAPIVersion));
                request.Headers.Add(string.Format("QB-Token: {0}", session.Token));

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();
                    user = JsonConvert.DeserializeObject<RootUser>(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        public static HttpStatusCode DeleteUserByIdentifier(int identifier, string postDataContentType = "application/json")
        {
            CreateDialogResponse dialogResponse = new CreateDialogResponse();

            // get session
            RootUser user = GetUserByIdentifier(identifier);
            Session session = GetSession(user.User.Login, user.User.Login);
            //
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://api.quickblox.com/users/{0}.json", identifier));
            request.Method = "DELETE";
            request.KeepAlive = false;
            request.ContentType = postDataContentType;
            request.Headers.Add(string.Format("QuickBlox-REST-API-Version: {0}", quickBloxRESTAPIVersion));
            request.Headers.Add(string.Format("QB-Token: {0}", session.Token));


            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
            requestWriter.Close();
            requestWriter = null;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode;
        }

        public static RootDialog GetDialogs(string login, string postDataContentType = "application/json")
        {
            RootDialog dialogs = new RootDialog();

            try
            {
                // get session
                Session session = GetSession(login, login);
                //
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://api.quickblox.com/chat/Dialog.json"));
                request.Method = "GET";
                request.KeepAlive = false;
                request.ContentType = postDataContentType;
                request.Headers.Add(string.Format("QB-Token: {0}", session.Token));

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();
                    dialogs = JsonConvert.DeserializeObject<RootDialog>(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dialogs;
        }

        public static CreateDialogResponse CreateDialog(string login, CreateDialogRequest dialog, string postDataContentType = "application/json")
        {
            CreateDialogResponse dialogResponse = new CreateDialogResponse();
            try
            {
                // get session
                Session session = GetSession(login, login);
                //
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.quickblox.com/chat/Dialog.json");
                request.Method = "POST";
                request.KeepAlive = false;
                request.ContentType = postDataContentType;
                request.Headers.Add(string.Format("QuickBlox-REST-API-Version: {0}", quickBloxRESTAPIVersion));
                request.Headers.Add(string.Format("QB-Token: {0}", session.Token));

                string postData = JsonConvert.SerializeObject(dialog);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = byteArray.Length;

                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                try
                {
                    requestWriter.Write(postData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    requestWriter.Close();
                    requestWriter = null;
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();
                    dialogResponse = JsonConvert.DeserializeObject<CreateDialogResponse>(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dialogResponse;
        }

        public static RootMessage GetMessages(string login, string dialogId, string postDataContentType = "application/json")
        {
            RootMessage messages = new RootMessage();

            try
            {
                // get session
                Session session = GetSession(login, login);
                //
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://api.quickblox.com/chat/Message.json?chat_dialog_id={0}", dialogId));
                request.Method = "GET";
                request.KeepAlive = false;
                request.ContentType = postDataContentType;
                request.Headers.Add(string.Format("QB-Token: {0}", session.Token));

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();
                    messages = JsonConvert.DeserializeObject<RootMessage>(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return messages;
        }

        /// <summary>
        /// return unreded mzssage of given dialog or all dialogs
        /// </summary>
        /// <param name="login"></param>
        /// <param name="dialogId">true : get unreaded of all dialogd</param>
        /// <param name="getAll"></param>
        /// <param name="postDataContentType"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetUnreadedMessages(string login, string dialogId, bool getAll, string postDataContentType = "application/json")
        {
            Dictionary<string, int> messages = new Dictionary<string, int>();

            try
            {
                // get session
                Session session = GetSession(login, login);
                //
                string uri = string.Format("https://api.quickblox.com/chat/Message/unread.json" + ((!getAll) ? "?chat_dialog_ids={0}" : ""), dialogId);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
                request.KeepAlive = false;
                request.ContentType = postDataContentType;
                request.Headers.Add(string.Format("QB-Token: {0}", session.Token));

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();

                    messages = JsonConvert.DeserializeObject<Dictionary<string, int>>(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return messages;
        }

        public static CreateMessageResponse CreateMessage(string login, CreateMessageRequest message, string postDataContentType = "application/json")
        {
            CreateMessageResponse messageResponse = new CreateMessageResponse();
            try
            {
                // get session
                Session session = GetSession(login, login);
                //
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.quickblox.com/chat/Message.json");
                request.Method = "POST";
                request.KeepAlive = false;
                request.ContentType = postDataContentType;
                request.Headers.Add(string.Format("QuickBlox-REST-API-Version: {0}", quickBloxRESTAPIVersion));
                request.Headers.Add(string.Format("QB-Token: {0}", session.Token));

                string postData = JsonConvert.SerializeObject(message);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = byteArray.Length;

                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                try
                {
                    requestWriter.Write(postData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    requestWriter.Close();
                    requestWriter = null;
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();
                    messageResponse = JsonConvert.DeserializeObject<CreateMessageResponse>(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return messageResponse;
        }

        public static string GenerateSignature(string applicationId, string authKey, string nonce, string timestamp)
        {
            using (HMACSHA1 hmac = new HMACSHA1(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["QB-AuthSecret"])))
            {
                StringBuilder signature = new StringBuilder();
                signature.Append("application_id=" + applicationId);
                signature.Append("&auth_key=" + authKey);
                signature.Append("&nonce=" + nonce);
                signature.Append("&timestamp=" + timestamp);

                return ByteToString(hmac.ComputeHash(Encoding.ASCII.GetBytes(signature.ToString())));
            }
        }

        public static string GenerateSignature(string applicationId, string authKey, string nonce, string timestamp, string login, string password)
        {
            using (HMACSHA1 hmac = new HMACSHA1(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["QB-AuthSecret"])))
            {
                StringBuilder signature = new StringBuilder();
                signature.Append("application_id=" + applicationId);
                signature.Append("&auth_key=" + authKey);
                signature.Append("&nonce=" + nonce);
                signature.Append("&timestamp=" + timestamp);
                signature.Append("&user[login]=" + login);
                signature.Append("&user[password]=" + password);

                return ByteToString(hmac.ComputeHash(Encoding.ASCII.GetBytes(signature.ToString())));
            }
        }

        public static string ByteToString(IEnumerable<byte> data)
        {
            return string.Concat(data.Select(b => b.ToString("x2")));
        }
    }
}