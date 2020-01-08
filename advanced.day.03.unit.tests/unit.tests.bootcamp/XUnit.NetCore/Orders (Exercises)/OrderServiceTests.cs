namespace xUnit.NetCore.Orders
{
    using System;
    using ProductionCode.Orders;
    using ProductionCode.Orders.Models;
    using Xunit;

    public class OrderServiceTests
    {
        [Fact]
        public void TestOrderServiceRefundPayment()
        {
            //
            // Arrange
            //
            var order = new Order();
            order.OrderId = 123456;
            order.Amount = 100;
            order.ClientId = 200;
            order.ClientName = "Bob's FoodMart";
            order.BookedDate = new DateTime(2019, 01, 12);
            order.CreatedDate = new DateTime(2019, 01, 01);
            order.CurrencyIsoCode = "USD";
            order.Adjustments.Add(new Adjustment
            { Amount = -20, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01) });

            var invoice = new Invoice();
            invoice.InvoiceId = 1;
            invoice.OrderId = 123456;
            invoice.Amount = 100;
            invoice.CreatedDate = new DateTime(2019, 01, 01);

            order.Invoices.Add(invoice);






            var orderService = new OrderService();


            //
            // Act
            //
            var payment = orderService.PayInvoice(order, 1);
            var refund = orderService.RefundPayment(payment);


            //
            // Assert
            //
            Assert.Equal(payment.Amount, refund.Amount);
        }

        [Fact]
        public void TestOrderServicePayInvoice_ThrowsWhenInvalidInvoiceId()
        {
            //
            // Arrange
            //
        var order = new Order();
            order.OrderId = 123456;
            order.Amount = 100;
            order.ClientId = 200;
            order.ClientName = "Bob's FoodMart";
            order.BookedDate = new DateTime(2019, 01, 12);
            order.CreatedDate = new DateTime(2019, 01, 01);
            order.CurrencyIsoCode = "USD";
            order.Adjustments.Add(new Adjustment
            { Amount = -20, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01) });

            var invoice = new Invoice();
            invoice.InvoiceId = 1;
            invoice.OrderId = 123456;
            invoice.Amount = 100;
            invoice.CreatedDate = new DateTime(2019, 01, 01);

            order.Invoices.Add(invoice);






            var orderService = new OrderService();

            //
            // Act
            //
            var ex = Assert.Throws<Exception>(() => orderService.PayInvoice(order, 2));

            //
            // Assert
            //
            Assert.Equal("Invalid InvoiceId", ex.Message);
        }

        [Fact]
        public void TestOrderServicePayInvoice_JoeInAccounting()
        {
            //
            // Arrange
            //
            var order = new Order();
            order.OrderId = 123456;
            order.Amount = 100;
            order.ClientId = 200;
            order.ClientName = "Bob's FoodMart";
            order.BookedDate = new DateTime(2019, 01, 12);
            order.CreatedDate = new DateTime(2019, 01, 01);
            order.CurrencyIsoCode = "USD";
            order.Adjustments.Add(new Adjustment
            { Amount = -20, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01) });

            var invoice = new Invoice();
            invoice.InvoiceId = 1;
            invoice.OrderId = 123456;
            invoice.Amount = 0;
            invoice.CreatedDate = new DateTime(2019, 01, 01);

          
            order.Invoices.Add(invoice);
          






            var orderService = new OrderService();

            //
            // Act
            //
            var payment = orderService.PayInvoice(order, 1);

            //
            // Assert
            //
            Assert.Equal(0, order.Payments.Count);
        }
        [Fact]
        public void TestOrderServicePayInvoice()
        {
            //
            // Arrange
            //
            var order = new Order();
            order.OrderId = 123456;
            order.Amount = 100;
            order.ClientId = 200;
            order.ClientName = "Bob's FoodMart";
            order.BookedDate = new DateTime(2019, 01, 12);
            order.CreatedDate = new DateTime(2019, 01, 01);
            order.CurrencyIsoCode = "USD";
            order.Adjustments.Add(new Adjustment
            { Amount = -20, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01) });

            var invoice = new Invoice();
            invoice.InvoiceId = 1;
            invoice.OrderId = 123456;
            invoice.Amount = 50;
            invoice.CreatedDate = new DateTime(2019, 01, 01);

           

            order.Invoices.Add(invoice);

         
           






            var orderService = new OrderService();

            //
            // Act
            //
            var payment = orderService.PayInvoice(order, 1);

            //
            // Assert
            //
            Assert.Equal(1, order.Payments.Count);
            Assert.Equal(payment.Amount, invoice.Amount);

        }

        [Fact]
        public void TestOrderServiceValidOrder_ThrowsWhenAmountIsNegative()
        {
            //
            // Arrange
            //
            var order = new Order();
            order.OrderId = 123456;
            order.Amount = -1;
            order.ClientId = 200;
            order.ClientName = "Bob's FoodMart";
            order.BookedDate = new DateTime(2019, 01, 12);
            order.CreatedDate = new DateTime(2019, 01, 01);
            order.CurrencyIsoCode = "USD";
            order.Adjustments.Add(new Adjustment
                {Amount = -20, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01)});

            var orderService = new OrderService();

            //
            // Act
            //
            var ex = Assert.Throws<Exception>(() => orderService.ValidateOrder(order));

            //
            // Assert
            //
            Assert.Equal("Order amount cannot be negative", ex.Message);
        }

        [Fact]
        public void TestOrderServiceValidOrder_ThrowsWhenAmountWithAdjustmentsIsNegative()
        {
            //
            // Arrange
            //
            var order = new Order();
            order.OrderId = 123456;
            order.Amount = 100;
            order.ClientId = 200;
            order.ClientName = "Bob's FoodMart";
            order.BookedDate = new DateTime(2019, 01, 12);
            order.CreatedDate = new DateTime(2019, 01, 01);
            order.CurrencyIsoCode = "USD";
            order.Adjustments.Add(new Adjustment
                {Amount = -120, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01)});

            var orderService = new OrderService();

            //
            // Act
            //
            var ex = Assert.Throws<Exception>(() => orderService.ValidateOrder(order));

            //
            // Assert
            //
            Assert.Equal("Order adjustments make total amount negative", ex.Message);
        }

        [Fact]
        public void TestOrderServiceValidOrder_ThrowsWhenOrderIsDeleted()
        {
            //
            // Arrange
            //
            var order = new Order();
            order.OrderId = 123456;
            order.Amount = 100;
            order.ClientId = 200;
            order.ClientName = "Bob's FoodMart";
            order.BookedDate = new DateTime(2019, 01, 12);
            order.CreatedDate = new DateTime(2019, 01, 01);
            order.CurrencyIsoCode = "USD";
            order.Adjustments.Add(new Adjustment
                {Amount = -20, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01)});
            order.IsDeleted = true;

            var orderService = new OrderService();

            //
            // Act
            //
            var ex = Assert.Throws<Exception>(() => orderService.ValidateOrder(order));

            //
            // Assert
            //
            Assert.Equal("Order has been deleted", ex.Message);
        }


        [Fact]
        public void TestOrder_AmountPaidIsZeroWhenZeroDollarPaymentsMade()
        {
            //
            // Arrange
            //
            var order = new Order();
            order.OrderId = 123456;
            order.Amount = 100;
            order.ClientId = 200;
            order.ClientName = "Bob's FoodMart";
            order.BookedDate = new DateTime(2019, 01, 12);
            order.CreatedDate = new DateTime(2019, 01, 01);
            order.CurrencyIsoCode = "USD";
            order.Adjustments.Add(new Adjustment() { Amount = -20, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01) });
            order.Invoices.Add(new Invoice() { Amount = 0, OrderId = 123456, InvoiceId = 1, CreatedDate = DateTime.Now });

            var orderService = new OrderService();

            //
            // Act
            //
            var payment = orderService.PayInvoice(order, 1);

            //
            // Assert
            //
            Assert.Equal(0, order.TotalPayments);
        }


        [Fact]
        public void TestPayInvoice_CreatesPaymentOfInvoiceAmount()
        {
            //
            // Arrange
            //
            var order = new Order();
            order.OrderId = 123456;
            order.Amount = 100;
            order.ClientId = 200;
            order.ClientName = "Bob's FoodMart";
            order.BookedDate = new DateTime(2019, 01, 12);
            order.CreatedDate = new DateTime(2019, 01, 01);
            order.CurrencyIsoCode = "USD";
            order.Adjustments.Add(new Adjustment() { Amount = -20, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01) });
            order.Invoices.Add(new Invoice() { Amount = 10, OrderId = 123456, InvoiceId = 1, CreatedDate = DateTime.Now });

            var orderService = new OrderService();

            //
            // Act
            //
            var payment = orderService.PayInvoice(order, 1);

            //
            // Assert
            //
            Assert.Equal(10, payment.Amount);
        }


        [Fact]
        public void TestPayInvoice_ThrowsExceptionWhenInvoiceNotFound()
        {
            //
            // Arrange
            //
            var order = new Order();
            order.OrderId = 123456;
            order.Amount = 100;
            order.ClientId = 200;
            order.ClientName = "Bob's FoodMart";
            order.BookedDate = new DateTime(2019, 01, 12);
            order.CreatedDate = new DateTime(2019, 01, 01);
            order.CurrencyIsoCode = "USD";
            order.Adjustments.Add(new Adjustment() { Amount = -20, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01) });

            var orderService = new OrderService();

            //
            // Act
            //
            var ex = Assert.Throws<Exception>(() => orderService.PayInvoice(order, 1));

            //
            // Assert
            //
            Assert.Equal("Invalid InvoiceId", ex.Message);

        }
    }
}
