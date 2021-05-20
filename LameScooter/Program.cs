using System;
using System.Linq;
using System.Threading.Tasks;

namespace LameScooter {
    class Program {
        static async Task Main(string[] args) {
            var rental = new OfflineLameScooterRental();

            if (args[0].Any(char.IsDigit)) {
                Console.ForegroundColor = ConsoleColor.Red;
                throw  new ArgumentException("Not a valid argument");
            }

            var count = await rental.GetScooterCountInStation(args[0]);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(count);
        }
    }
}
