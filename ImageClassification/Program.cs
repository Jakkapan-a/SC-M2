﻿using System.Diagnostics;

namespace ImageClassification
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Load sample data
            //var imageBytes = File.ReadAllBytes(@"D:\CPush\Deep\ML03\img\NORMAL\48cffbb0-0fd8-4936-931f-8e3c73f2c396.jpg");
            //_731TMC_SW.ModelInput sampleData = new _731TMC_SW.ModelInput()
            //{
            //    ImageSource = imageBytes,
            //};

            ////Load model and predict output
            //var result = _731TMC_SW.Predict(sampleData);

            _731TMC_SW.ModelInput input = new _731TMC_SW.ModelInput();

            bool isRunning = false;
            if(args.Count() > 0 && args[0] == "-file")
            {
                Console.WriteLine("start");
                do
                {
                    string? data = Console.ReadLine();
                    if (data != null)
                    {
                        if (data == "exit")
                        {
                            isRunning = true;
                        }
                        else if (File.Exists(data))
                        {
                            Stopwatch stopwatch = new Stopwatch();
                            stopwatch.Start();

                            var imageBytes = File.ReadAllBytes(data);
                            input.ImageSource = imageBytes;
                            var result = _731TMC_SW.Predict(input);
                            Console.WriteLine($"Label:{result.PredictedLabel}-scores:[{String.Join(",", result.Score)}]");
                            stopwatch.Stop();
                            Console.WriteLine("Time is {0} ms", stopwatch.ElapsedMilliseconds);
                            imageBytes = null;
                            result = null;
                        }
                        else
                        {
                            Console.WriteLine("NotFound");
                        }                   
                    }
                } while (!isRunning);
            }
        }


    }
}