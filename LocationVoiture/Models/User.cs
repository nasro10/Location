using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseVersion.Helpers.Quickblox
{
    public class RootUserRequest
    {
        [JsonProperty("user")]
        public UserRequest User { get; set; }
    }

    public class UpdateRootUserRequest
    {
        [JsonProperty("user")]
        public UpdateUserRequest User { get; set; }
    }

    public class UserRequest
    {
        [JsonProperty("full_name")]
        public string Fullname { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
    }

    public class UpdateUserRequest
    {
        [JsonProperty("full_name")]
        public string Fullname { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("old_password")]
        public string OldPassword { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
    }

    public class RootUser
    {
        [JsonProperty("user")]
        public User User { get; set; }
    }

    public class User
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("full_name")]
        public string Fullname { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("website")]
        public string Website { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("last_request_at")]
        public object LastRequestAt { get; set; }
        [JsonProperty("external_user_id")]
        public int? ExternalUserId { get; set; }
        [JsonProperty("facebook_id")]
        public string FacebookId { get; set; }
        [JsonProperty("twitter_id")]
        public string TwitterId { get; set; }
        [JsonProperty("blob_id")]
        public object BlobId { get; set; }
        [JsonProperty("custom_data")]
        public object CustomData { get; set; }
        [JsonProperty("user_tags")]
        public string UserTags { get; set; }
    }
}