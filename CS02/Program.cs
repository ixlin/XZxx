// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

Trace.Listeners.Add(new TextWriterTraceListener(
    File.CreateText(
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            "log.txt"))));
Trace.AutoFlush = true;

ConfigurationBuilder builder = new();
builder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json",
    optional: true, reloadOnChange: true);
IConfigurationRoot configuration = builder.Build();
TraceSwitch ts = new (
    displayName: "PacktSwitch",
    description: "This switch is set via a JSON config."
    );
configuration.GetSection("PacktSwitch").Bind(ts);

Trace.WriteLineIf(ts.TraceError, DateTime.Now.ToString() + " Trace Error");
Trace.WriteLineIf(ts.TraceWarning, DateTime.Now.ToString() + " Trace Warning");
Trace.WriteLineIf(ts.TraceInfo, DateTime.Now.ToString() + " Trace Information");
Trace.WriteLineIf(ts.TraceVerbose, DateTime.Now.ToString() + " Trace verbose");

//string[] names = { "lily", "duo", "kevin" };
//foreach (var name in names)
//{
//    Console.WriteLine("{0} is {1} yeas old.", name, name.Length);
//    Debug.WriteLine(DateTime.Now.ToString() + " - 这是DEBUG调试信息，release后会自动停止记录");
//    Trace.WriteLine(DateTime.Now.ToString() + " - This is Trace message, for release version");
//}