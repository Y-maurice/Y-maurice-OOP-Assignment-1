using Part2_HotelReservationSystem.Enums;

namespace Part2_HotelReservationSystem;

public class Reservation
{
    private readonly decimal _rateAtBooking;
    public int ReservationId { get; }

    public DateOnly CheckInDate { get; }
    public DateOnly CheckOutDate { get; }

    public Room Room { get; }

    public Guest Guest { get; }

    public ReservationStatus Status { get; private set; }

    public int Nights => CheckOutDate.DayNumber - CheckInDate.DayNumber;

    public decimal TotalCost => Nights * _rateAtBooking;

    public bool IsActive => Status is ReservationStatus.Pending
                                   or ReservationStatus.Confirmed
                                   or ReservationStatus.CheckedIn;

    public Reservation(int reservationId, Guest guest, Room room,
                          DateOnly checkInDate, DateOnly checkOutDate)
    {
        if (reservationId <= 0)
            throw new ArgumentOutOfRangeException(nameof(reservationId), reservationId,
                "Reservation ID must be greater than zero.");

        ArgumentNullException.ThrowIfNull(guest);
        ArgumentNullException.ThrowIfNull(room);

        if (checkOutDate <= checkInDate)
            throw new ArgumentException(
                $"Check-out date ({checkOutDate}) must be after check-in date ({checkInDate}).",
                nameof(checkOutDate));

        if (room.IsUnderMaintenance)
            throw new InvalidOperationException(
                $"Room {room.RoomNumber} is under maintenance and cannot be booked.");

        ReservationId = reservationId;
        Guest = guest;
        Room = room;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        _rateAtBooking = room.NightlyRate;
        Status = ReservationStatus.Pending;
    }


    private void RequireStatus(ReservationStatus required, string action)
    {
        if (Status != required)
            throw new InvalidOperationException(
                $"Reservation {ReservationId} cannot be {action} while {Status}. " +
                $"It must be {required}.");
    }
    // validates the reservation is in the expected state before allowing state transition

    public bool OverlapsWith(DateOnly checkIn, DateOnly checkOut) =>
        checkIn < CheckOutDate && CheckInDate < checkOut;

    public void Confirm()
    {
        RequireStatus(ReservationStatus.Pending, "confirmed");
        Status = ReservationStatus.Confirmed;
    }

    public void CheckIn()
    {
        RequireStatus(ReservationStatus.Confirmed, "checked in");
        Status = ReservationStatus.CheckedIn;
    }

    public void CheckOut()
    {
        RequireStatus(ReservationStatus.CheckedIn, "checked out");
        Status = ReservationStatus.CheckedOut;
    }

    public void Cancel()
    {
        if (Status is not (ReservationStatus.Pending or ReservationStatus.Confirmed))
            throw new InvalidOperationException(
                $"Reservation {ReservationId} cannot be cancelled while {Status}. " +
                "Only Pending or Confirmed reservations can be cancelled.");

        Status = ReservationStatus.Cancelled;
    }


    public override string ToString()
    {
        return $"Reservation ID: {ReservationId}, Check-in: {CheckInDate}, Check-out: {CheckOutDate}, Room: {Room}, Guest: {Guest}";
    }


}
