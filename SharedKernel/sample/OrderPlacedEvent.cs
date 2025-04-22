using SharedKernel.Abstractions;
using System;
using System.Collections.Generic;

namespace SharedKernel.Events
{
    /// <summary>
    /// رویداد مربوط به ثبت سفارش جدید.
    /// </summary>
    public class OrderPlacedEvent : IEvent
    {
        /// <summary>
        /// شناسه سفارش.
        /// </summary>
        public Guid OrderId { get; }

        /// <summary>
        /// زمان وقوع رویداد.
        /// </summary>
        public DateTime OccurredOn { get; }

        /// <summary>
        /// شناسه منحصر به فرد رویداد.
        /// </summary>
        public Guid EventId { get; }

        /// <summary>
        /// نوع رویداد.
        /// </summary>
        public string EventType { get; }

        /// <summary>
        /// متادیتا (اطلاعات اضافی) مرتبط با رویداد.
        /// </summary>
        public IDictionary<string, object> Metadata { get; }

        /// <summary>
        /// سازنده رویداد.
        /// </summary>
        /// <param name="orderId">شناسه سفارش.</param>
        public OrderPlacedEvent(Guid orderId)
        {
            OrderId = orderId;
            OccurredOn = DateTime.UtcNow;
            EventId = Guid.NewGuid();
            EventType = nameof(OrderPlacedEvent);
            Metadata = new Dictionary<string, object>
            {
                { "Source", "OrderService" },
                { "Priority", "High" },
                { "Environment", "Production" }
            };
        }
    }
}