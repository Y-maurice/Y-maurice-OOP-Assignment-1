namespace Part2_HotelReservationSystem;


public class Menu
{
    private readonly ReservationSystem _system;

    public Menu(ReservationSystem system)
    {
        ArgumentNullException.ThrowIfNull(system);
        _system = system;
    }

    public void Run()
    {
        bool running = true;

        while (running)
        {
            PrintMenu();
            int choice = InputHelper.ReadIntInRange("Choose an option: ", 0, 14);
            Console.WriteLine();

            if (choice == 0)
            {
                running = false;
                Console.WriteLine("Goodbye!");
                continue;
            }

            try
            {
                Execute(choice);
            }
            // The system and models signal rule violations with these exception types

            catch (Exception ex) when (ex is ArgumentException
                                          or InvalidOperationException
                                          or KeyNotFoundException)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("========== Hotel Reservation System ==========");
        Console.WriteLine(" 1) Add a room");
        Console.WriteLine(" 2) Register a guest");
        Console.WriteLine(" 3) Make a reservation");
        Console.WriteLine(" 4) Confirm a reservation");
        Console.WriteLine(" 5) Check in a reservation");
        Console.WriteLine(" 6) Check out a reservation");
        Console.WriteLine(" 7) Cancel a reservation");
        Console.WriteLine(" 8) Change a room's nightly rate");
        Console.WriteLine(" 9) Start room maintenance");
        Console.WriteLine("10) End room maintenance");
        Console.WriteLine("11) Show a guest's reservations");
        Console.WriteLine("12) Show all reservations");
        Console.WriteLine("13) Show all rooms");
        Console.WriteLine("14) Show all guests");
        Console.WriteLine(" 0) Exit");
        Console.WriteLine("==============================================");
    }

    private void Execute(int choice)
    {
        switch (choice)
        {
            case 1: AddRoom(); break;
            case 2: RegisterGuest(); break;
            case 3: MakeReservation(); break;
            case 4: ConfirmReservation(); break;
            case 5: CheckInReservation(); break;
            case 6: CheckOutReservation(); break;
            case 7: CancelReservation(); break;
            case 8: ChangeRoomRate(); break;
            case 9: StartMaintenance(); break;
            case 10: EndMaintenance(); break;
            case 11: ShowGuestReservations(); break;
            case 12: PrintReservations(_system.Reservations); break;
            case 13: ShowRooms(); break;
            case 14: ShowGuests(); break;
        }
    }

    // ---------- Setup ----------

    private void AddRoom()
    {
        int number = InputHelper.ReadInt("Room number: ");
        var type = InputHelper.ReadRoomType();
        decimal rate = InputHelper.ReadDecimal("Nightly rate: ");

        Room room = _system.AddRoom(number, type, rate);
        Console.WriteLine($"Room added: {room}");
    }

    private void RegisterGuest()
    {
        string name = InputHelper.ReadNonEmptyString("Full name: ");
        string phone = InputHelper.ReadNonEmptyString("Phone number: ");

        Guest guest = _system.RegisterGuest(name, phone);
        Console.WriteLine($"Guest registered: {guest}");
    }

    // ---------- Reservations ----------

    private void MakeReservation()
    {
        int guestId = InputHelper.ReadInt("Guest ID: ");
        int roomNumber = InputHelper.ReadInt("Room number: ");
        DateOnly checkIn = InputHelper.ReadDate("Check-in date");
        DateOnly checkOut = InputHelper.ReadDate("Check-out date");

        Reservation reservation = _system.MakeReservation(guestId, roomNumber, checkIn, checkOut);
        Console.WriteLine($"Reservation created: {reservation}");
    }

    private void ConfirmReservation()
    {
        int id = InputHelper.ReadInt("Reservation ID: ");
        _system.ConfirmReservation(id);
        Console.WriteLine($"Reservation {id} confirmed.");
    }

    private void CheckInReservation()
    {
        int id = InputHelper.ReadInt("Reservation ID: ");
        _system.CheckInReservation(id);
        Console.WriteLine($"Reservation {id} checked in.");
    }

    private void CheckOutReservation()
    {
        int id = InputHelper.ReadInt("Reservation ID: ");
        _system.CheckOutReservation(id);
        Console.WriteLine($"Reservation {id} checked out.");
    }

    private void CancelReservation()
    {
        int id = InputHelper.ReadInt("Reservation ID: ");
        _system.CancelReservation(id);
        Console.WriteLine($"Reservation {id} cancelled.");
    }

    // ---------- Rooms ----------

    private void ChangeRoomRate()
    {
        int roomNumber = InputHelper.ReadInt("Room number: ");
        decimal newRate = InputHelper.ReadDecimal("New nightly rate: ");

        _system.ChangeRoomRate(roomNumber, newRate);
        Console.WriteLine($"Room {roomNumber} nightly rate changed to {newRate:F2}.");
    }

    private void StartMaintenance()
    {
        int roomNumber = InputHelper.ReadInt("Room number: ");
        _system.StartRoomMaintenance(roomNumber);
        Console.WriteLine($"Room {roomNumber} is now under maintenance.");
    }

    private void EndMaintenance()
    {
        int roomNumber = InputHelper.ReadInt("Room number: ");
        _system.EndRoomMaintenance(roomNumber);
        Console.WriteLine($"Room {roomNumber} maintenance ended.");
    }

    // ---------- Display ----------

    private void ShowGuestReservations()
    {
        int guestId = InputHelper.ReadInt("Guest ID: ");
        PrintReservations(_system.GetReservationsForGuest(guestId));
    }

    private void ShowRooms()
    {
        if (_system.Rooms.Count == 0)
        {
            Console.WriteLine("No rooms yet.");
            return;
        }

        foreach (var room in _system.Rooms)
            Console.WriteLine(room);
    }

    private void ShowGuests()
    {
        if (_system.Guests.Count == 0)
        {
            Console.WriteLine("No guests yet.");
            return;
        }

        foreach (var guest in _system.Guests)
            Console.WriteLine(guest);
    }

    private static void PrintReservations(IReadOnlyList<Reservation> reservations)
    {
        if (reservations.Count == 0)
        {
            Console.WriteLine("No reservations found.");
            return;
        }

        foreach (var reservation in reservations)
            Console.WriteLine(reservation);
    }
}