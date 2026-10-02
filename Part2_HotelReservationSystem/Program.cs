using Part2_HotelReservationSystem.Enums;

namespace Part2_HotelReservationSystem;

public class Program
{
    public static void Main()
    {
        var system = new ReservationSystem();
        var menu = new Menu(system);

        system.AddRoom(101, RoomType.Single, 80m);
        system.AddRoom(102, RoomType.Suite, 200m);

        system.RegisterGuest("guest 1", "01012345678");
        system.RegisterGuest("guest 2", "01198765432");

        menu.Run();
    }
}