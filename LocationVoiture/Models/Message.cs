using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseVersion.Helpers.Quickblox
{
    public partial class RootMessage
    {
        [JsonProperty("skip")]
        public long Skip { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }

    public class Item
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("attachments")]
        public object[] Attachments { get; set; }

        [JsonProperty("chat_dialog_id")]
        public string ChatDialogId { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("date_sent")]
        public long DateSent { get; set; }

        [JsonProperty("delivered_ids")]
        public long[] DeliveredIds { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("read_ids")]
        public long[] ReadIds { get; set; }

        [JsonProperty("recipient_id")]
        public long? RecipientId { get; set; }

        [JsonProperty("sender_id")]
        public long SenderId { get; set; }    
        [JsonProperty("senderName")]
        public string SenderName { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("read")]
        public long Read { get; set; }
    }

    public class CreateMessageRequest
    {
        [JsonProperty("chat_dialog_id")]
        public string ChatDialogId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("recipient_id")]
        public long RecipientId { get; set; }

        [JsonProperty("attachments")]
        public Dictionary<string, Attachment> Attachments { get; set; }

        [JsonProperty("sender_name")]
        public string SenderName { get; set; }
    }

    public partial class Attachment
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class CreateMessageResponse
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("attachments")]
        public object[] Attachments { get; set; }

        [JsonProperty("chat_dialog_id")]
        public string ChatDialogId { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("date_sent")]
        public long DateSent { get; set; }

        [JsonProperty("delivered_ids")]
        public long[] DeliveredIds { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("read_ids")]
        public long[] ReadIds { get; set; }

        [JsonProperty("recipient_id")]
        public long RecipientId { get; set; }

        [JsonProperty("sender_id")]
        public long SenderId { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("read")]
        public long Read { get; set; }
    }

    public partial class UnreadedMessage
    {
        [JsonProperty("skip")]
        public long Skip { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }
}