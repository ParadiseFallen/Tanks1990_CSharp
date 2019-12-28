#define TEST
namespace Tanks1990
{
    class Program
    {
        static void Main(string[] args)
        {

#if TEST
            Application.DEBUG.Tests tests = new Application.DEBUG.Tests();
            tests.Run();
#else
            Application.App app = new Application.App();
            app.Run();
#endif

        }

    }
}
