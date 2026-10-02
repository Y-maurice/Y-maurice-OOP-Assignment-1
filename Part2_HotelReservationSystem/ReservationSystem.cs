using Part2_HotelReservationSystem.Enums;

namespace Part2_HotelReservationSystem;


public class ReservationSystem
{

    private readonly List<Guest> _guests = [];
    private readonly List<Room> _rooms = [];
    private readonly List<Reservation> _reservations = [];

    // The system generates IDs so they are always unique
    // A counter only moves forward after an object was created successfully.
    private int _nextGuestId = 1;
    private int _nextReservationId = 1;

    // Read-only views for outside code. AsReadOnly() returns a real wrapper,
    // so callers cannot cast it back to a List and modify it.
    public IReadOnlyList<Guest> Guests => _guests.AsReadOnly();
    public IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();
    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();


    // Lookups
    // The Find methods throw when nothing is found, so every action method
    // reports a missing item the same way. TryFindRoom exists for the one case
    // where "not found" is the expected outcome (AddRoom).

    private Guest FindGuestById(int guestId)
    {
        foreach (var guest in _guests)
        {
            if (guest.GuestId == guestId)
                return guest;
        }

        throw new KeyNotFoundException($"Guest {guestId} does not exist.");
    }

    private bool TryFindRoom(int roomNumber, out Room? room)
    {
        foreach (var r in _rooms)
        {
            if (r.RoomNumber == roomNumber)
            {
                room = r;
                return true;
            }
        }

        room = null;
        return false;
    }

    private Room FindRoom(int roomNumber)
    {
        if (!TryFindRoom(roomNumber, out var room))
            throw new KeyNotFoundException($"Room {roomNumber} does not exist.");

        return room!;
    }

    private Reservation FindReservationById(int reservationId)
    {
        foreach (var reservation in _reservations)
        {
            if (reservation.ReservationId == reservationId)
                return reservation;
        }

        throw new KeyNotFoundException($"Reservation {reservationId} does not exist.");
    }

    // ---------------------------------------------------------------
    // Setting up the hotel: rooms and guests

    public Room AddRoom(int roomNumber, RoomType roomType, decimal nightlyRate)
    {
        // Create first: the Room constructor validates number, type and rate,
        // so bad input fails with its own specific message before anything else
        var room = new Room(roomNumber, roomType, nightlyRate);

        // Room numbers identify rooms, so they must be unique.
        if (TryFindRoom(roomNumber, out _))
            throw new InvalidOperationException($"Room {roomNumber} already exists.");

        _rooms.Add(room);
        return room;
    }

    public Guest RegisterGuest(string fullName, string phoneNumber)
    {
        // The Guest constructor validates the name and phone.
        var guest = new Guest(_nextGuestId, fullName, phoneNumber);

        _guests.Add(guest);

        // Advance the counter only after the guest exists, so a failed
        // registration doesn't waste an ID.
        _nextGuestId++;
        return guest;
    }

    // ---------------------------------------------------------------
    // Room operations (find the room, then let Room enforce its own rules)


    public void ChangeRoomRate(int roomNumber, decimal newRate)
    {
        // Room rejects zero and negative rates.
        FindRoom(roomNumber).ChangeNightlyRate(newRate);
    }

    public void StartRoomMaintenance(int roomNumber)
    {
        FindRoom(roomNumber).StartMaintenance();
    }

    public void EndRoomMaintenance(int roomNumber)
    {
        FindRoom(roomNumber).EndMaintenance();
    }

    // ---------------------------------------------------------------
    // Reservation status operations
    // The Reservation class decides which transitions are legal and throws
    // otherwise, so the system does not repeat those rules.


    public void ConfirmReservation(int reservationId)
    {
        FindReservationById(reservationId).Confirm();
    }

    public void CheckInReservation(int reservationId)
    {
        FindReservationById(reservationId).CheckIn();
    }

    public void CheckOutReservation(int reservationId)
    {
        FindReservationById(reservationId).CheckOut();
    }

    public void CancelReservation(int reservationId)
    {
        FindReservationById(reservationId).Cancel();
    }

    // ---------------------------------------------------------------
    // Booking


    public Reservation MakeReservation(int guestId, int roomNumber,
                                       DateOnly checkInDate, DateOnly checkOutDate)
    {
        // 1-2. Both the guest and the room must already exist in the system.
        var guest = FindGuestById(guestId);
        var room = FindRoom(roomNumber);

        // 3. Build the reservation. Its constructor validates the dates and
        //    that the room isn't under maintenance. Nothing is registered yet,
        //    so if this throws, no list or counter has changed.

        var reservation = new Reservation(_nextReservationId, guest, room,
                                          checkInDate, checkOutDate);

        // 4. Double-booking check: another active reservation for the same room
        //    whose dates overlap means the room is taken. Cancelled and
        //    checked-out reservations are not active, so they never block.
        //    Back-to-back stays (check-out day == next check-in day) are allowed.

        foreach (var existing in _reservations)
        {
            if (ReferenceEquals(existing.Room, room)
                && existing.IsActive
                && existing.OverlapsWith(checkInDate, checkOutDate))
            {
                throw new InvalidOperationException(
                    $"Room {roomNumber} is already booked from {existing.CheckInDate} " +
                    $"to {existing.CheckOutDate} (reservation {existing.ReservationId}).");
            }
        }

        // 5 Everything is valid: register the reservation on both sides
        //      and only now move the ID counter forward.

        guest.AddReservation(reservation);
        _reservations.Add(reservation);
        _nextReservationId++;

        return reservation;
    }



    public IReadOnlyList<Reservation> GetReservationsForGuest(int guestId)
    {
        // The guest already keeps its own read-only history.
        return FindGuestById(guestId).Reservations;
    }
}