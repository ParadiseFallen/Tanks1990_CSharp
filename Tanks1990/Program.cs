//#define TEST
#define WINDOWS
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


#if WINDOWS
            Application.AppForWindows app = new Application.AppForWindows();
            app.Run();
#endif



#endif

        }

    }
}
