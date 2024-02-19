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

            var app = new Runner();
            app.Run(args[0], args[1]);
        }
    }
}