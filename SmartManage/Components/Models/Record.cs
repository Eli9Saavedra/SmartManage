using System;

namespace SmartManage.Components.Models
{
    public class Record
    {
        public int RecordId { get; set; }
        public string RecordName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;

        // Owner relationship (new)
        public int? OwnerId { get; set; }
        public User? Owner { get; set; }
    }
}
