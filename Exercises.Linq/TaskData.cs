using System.Collections.Generic;

namespace Exercises.Linq
{
    public class TaskData
    {

        public List<string> fruits = new List<string>()
            {
                "Lemon", "Apple", "Orange", "Lime", "Watermelon", "Loganberry"
            };

        public List<int> mixedNumbers = new List<int>() { 15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96 };

        public List<string> names = new List<string>()
            {
                "Heather", "James", "Xavier", "Michelle", "Brian", "Nina",
                "Kathleen", "Sophia", "Amir", "Douglas", "Zarley", "Beatrice",
                "Theodora", "William", "Svetlana", "Charisse", "Yolanda",
                "Gregorio", "Jean-Paul", "Evangelina", "Viktor", "Jacqueline",
                "Francisco", "Tre"
            };

        public List<double> purchases = new List<double>()
            {
                2340.29, 745.31, 21.76, 34.03, 4786.45, 879.45, 9442.85, 2454.63, 45.65
            };

        public List<int> wheresSquaredo = new List<int>()
            {
                66, 12, 8, 27, 82, 34, 7, 50, 19, 46, 81, 23, 30, 4, 68, 14
            };

        public List<Customer> customers = new List<Customer>() {
        new Customer(){ Name="Bob Lesman", Balance=80345.66, Bank="FTB"},
        new Customer(){ Name="Joe Landy", Balance=9284756.21, Bank="WF"},
        new Customer(){ Name="Meg Ford", Balance=487233.01, Bank="BOA"},
        new Customer(){ Name="Peg Vale", Balance=7001449.92, Bank="BOA"},
        new Customer(){ Name="Mike Johnson", Balance=790872.12, Bank="WF"},
        new Customer(){ Name="Les Paul", Balance=8374892.54, Bank="WF"},
        new Customer(){ Name="Sid Crosby", Balance=957436.39, Bank="FTB"},
        new Customer(){ Name="Sarah Ng", Balance=56562389.85, Bank="FTB"},
        new Customer(){ Name="Tina Fey", Balance=1000000.00, Bank="CITI"},
        new Customer(){ Name="Sid Brown", Balance=49582.68, Bank="CITI"}
        };
    }


    public class GroupedMillionaires
    {
        public string Bank { get; set; }
        public IEnumerable<string> Millionaires { get; set; }
    }


    public class Customer
    {
        public string Name { get; set; }
        public double Balance { get; set; }
        public string Bank { get; set; }
    }

}