namespace ReservationLogicTesting;

[TestClass]
public class TestReservationLogic
{
    [TestMethod]
    [DataRow(10, 9, 10)]
    public void TestCreateTableTest(int chairs, int minCapacity, int maxCapacity)
    {
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(chairs, minCapacity, maxCapacity, "Regular");

        Assert.AreEqual(10, table.Chairs);
        Assert.AreEqual(9, table.MinCapacity);
        Assert.AreEqual(10, table.MaxCapacity);
        // check if table is added to list / json file
        Assert.AreEqual(table.Id, reservationlogic._tables.Last().Id);
    }

    // [TestMethod]
    // public void TestCreateReservation()
    // {
    //     // create table to assign reservation to
    //     ReservationLogic reservationlogic = new();

    //     TableModel table = reservationlogic.Createtable(10, 9, 10, "Regular");
    //     // create reservation and assign to table
    //     ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "Regular", null);
    //     Assert.AreEqual(reservationlogic._reservations.Last().Id, reservation.Id);
    //     Assert.AreEqual("Yapper", reservation.Name);
    //     Assert.AreEqual(10, reservation.ClientID);
    //     Assert.AreEqual(10, reservation.HowMany);
    //     Assert.AreEqual(DateTime.Today, reservation.Date);
    //     // check if table is assigened to table
    //     // assert if the id of the reservation is the same as the id of the last item in the table.reservations to see if assgigned to table
    //     Assert.AreEqual(table.Reservations.Last().Id, reservation.Id);

