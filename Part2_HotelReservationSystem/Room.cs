using Part2_HotelReservationSystem.Enums;

namespace Part2_HotelReservationSystem;

public class Room
{
    public int RoomNumber { get; }

    public RoomType RoomType { get; }

    public decimal NightlyRate { get; private set; }

    public bool IsUnderMaintenance { get; private set; }

    public Room(int roomNumber, RoomType roomType, decimal nightlyRate)
    {
        if (roomNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(roomNumber), roomNumber,
                "Room number must be greater than zero.");

        if (!Enum.IsDefined(roomType))
            throw new ArgumentOutOfRangeException(nameof(roomType), roomType,
                "Room type is not a valid RoomType value.");

        RoomNumber = roomNumber;
        RoomType = roomType;
        NightlyRate = ValidateNightlyRate(nightlyRate);
    }

    private static decimal ValidateNightlyRate(decimal rate) //static when a method doesn't read or write any state (good practice)
    {
        if (rate <= 0)
            throw new ArgumentOutOfRangeException(nameof(rate), rate,
                "Nightly rate must be greater than zero.");

        return rate;
    }

    public void StartMaintenance()
    {
        if (IsUnderMaintenance)
            throw new InvalidOperationException(
                $"Room {RoomNumber} is already under maintenance.");

        IsUnderMaintenance = true;
    }

    public void EndMaintenance()
    {
        if (!IsUnderMaintenance)
            throw new InvalidOperationException(
                $"Room {RoomNumber} is not currently under maintenance.");

        IsUnderMaintenance = false;
    }

    public void ChangeNightlyRate(decimal newRate)
    {


        NightlyRate = ValidateNightlyRate(newRate);
    }

    public override string ToString() =>
    $"Room {RoomNumber} | {RoomType} | Rate: {NightlyRate:F2}/night | " +
    $"Under maintenance: {(IsUnderMaintenance ? "Yes" : "No")}";
}
