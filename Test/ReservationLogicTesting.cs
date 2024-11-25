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
    [DataRow("29/11/2024")]
    [DataRow("01/01/2025")]
    // you can only book 3 months ahead

    // date of test: 12/11/2024
    public void TestIsValidDateTrue(string str_Date)
    {
        ReservationLogic reservationLogic = new();
        DateTime date = Convert.ToDateTime(str_Date);
        Assert.AreEqual(true, reservationLogic.IsValidDate(date));
    }

    [TestMethod]
    [DataRow("01/01/2022")]
    [DataRow("05/05/2025")]

    public void TestIsValidDateFalse(string str_Date)
    {
        ReservationLogic reservationLogic = new();
        DateTime date = Convert.ToDateTime(str_Date);
        Assert.AreEqual(false, reservationLogic.IsValidDate(date));
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



}