    // }
    [TestMethod]
    public void TestGetTableByID()
    {
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "Regular");
        Assert.AreEqual(reservationlogic.GetTableById(reservationlogic._tables.Last().Id), table);

    }
    [TestMethod]
    public void TestGetReservationByID()
    {
        ReservationLogic reservationlogic = new();
        ReservationModel reservation = reservationlogic.Create_reservation(10, "Yapper", 10, 10, DateTime.Today, "Regular", null, false);
        Assert.AreEqual(reservationlogic.GetReservationById(reservationlogic._reservations.Last().Id).Id, reservation.Id);

    }

    [TestMethod]
    // today
    // you can only book 3 months ahead
    // "==" 3 months
    // DateTime.Today()  <
    // :)
    public void TestIsValidDateTrue()
    {
        DateTime today = DateTime.Today;
        DateTime oneday = DateTime.Today.AddDays(1);
        DateTime twoday = DateTime.Today.AddDays(2);
        DateTime threemonth = DateTime.Today.AddMonths(3);

        ReservationLogic reservationLogic = new();

        Assert.AreEqual(true, reservationLogic.IsValidDate(today));
        Assert.AreEqual(true, reservationLogic.IsValidDate(oneday));
        Assert.AreEqual(true, reservationLogic.IsValidDate(twoday));
        Assert.AreEqual(true, reservationLogic.IsValidDate(threemonth));
    }

    [TestMethod]
    // DateTime.Today( ) >
    // -1
    // -2
    // > 3 months 1 day
    // > 3 months 2 day
    // :(
    public void TestIsValidDateFalse()
    {
        DateTime oneday = DateTime.Today.AddDays(-1);
        DateTime twoday = DateTime.Today.AddDays(-2);
        DateTime threemonthoneday = DateTime.Today.AddMonths(3).AddDays(1);
        DateTime threemonthtwoday = DateTime.Today.AddMonths(3).AddDays(2);
        ReservationLogic reservationLogic = new();

        Assert.AreEqual(false, reservationLogic.IsValidDate(oneday));
        Assert.AreEqual(false, reservationLogic.IsValidDate(twoday));
        Assert.AreEqual(false, reservationLogic.IsValidDate(threemonthoneday));
        Assert.AreEqual(false, reservationLogic.IsValidDate(threemonthtwoday));

    }

    [TestMethod]
    // :)
    public void RemoveReservationByID_ValidreservationId_RemovedReservation()
    {
        //arrange
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "Regular");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "Regular", null, false);

        //act
        reservationlogic.RemoveReservationByID(reservation.Id);

        //assert
        Assert.AreEqual(false, table.Reservations.Contains(reservation));
    }
    [TestMethod]
    //:(
    public void RemoveReservationByID_ValidreservationId_null()
    {
        //arrange
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "Regular");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "Regular", null, false);

        //act
        ReservationModel remove = reservationlogic.RemoveReservationByID(2000000000);

        //assert
        Assert.AreEqual(null, remove);
    }

    [TestMethod]
    // type of reservation --------
    //:)
    public void TypeOfReservation_ValidTableId_HotSeat()
    {
        //arrange 
        ReservationLogic reservationlogic = new();

        //act
        string type = reservationlogic.TypeOfReservation(23);
        //assert
        Assert.AreEqual("HotSeat", type);
    }
    [TestMethod]
    //:)
    public void TypeOfReservation_ValidTableId_Regular()
    {
        //arrange 
        ReservationLogic reservationlogic = new();

        //act
        string type = reservationlogic.TypeOfReservation(1);
        //assert
        Assert.AreEqual("Regular", type);
    }

    [TestMethod]
    //:(
    public void TypeOfReservation_inValidTableId_null()
    {
        //arrange 
        ReservationLogic reservationlogic = new();

        //act
        string type = reservationlogic.TypeOfReservation(2000);
        //assert
        Assert.AreEqual(null, type);
    }

    // [TestMethod]
    // [DataRow("1")]
    // [DataRow("2")]
    // [DataRow("3")]
    // [DataRow("4")]

    // //:)
    // public void TimSlotChooser_ValidID_Timeslot(string id)
    // {
    //     //arrange 


    //     //act
    //     string TimeSlot = ReservationLogic.TimSlotChooser(id);
    //     //assert

    //     Assert.AreNotEqual(null, TimeSlot); // timeslot would be null if input was different
    // }
    // [TestMethod]
    // [DataRow("0")]
    // [DataRow("5")]
    // [DataRow("0")]
    // [DataRow(null)]

    // //:(
    // public void TimSlotChooser_InValidID_Null(string id)
    // {
    //     //arrange 


    //     //act
    //     string TimeSlot = ReservationLogic.TimSlotChooser(id);
    //     //assert
    //     Assert.AreEqual(null, TimeSlot);
    // }


    [TestMethod]
    // UnassignTable
    // :)
    public void UnassignTable_ValidReservationID_RemovedReservation()
    {
        //arrange
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "Regular");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "Regular", null, false);

        //act
        reservationlogic.UnassignTable(reservation.Id);

        //assert

        Assert.AreEqual(true, table.Reservations.Count == 0);
    }
    [TestMethod]
    // UnassignTable
    // :(
    public void UnassignTable_InvalidReservationID_RemovedReservation()
    {
        //arrange
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "Regular");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "Regular", null, false);

        //act
        reservationlogic.UnassignTable(0);

        //assert
        Assert.AreEqual(true, table.Reservations.Count == 1);
    }


    [TestMethod]
    public void CreateReceipt()
    {
        //arrange
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "Regular");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "Regular", null, false);

        //act
        ReceiptModel receipt = reservationlogic.CreateReceipt(reservation, 100, "10", "haha", [], [table.Id]);

        //assert
        Assert.AreEqual(reservationlogic._receipts.Last().Id, receipt.Id);
    }

    [TestMethod]
    public void GetReceiptById_ValidreservationID_Receipt()
    // :)
    {
        //arrange
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "Regular");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "Regular", null, false);
        ReceiptModel receipt = reservationlogic.CreateReceipt(reservation, 100, "10", "haha", [], [table.Id]);

        //act
        ReceiptModel getReceipt = reservationlogic.GetReceiptById(reservation.Id);

        //assert
        Assert.AreEqual(receipt.Id, getReceipt.Id);
    }
    [TestMethod]
    public void GetReceiptById_InvalidReservationID_Null()
    // :(
    {
        //arrange
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "Regular");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "Regular", null, false);
        ReceiptModel receipt = reservationlogic.CreateReceipt(reservation, 100, "10", "haha", [], [table.Id]);

        //act
        ReceiptModel getReceipt = reservationlogic.GetReceiptById(0);

        //assert
        Assert.AreEqual(null, getReceipt);
    }

    // [TestMethod]
    // public void CheckDate()
    // {
    //     //arrange
    //     ReservationLogic reservationlogic = new();

    //     //act
    //     bool check = reservationlogic.CheckDate(DateTime.Today);

    //     //assert
    //     Assert.AreEqual(true, check);
    // }


    [TestMethod]
    public void Create_reservationHotSeat()
    {
        //arrange
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "HotSeat");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "HotSeat", null, false);

        //act
        ReceiptModel receipt = reservationlogic.CreateReceipt(reservation, 100, "10", "haha", [], [table.Id]);

        //assert
        Assert.AreEqual(reservationlogic._receipts.Last().Id, receipt.Id);
    }
    [TestMethod]
    public void CreateReceiptHotSeat()
    {
        //arrange
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "HotSeat");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "HotSeat", null, false);

        //act
        ReceiptModel receipt = reservationlogic.CreateReceipt(reservation, 100, "10", "haha", [], [table.Id]);

        //assert
        Assert.AreEqual(reservationlogic._receipts.Last().Id, receipt.Id);
    }

    [TestMethod]

    public void GetTablesByReservation()
    {
        //arrange
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "HotSeat");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "HotSeat", null, false);

        //act
        List<TableModel> tables = reservationlogic.GetTablesByReservation(reservation);

        //assert
        Assert.AreEqual(table, tables[0]);
    }

    [TestMethod]
    // :)
    public void ModifyReservation_ValidInput_InfoChanged()
    {
        //arrange
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "HotSeat");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "HotSeat", null, false);

        //act
        reservationlogic.ModifyReservation(reservation, 10);
        reservationlogic.ModifyReservation(reservation, DateTime.Today);
        reservationlogic.ModifyReservation(reservation, "ola");

        //assert
        Assert.AreEqual(10, reservation.HowMany);
        Assert.AreEqual(DateTime.Today, reservation.Date);
        Assert.AreEqual("ola", reservation.TimeSlot);

    }

    [TestMethod]
    // :)
    public void ModifyReservation_InValidInput_InfoNotChanged()
    {
        //arrange
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "HotSeat");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "HotSeat", null, false);

        //act
        reservationlogic.ModifyReservation(reservation, 10.10);
        reservationlogic.ModifyReservation(reservation, 'h');
        reservationlogic.ModifyReservation(reservation, true);


        //assert
        Assert.AreEqual(reservation.HowMany, reservation.HowMany);
        Assert.AreEqual(reservation.Date, reservation.Date);
        Assert.AreEqual(reservation.TimeSlot, reservation.TimeSlot);

    }

    [TestMethod]
    public void ModifyReservation()
    {
        //arrange
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "HotSeat");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "HotSeat", null, false);

        //act
        reservationlogic.ModifyReservation(reservation, [0], 0, DateTime.Today, "0", "0", false);


        //assert
        Assert.AreEqual(true, reservation.TableID.Contains(0));
        Assert.AreEqual(0, reservation.HowMany);
        Assert.AreEqual(DateTime.Today, reservation.Date);
        Assert.AreEqual("0", reservation.TimeSlot);
        Assert.AreEqual("0", reservation.TypeOfReservation);
        Assert.AreEqual(false, reservation.FoodOrdered);
    }


//     [TestMethod]
//     // ModifyReceipt
//     public void ModifyReceipt()
//     {
//         // /arrange
//         ReservationLogic reservationlogic = new();

//         TableModel table = reservationlogic.Createtable(10, 9, 10, "HotSeat");
//         // create reservation and assign to table
//         ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "HotSeat", "Yesterdat", false);
//         reservationlogic.ModifyReservation(reservation, [0], 0, DateTime.Today, "0", "0", false);

//         //act
//         ReceiptModel receipt = reservationlogic.ModifyReceipt(reservation, null);
//         // assert
//         Assert.AreEqual(receipt.ReservationId,reservation.Id);
//         Assert.AreEqual(receipt.ClientId,reservation.ClientID);
//         Assert.AreEqual(receipt.Date, DateTime.Today);
//         Assert.AreEqual(receipt.TimeSlot, "0");
//         Assert.AreEqual(receipt.TypeOfReservation, "0");
//         Assert.AreEqual(receipt.TableID, "0");
       




//     }
}




