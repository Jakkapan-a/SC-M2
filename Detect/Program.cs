using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Load sample data
            var imageBytes = File.ReadAllBytes(@"D:\CPush\Deep\ML03\img\NORMAL\48cffbb0-0fd8-4936-931f-8e3c73f2c396.jpg");
            _731TMC_SW.ModelInput sampleData = new _731TMC_SW.ModelInput()
            {
                ImageSource = imageBytes,
            };

            //Load model and predict output
            var result = _731TMC_SW.Predict(sampleData);

        }
    }
}
