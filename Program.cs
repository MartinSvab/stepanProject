namespace StepanProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 2)
            {
                throw new ApplicationException("Wrong amount of parameters");
            }
            else if (args[1].Length != 8)
            {
                throw new ApplicationException("Date is in wrong format (yyyymmdd)");
            }

            var app = new Runner();
            app.Run(args[0], args[1]);
        }
    }
}