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

    [TestMethod]
    public void TestCreateReservation()
    {
        // create table to assign reservation to
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "Regular");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "Regular", null);
        Assert.AreEqual(reservationlogic._reservations.Last().Id, reservation.Id);
        Assert.AreEqual("Yapper", reservation.Name);
        Assert.AreEqual(10, reservation.ClientID);
        Assert.AreEqual(10, reservation.HowMany);
        Assert.AreEqual(DateTime.Today, reservation.Date);
        // check if table is assigened to table
        // assert if the id of the reservation is the same as the id of the last item in the table.reservations to see if assgigned to table
        Assert.AreEqual(table.Reservations.Last().Id, reservation.Id);

    }
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
        ReservationModel reservation = reservationlogic.Create_reservation(10, "Yapper", 10, 10, DateTime.Today, "Regular", null);
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
    //:)
    public void RemoveReservationByID_ValidreservationId_RemovedReservation()
    {
        //arrange
        ReservationLogic reservationlogic = new();

        TableModel table = reservationlogic.Createtable(10, 9, 10, "Regular");
        // create reservation and assign to table
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "Regular", null);

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
        ReservationModel reservation = reservationlogic.Create_reservation(table.Id, "Yapper", 10, 10, DateTime.Today, "Regular", null);

        //act
        ReservationModel remove = reservationlogic.RemoveReservationByID(2000);

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

    [TestMethod]
    [DataRow("1")]
    [DataRow("2")]
    [DataRow("3")]
    [DataRow("4")]

    //:)
    public void TimSlotChooser_ValidID_Timeslot(string id)
    {
        //arrange 


        //act
        string TimeSlot = ReservationLogic.TimSlotChooser(id);
        //assert

        Assert.AreNotEqual(null, TimeSlot); // timeslot would be null if input was different
    }
    [TestMethod]
    [DataRow("0")]
    [DataRow("5")]
    [DataRow("0")]
    [DataRow(null)]

    //:(
    public void TimSlotChooser_InValidID_Null(string id)
    {
        //arrange 


        //act
        string TimeSlot = ReservationLogic.TimSlotChooser(id);
        //assert
        Assert.AreEqual(null, TimeSlot);
    }
}





