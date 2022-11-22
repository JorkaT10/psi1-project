using Bunit;
using PSI_MobileApp;
using PSI_MobileApp.Pages;

namespace PSI_MobileAppTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public async void ExceptionLoggerTest()
        {
            var path = "C:\\Users\\Rokas\\source\\repos\\merges-psi-project\\PSI_MobileApp\\TestLog.txt";
            if (File.Exists(path))
                File.Delete(path);
            using (var logger = new ExceptionLogger(path))
            {
                logger.Log(new Exception("Test"));
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        Assert.Contains("Test", streamReader.ReadToEnd());
                    }
                }
            }

               
        }
    }
}