namespace ChristmasPastryShop.Core
{
    using Contracts;
    using Models.Booths;
    using Models.Booths.Contracts;
    using Models.Cocktails;
    using Models.Cocktails.Contracts;
    using Models.Delicacies;
    using Models.Delicacies.Contracts;
    using Repositories;
    using System.Linq;
    using System.Text;
    using Utilities.Messages;

    public class Controller : IController
    {
        private readonly BoothRepository booths;

        public Controller()
        {
            this.booths = new BoothRepository();
        }


        public string AddBooth(int capacity)
        {
            IBooth booth = new Booth(this.booths.Models.Count + 1, capacity);
            this.booths.AddModel(booth);

            return string.Format(OutputMessages.NewBoothAdded, booth.BoothId, booth.Capacity);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            var booth = this.GetBoothById(boothId);

            if (!IsDelicacy(delicacyTypeName))
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);

            if (booth.DelicacyMenu.Models.Any(d => d.Name == delicacyName))
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);

            IDelicacy delicacy = null;
            if (delicacyTypeName == nameof(Gingerbread))
            {
                delicacy = new Gingerbread(delicacyName);
            }
            else if (delicacyTypeName == nameof(Stolen))
            {
                delicacy = new Stolen(delicacyName);
            }

            booth.DelicacyMenu.AddModel(delicacy);
            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            var booth = this.GetBoothById(boothId);

            if (!IsCocktail(cocktailTypeName))
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);

            if (!this.IsValidSize(size))
                return string.Format(OutputMessages.InvalidCocktailSize, size);

            if (booth.CocktailMenu.Models.Any(c => c.Name == cocktailName && c.Size == size))
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);

            ICocktail cocktail = null;
            if (cocktailTypeName == nameof(Hibernation))
            {
                cocktail = new Hibernation(cocktailName, size);
            }
            else if (cocktailTypeName == nameof(MulledWine))
            {
                cocktail = new MulledWine(cocktailName, size);
            }


            booth.CocktailMenu.AddModel(cocktail);
            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string ReserveBooth(int countOfPeople)
        {
            var allAvailableBooths = this.booths.Models.Where(b => b.IsReserved == false && b.Capacity >= countOfPeople)
                .OrderBy(b => b.Capacity)
                .ThenByDescending(b => b.BoothId)
                .ToList();

            var availableBooth = allAvailableBooths.FirstOrDefault();

            if (availableBooth == null)
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);

            availableBooth.ChangeStatus();
            return string.Format(OutputMessages.BoothReservedSuccessfully, availableBooth.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            var booth = this.GetBoothById(boothId);

            string[] orderArgs = order.Split("/");
            string itemTypeName = orderArgs[0];

            if (!IsCocktail(itemTypeName) && !IsDelicacy(itemTypeName))
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);

            string itemName = orderArgs[1];

            if (!this.ItemExists(booth, itemName))
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);

            int countOfOrderedPieces = int.Parse(orderArgs[2]);

            bool isCocktail = IsCocktail(itemTypeName);
            bool isDelicacy = IsDelicacy(itemTypeName);

            string size;
            double price = countOfOrderedPieces;
            if (isCocktail)
            {
                size = orderArgs[3];
                var cocktail = booth.CocktailMenu.Models.FirstOrDefault(c => c.Name == itemName && c.Size == size);
                if (cocktail == null)
                    return string.Format(OutputMessages.CocktailStillNotAdded, size, itemName);

                price = cocktail.Price * countOfOrderedPieces;
            }
            else if (isDelicacy)
            {
                var delicacy = booth.DelicacyMenu.Models.FirstOrDefault(d => d.Name == itemName);
                if (delicacy == null)
                    return string.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);

                price *= delicacy.Price;
            }

            booth.UpdateCurrentBill(price);
            return string.Format(OutputMessages.SuccessfullyOrdered, booth.BoothId, countOfOrderedPieces, itemName);
        }

        public string LeaveBooth(int boothId)
        {
            var booth = this.GetBoothById(boothId);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format(OutputMessages.GetBill, booth.CurrentBill.ToString("F2")));
            booth.Charge();
            booth.ChangeStatus();
            sb.AppendLine(string.Format(OutputMessages.BoothIsAvailable, booth.BoothId));

            return sb.ToString().Trim();
        }

        public string BoothReport(int boothId)
        {
            var booth = this.booths.Models.First(b => b.BoothId == boothId);
            return booth.ToString();
        }

        private static bool IsDelicacy(string delicacyTypeName)
        {
            switch (delicacyTypeName)
            {
                case nameof(Gingerbread):
                case nameof(Stolen):
                    return true;

                default: return false;
            }
        }

        private static bool IsCocktail(string cocktailTypeName)
        {
            switch (cocktailTypeName)
            {
                case nameof(Hibernation):
                case nameof(MulledWine):
                    return true;

                default: return false;
            }
        }

        private IBooth GetBoothById(int id)
        {
            return this.booths.Models.First(b => b.BoothId == id);
        }

        private bool IsValidSize(string size)
        {
            switch (size)
            {
                case "Large":
                case "Middle":
                case "Small":
                    return true;

                default: return false;
            }
        }

        private bool ItemExists(IBooth booth, string itemName)
        {
            return booth.CocktailMenu.Models.Any(i => i.Name == itemName)
                   || booth.DelicacyMenu.Models.Any(i => i.Name == itemName);
        }
    }
}