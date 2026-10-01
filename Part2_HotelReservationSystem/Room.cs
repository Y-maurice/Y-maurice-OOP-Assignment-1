using Part2_HotelReservationSystem.Enums;

namespace Part2_HotelReservationSystem;

public class Room
{
    public int Number { get; }

    public RoomType RoomType { get; }

    public decimal NightlyRate { get; private set; }

    public bool IsUnderMaintenance { get; private set; }

    public Room(int number, RoomType roomType, decimal nightlyRate, bool isUnderMaintenance = false)
    {
        Number = number;
        RoomType = roomType;
        NightlyRate = ValidateNightlyRate(nightlyRate);

    }

    private decimal ValidateNightlyRate(decimal rate)
    {
        if (rate <= 0)
        {
            throw new InvalidOperationException("nightly rate cannot be less than or equal zero");
        }
        return rate;
    }

    public void StartMaintenance()
    {
        if (IsUnderMaintenance == true)
            throw new InvalidOperationException("room is already being maintaned");

        IsUnderMaintenance = true;
    }

    public void EndMaintenance()
    {
        IsUnderMaintenance = false;
    }

    public void changeNightlyRate(decimal rate)
    {


        NightlyRate = ValidateNightlyRate(rate);
    }

}
