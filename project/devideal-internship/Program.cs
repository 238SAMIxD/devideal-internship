using System;

namespace devideal_internship {
    
    class Program {
        static DateTime DateInput(string type) {
            DateTime date;

            Console.WriteLine("Enter date of book {0}:", type);

            if (DateTime.TryParse(Console.ReadLine(), out date)) return date;
            
            Console.WriteLine("You have entered an incorrect date.");

            return DateInput(type);
        }

        static int CalculatePrice(int category, DateTime borrowDate, DateTime returnDate) {
            int fine = 0;

            switch (category) {
                case 1:
                    fine = 5;
                    break;
                case 2:
                    fine = 3;
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                    fine = 2;
                    break;
            }

            try {
                if (returnDate.CompareTo(borrowDate) < 0) {
                    throw new InvalidOperationException("Return date cannot be before borrow date. Try again...");
                }
            } catch(InvalidOperationException e) {
                Console.WriteLine(e.Message);

                return -1;
            }

            int days = (int)returnDate.Subtract(borrowDate).TotalDays;

            return fine * (days > 1 ? days - 1 : 0);
        }
        
        static void Main(string[] args) {
            int category;
            bool working = true;

            while (working) {
                category = -1;

                Console.WriteLine("Welcome to our library!");
                Console.WriteLine("Please, pick a book category:");
                Console.WriteLine("[1] IT");
                Console.WriteLine("[2] History");
                Console.WriteLine("[3] Classics");
                Console.WriteLine("[4] Law");
                Console.WriteLine("[5] Medical");
                Console.WriteLine("[6] Philosophy");
                Console.WriteLine("[0] Quit the app");

                try {
                    category = Convert.ToInt32(Console.ReadLine());
                } catch (FormatException e) {
                    Console.WriteLine(e.Message);
                } finally {
                    switch (category) {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            DateTime borrowDate = DateInput("borrow");
                            DateTime returnDate = DateInput("return");
                            int price = CalculatePrice(category, borrowDate, returnDate);

                            if (price == -1) break;

                            Console.WriteLine((price == 0 ? "Borrower has no fee to pay" : "Borrower fee is " + price + " PLN") + "\n");

                            break;
                        case 0:
                            working = false;
                            break;
                        default:
                            Console.WriteLine("Pick a category within 1-6 range or 0 to quit");
                            break;
                    }
                }
            }

            Console.WriteLine("Thank you for your visit! See you again.");
        }
    }
}
