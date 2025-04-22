//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SharedKernel.Tests
//{
//    public class OrderEntityTests
//    {
//        [Fact]
//        public void AddDomainEvent_Should_AddEventToDomainEvents()
//        {
//            // Arrange
//            var order = new OrderEntity();
//            var domainEvent = new OrderPlacedEvent(Guid.NewGuid());

//            // Act
//            order.AddDomainEvent(domainEvent);

//            // Assert
//            Assert.Contains(domainEvent, order.DomainEvents);
//        }
//    }

//}
