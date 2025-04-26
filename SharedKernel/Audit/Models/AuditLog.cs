using System;
using System.Collections.Generic;

namespace SharedKernel.Audit
{
    /// <summary>
    /// مدل لاگ تغییرات.
    /// </summary>
    public class AuditLog
    {
        public string EntityId { get; set; }
        public string OperationType { get; set; }
        public string ChangedBy { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Reason { get; set; }
        public Dictionary<string, (object oldValue, object newValue)> Changes { get; set; }

        public AuditLog()
        {
            Changes = new Dictionary<string, (object oldValue, object newValue)>();
        }
    }
}
