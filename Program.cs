namespace StepanProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new Runner();

            switch (args.Length)
            {
                case 1:
                    app.Run(args[0], "99999999");
                    break;
                case 2:
                    app.Run(args[0], args[1]);
                    break;
                default:
                    throw new Exception("Wrong amount of parameters");
            }
        }
    }
}