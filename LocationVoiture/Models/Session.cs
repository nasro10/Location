using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseVersion.Helpers.Quickblox
{
    public class RootSession
    {
        public Session Session { get; set; }
    }

    public class Session
    {
        public int ApplicationId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Id { get; set; }
        public int Nonce { get; set; }
        public string Token { get; set; }
        public int Ts { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        public string _id { get; set; }
    }
}