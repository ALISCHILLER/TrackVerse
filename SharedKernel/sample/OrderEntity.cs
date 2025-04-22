//using SharedKernel.Abstractions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SharedKernel.sample
//{
//    public class OrderEntity : BaseEntity, IAggregateRoot
//    {
//        private readonly List<IDomainEvent> _domainEvents = new();

//        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

//        public void AddDomainEvent(IDomainEvent domainEvent)
//        {
//            _domainEvents.Add(domainEvent);
//        }

//        public void RemoveDomainEvent(IDomainEvent domainEvent)
//        {
//            _domainEvents.Remove(domainEvent);
//        }

//        public void ClearDomainEvents()
//        {
//            _domainEvents.Clear();
//        }

//        public void PlaceOrder()
//        {
//            // منطق ایجاد سفارش
//            AddDomainEvent(new OrderPlacedEvent(this.Id));
//        }
//    }
//}
