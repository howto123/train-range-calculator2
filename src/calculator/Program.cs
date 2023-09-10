using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using calculator.Work;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("calculatorsettings.json")
    .Build();

var inPath = configuration["inPath"];
var outPath = configuration["outPath"];


// Console.WriteLine(@$"Hi! The train-range-calculator is running! It will take a JSON-file from this directory:
// '{inPath}' and create a new JSON-file in '{outPath}'. You can change this in 'settings.json'. There you can set the 
// number of steps that you would like to calculate as well. Make sure that there is a suitable class ready in './StepByStep'");

CityCreator.MyPlayGround();