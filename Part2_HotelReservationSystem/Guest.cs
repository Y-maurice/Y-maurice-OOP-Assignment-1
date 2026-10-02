namespace Part2_HotelReservationSystem;

public class Guest
{
    private readonly List<Reservation> _reservations = [];

    public int GuestId { get; }

    public string FullName { get; }

    public string PhoneNumber { get; }

    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

    public Guest(int guestId, string fullName, string phoneNumber)
    {
        if (guestId <= 0)
            throw new ArgumentOutOfRangeException(nameof(guestId), guestId,
                "Guest ID must be greater than zero.");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name cannot be null, empty or whitespace.",
                nameof(fullName));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be null, empty or whitespace.",
                nameof(phoneNumber));

        GuestId = guestId;
        FullName = fullName.Trim();
        PhoneNumber = phoneNumber.Trim();
    }

    internal void AddReservation(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);

        if (!ReferenceEquals(reservation.Guest, this))
            throw new InvalidOperationException(
                $"Reservation {reservation.ReservationId} belongs to guest " +
                $"{reservation.Guest.GuestId}, not guest {GuestId}.");

        if (_reservations.Contains(reservation))
            throw new InvalidOperationException(
                $"Reservation {reservation.ReservationId} is already in guest {GuestId}'s history.");

        _reservations.Add(reservation);
    }

    public override string ToString() =>
        $"Guest ID: {GuestId}, Name: {FullName}, Phone: {PhoneNumber}";
}