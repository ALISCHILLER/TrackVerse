using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedKernel.Audit
{
    public class AuditLogS
    {
        public List<AuditChangeS> Changes { get; set; } = new List<AuditChangeS>();
    }

    public class AuditChangeS
    {
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }

    public class AuditService
    {
        public void SaveChanges(IEnumerable<AuditChangeS> changes)
        {
            foreach (var change in changes)
            {
                Console.WriteLine($"Property: {change.PropertyName}, Old: {change.OldValue}, New: {change.NewValue}");
            }
        }

        public void ProcessAuditLog(AuditLogS auditLog)
        {
            if (auditLog.Changes.Count() == 0) // فراخوانی متد Count()
            {
                Console.WriteLine("No changes to save.");
                return;
            }

            SaveChanges(auditLog.Changes); // ارسال لیست تغییرات
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var auditLog = new AuditLogS
            {
                Changes = new List<AuditChangeS>
                {
                    new AuditChangeS { PropertyName = "Email", OldValue = "old@example.com", NewValue = "new@example.com" },
                    new AuditChangeS { PropertyName = "Name", OldValue = "John", NewValue = "Doe" }
                }
            };

            var auditService = new AuditService();
            auditService.ProcessAuditLog(auditLog);
        }
    }
}