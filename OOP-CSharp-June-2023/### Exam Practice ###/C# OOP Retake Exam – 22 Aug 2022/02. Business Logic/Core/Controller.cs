namespace BookingApp.Core
{
    using Contracts;
    using Models.Bookings;
    using Models.Bookings.Contracts;
    using Models.Hotels;
    using Models.Hotels.Contacts;
    using Models.Rooms;
    using Models.Rooms.Contracts;
    using Repositories;
    using Repositories.Contracts;
    using System;
    using System.Linq;
    using System.Text;
    using Utilities.Messages;

    public class Controller : IController
    {
        private readonly IRepository<IHotel> hotels;

        public Controller()
        {
            this.hotels = new HotelRepository();
        }

        public string AddHotel(string hotelName, int category)
        {
            if (this.hotels.Select(hotelName) != null)
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);

            var hotel = new Hotel(hotelName, category);
            this.hotels.AddNew(hotel);

            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            if (this.hotels.Select(hotelName) == null)
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);

            var hotel = this.hotels.Select(hotelName);
            if (hotel.Rooms.Select(roomTypeName) != null) // Check
                return string.Format(OutputMessages.RoomTypeAlreadyCreated);

            if (!this.IsValidRoom(roomTypeName))
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);

            var roomType = this.CreateRoom(roomTypeName);
            hotel.Rooms.AddNew(roomType);

            return string.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            if (this.hotels.Select(hotelName) == null)
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);

            var hotel = this.hotels.Select(hotelName);

            if (!this.IsValidRoom(roomTypeName))
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);

            if (hotel.Rooms.Select(roomTypeName) == null)
                return string.Format(OutputMessages.RoomTypeNotCreated);

            var room = hotel.Rooms.Select(roomTypeName);

            if (room.PricePerNight > 0)
                throw new InvalidOperationException(ExceptionMessages.CannotResetInitialPrice);

            room.SetPrice(price);
            return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category) 
        {
            int totalGuests = adults + children;

            if (this.hotels.All().FirstOrDefault(h => h.Category == category) == null)
                return string.Format(OutputMessages.CategoryInvalid, category);

            var orderedHotels = this.hotels.All()
                .Where(h => h.Category == category)
                .OrderBy(h => h.Turnover)
                .ThenBy(h => h.FullName);

            foreach (var hotel in orderedHotels)
            {
                var selectedRoom = hotel.Rooms.All()
                    .Where(h => h.PricePerNight > 0)
                    .Where(h => h.BedCapacity >= totalGuests)
                    .OrderBy(h => h.BedCapacity).FirstOrDefault();

                if (selectedRoom != null)
                {
                    int bookingNumber = this.hotels.All().Sum(h => h.Bookings.All().Count) + 1;
                    IBooking booking = new Booking(selectedRoom, duration, adults, children, bookingNumber);
                    hotel.Bookings.AddNew(booking);
                    return string.Format(OutputMessages.BookingSuccessful, bookingNumber, hotel.FullName);
                }
            }

            return string.Format(OutputMessages.RoomNotAppropriate);
        }

        public string HotelReport(string hotelName)
        {
            var hotel = this.hotels.Select(hotelName);

            if (hotel == null)
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);

            var sb = new StringBuilder();
            sb.AppendLine($"Hotel name: {hotelName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
            sb.AppendLine("--Bookings:");
            sb.AppendLine();

            if (hotel.Bookings.All().Count == 0)
            {
                sb.AppendLine("none");
            }
            else
            {
                foreach (var booking in hotel.Bookings.All())
                {
                    sb.AppendLine(booking.BookingSummary());
                    sb.AppendLine();
                }
            }

            return sb.ToString().Trim();
        }

        private bool IsValidRoom(string roomTypeName)
            => roomTypeName == nameof(Apartment) || roomTypeName == nameof(DoubleBed) || roomTypeName == nameof(Studio);

        private IRoom CreateRoom(string roomTypeName)
        {
            IRoom roomType = null;
            if (roomTypeName == nameof(DoubleBed))
            {
                roomType = new DoubleBed();
            }
            else if (roomTypeName == nameof(Studio))
            {
                roomType = new Studio();
            }
            else if (roomTypeName == nameof(Apartment))
            {
                roomType = new Apartment();
            }

            return roomType;
        }
    }
}