Imports System.Data
Imports Microsoft.VisualBasic
Imports SiExProECommerce


Public Class ECommerceDALC
    Public Shared Function UpdateOrderStatus(orderId As Integer, orderStatusId As Integer) As Boolean
        Dim dbContext As New GombarECommerceEntities

        Dim order = dbContext.Orders.FirstOrDefault(Function(x) x.Id = orderId)

        If order IsNot Nothing Then
            order.OrderStatusId = orderStatusId
            dbContext.Entry(order).State = EntityState.Modified

            Dim orderNote As New OrderNote()

            With orderNote
                .CreatedOnUtc = DateTime.Now
                .OrderId = orderId
                .Note = "Orden ha sido modificada. Nuevo status: Procesando"
                .DisplayToCustomer = False
            End With

            dbContext.OrderNotes.Add(orderNote)

            Dim logItem = New ActivityLog()

            With logItem
                .ActivityLogTypeId = 101
                .EntityId = orderId
                .EntityName = "Order"
                .CustomerId = order.CustomOrderNumber
                .Comment = "Orden modificada (Numero de Orden = " & orderId & "). Vea las notas de la orden para mas detalles."
                .CreatedOnUtc = DateTime.Now
                .IpAddress = ""
            End With

            dbContext.ActivityLogs.Add(logItem)

            Return dbContext.SaveChanges() > 0
        End If

        Return False

    End Function

    Public Shared Function AddShipmnet(orderId As Integer, idEnvio As Integer) As Boolean
        Dim dbContext As New GombarECommerceEntities

        Dim order = dbContext.Orders.FirstOrDefault(Function(x) x.Id = orderId)

        Dim orderItems = dbContext.OrderItems.Where(Function(x) x.Id = orderId)

        If order IsNot Nothing And orderItems IsNot Nothing Then
            Dim totalWeight = 0

            Dim shipment As New Shipment()

            With shipment
                .AdminComment = "Orden Exportada"
                .OrderId = orderId
                .TrackingNumber = idEnvio
                .TotalWeight = Nothing
                .CreatedOnUtc = DateTime.Now
            End With

            dbContext.Shipments.Add(shipment)
            dbContext.SaveChanges()

            For Each orderItem In orderItems
                Dim shipmentItem As New ShipmentItem()
                totalWeight = totalWeight + (orderItem.ItemWeight * orderItem.Quantity)

                With shipmentItem
                    .ShipmentId = shipment.Id
                    .OrderItemId = orderItem.Id
                    .Quantity = orderItem.Quantity
                    .WarehouseId = 0
                End With

                dbContext.ShipmentItems.Add(shipmentItem)
            Next

            shipment.TotalWeight = totalWeight
            shipment.ShippedDateUtc = DateTime.Now
            dbContext.Entry(shipment).State = EntityState.Modified

            Dim orderNote As New OrderNote()

            With orderNote
                .CreatedOnUtc = DateTime.Now
                .OrderId = orderId
                .Note = "Orden ha sido enviada"
                .DisplayToCustomer = False
            End With

            dbContext.OrderNotes.Add(orderNote)

            Dim logItem = New ActivityLog()

            With logItem
                .ActivityLogTypeId = 101
                .EntityId = orderId
                .EntityName = "Order"
                .CustomerId = order.CustomOrderNumber
                .Comment = "Orden modificada (Numero de Orden = " & orderId & "). Vea las notas de la orden para mas detalles."
                .CreatedOnUtc = DateTime.Now
                .IpAddress = ""
            End With

            dbContext.ActivityLogs.Add(logItem)

            order.ShippingStatusId = ECommerceShippingStatusEnum.Shipped

        End If

        Return False
    End Function


End Class
