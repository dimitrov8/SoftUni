namespace Login
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            string username = Console.ReadLine();

            string correctPassword = string.Empty;
            int attempts = 0;
            for (int i = username.Length - 1; i >= 0; i--)
            {
                correctPassword += username[i];
            }
            string passwordInput = Console.ReadLine();

            while (passwordInput != correctPassword)
            {
                attempts++;
                if (attempts == 4)
                {
                    Console.WriteLine($"User {username} blocked!");
                    return;
                }
                Console.WriteLine("Incorrect password. Try again.");
                passwordInput = Console.ReadLine();
            }
            Console.WriteLine($"User {username} logged in.");
        }
    }
}
