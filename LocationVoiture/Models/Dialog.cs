using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseVersion.Helpers.Quickblox
{
    public partial class RootDialog
    {
        [JsonProperty("total_entries")]
        public int TotalEntries { get; set; }

        [JsonProperty("skip")]
        public int Skip { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("items")]
        public Dialog[] Items { get; set; }
    }

    public partial class Dialog
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("last_message")]
        public object LastMessage { get; set; }

        [JsonProperty("last_message_date_sent")]
        public object LastMessageDateSent { get; set; }

        [JsonProperty("last_message_id")]
        public object LastMessageId { get; set; }

        [JsonProperty("last_message_user_id")]
        public object LastMessageUserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("occupants_ids")]
        public int[] OccupantsIds { get; set; }

        [JsonProperty("photo")]
        public object Photo { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("xmpp_room_jid")]
        public string XmppRoomJid { get; set; }

        [JsonProperty("unread_messages_count")]
        public int UnreadMessagesCount { get; set; }

        [JsonProperty("data")]
        public CustomData CustomData { get; set; }
    }

    public class CreateDialogRequest
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("occupants_ids")]
        public string OccupantsIds { get; set; }

        [JsonProperty("data")]
        public CustomData CustomData { get; set; }
    }

    public class CustomData
    {
        [JsonProperty("class_name")]
        public string ClassName { get; set; }
        [JsonProperty("offer_id")]
        public int OfferId { get; set; }
        [JsonProperty("order_id")]
        public int OrderId { get; set; }
        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }
        [JsonProperty("opponent_name")]
        public string OpponentName { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
    }

    public class CreateDialogResponse
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("last_message")]
        public object LastMessage { get; set; }

        [JsonProperty("last_message_date_sent")]
        public object LastMessageDateSent { get; set; }

        [JsonProperty("last_message_id")]
        public object LastMessageId { get; set; }

        [JsonProperty("last_message_user_id")]
        public object LastMessageUserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("occupants_ids")]
        public long[] OccupantsIds { get; set; }

        [JsonProperty("photo")]
        public object Photo { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("xmpp_room_jid")]
        public string XmppRoomJid { get; set; }

        [JsonProperty("unread_messages_count")]
        public long? UnreadMessagesCount { get; set; }
    }
}