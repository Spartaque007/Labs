namespace Exercises.Linq
{
    public class Customer
    {
        public string Name { get; set; }

        public double Balance { get; set; }

        public string Bank { get; set; }


        public override string ToString()
        {
            return $"Name: {Name} Balance: {Balance} Bank: {Bank}";
        }
    }
}