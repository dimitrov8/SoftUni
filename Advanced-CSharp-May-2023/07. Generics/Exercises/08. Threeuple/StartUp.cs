using System;

namespace Threeuple
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] personNameAndTown = Console.ReadLine()!.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int addressCount = personNameAndTown.Length - 3;
            var personNameAndTownInfo = new CustomThreeuple<string, string, string>(personNameAndTown[0] + " " + personNameAndTown[1], personNameAndTown[2],
                string.Join(" ", personNameAndTown, 3, addressCount));

            string[] personBeer = Console.ReadLine()!.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var personBeerInfo = new CustomThreeuple<string, int, bool>(personBeer[0], int.Parse(personBeer[1]), personBeer[2] == "drunk");
            
            string[] personBankBalance = Console.ReadLine()!.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var personBankBalanceInfo = new CustomThreeuple<string, double, string>(personBankBalance[0], double.Parse(personBankBalance[1]), personBankBalance[2]);

            Console.WriteLine(personNameAndTownInfo);
            Console.WriteLine(personBeerInfo);
            Console.WriteLine(personBankBalanceInfo);
        }
    }
}