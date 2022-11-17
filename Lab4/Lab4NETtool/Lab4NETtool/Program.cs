using All_Labs;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Reflection;

namespace Lab4NETtool
{
    class Program
    {
        private static string FileExistsToString(bool ex)
        {
            return ex ? "Exists" : "Not found";
        }
        private static string getPathToFile()
        {
            string labPath = Environment.GetEnvironmentVariable("LAB_PATH") ?? "";
            if (labPath.Length > 0) return labPath;
            else return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
        static int Main(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "Lab4",
                Description = "Consolle app pr4",
            };
            app.HelpOption(inherited: true);
            app.Command("version", configCmd => {
                configCmd.OnExecute(() => {
                    Console.WriteLine("Hleb Hridin");
                    Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Version);
                });
            });
            app.Command("labs", configCmd =>
            {
                
                configCmd.OnExecute(() => {
                    Console.WriteLine("Specify a lab to execute");
                    app.ShowHelp();
                    return 1;
                });
                configCmd.Command("lab1", setCmd =>
                 {
                     setCmd.Description = "Execute LAB1";
                     var folder = getPathToFile();
                     var input = setCmd.Option("--input", "Input file path", CommandOptionType.SingleValue);
                     var output = setCmd.Option("--output", "Output file path", CommandOptionType.SingleValue);
                     input.DefaultValue = $"{folder}/Lab1INPUT.txt";
                     output.DefaultValue = $"{folder}/Lab1OUTPUT.txt";
                     setCmd.OnExecute(() =>
                     {
                         bool inExists = System.IO.File.Exists(input.Value());
                         bool outExists = System.IO.File.Exists(input.Value());
                         Console.WriteLine("############################################################################");
                         Console.WriteLine($"Input file {input.Value()} {FileExistsToString(inExists)}");
                         Console.WriteLine($"Input file {output.Value()} {FileExistsToString(outExists)}");
                         Console.WriteLine("############################################################################");
                         Console.WriteLine("############################# INPUT FILE CONTENT ###########################");
                         string[] inputData = System.IO.File.ReadAllLines(input.Value());
                         foreach (string v in inputData)
                             Console.WriteLine(v);
                         Console.WriteLine("############################################################################");
                         Console.WriteLine("");
                         Console.WriteLine("############################################################################");
                         Console.WriteLine("############################# RESULT FILE CONTENT ##########################");
                         var result = PR1.Main(inputData);
                         System.IO.File.WriteAllText(output.Value(),result);
                         Console.WriteLine(result);
                         Console.WriteLine("############################################################################");
                     });
                });
                configCmd.Command("lab2", setCmd =>
                {
                    setCmd.Description = "Execute LAB2";
                    var folder = getPathToFile();
                    var input = setCmd.Option("--input", "Input file path", CommandOptionType.SingleValue);
                    var output = setCmd.Option("--output", "Output file path", CommandOptionType.SingleValue);
                    input.DefaultValue = $"{folder}/Lab2INPUT.txt";
                    output.DefaultValue = $"{folder}/Lab2OUTPUT.txt";
                    setCmd.OnExecute(() =>
                    {
                        bool inExists = System.IO.File.Exists(input.Value());
                        bool outExists = System.IO.File.Exists(input.Value());
                        Console.WriteLine("############################################################################");
                        Console.WriteLine($"Input file {input.Value()} {FileExistsToString(inExists)}");
                        Console.WriteLine($"Input file {output.Value()} {FileExistsToString(outExists)}");
                        Console.WriteLine("############################################################################");
                        Console.WriteLine("############################# INPUT FILE CONTENT ###########################");
                        string[] inputData = System.IO.File.ReadAllLines(input.Value());
                        foreach (string v in inputData)
                            Console.WriteLine(v);
                        Console.WriteLine("############################################################################");
                        Console.WriteLine("");
                        Console.WriteLine("############################################################################");
                        Console.WriteLine("############################# RESULT FILE CONTENT ##########################");
                        
                        var result = PR2.Main(inputData);
                        System.IO.File.WriteAllText(output.Value(), result);
                        Console.WriteLine(result);
                        Console.WriteLine("############################################################################");
                    });
                });
                configCmd.Command("lab3", setCmd =>
                {
                    setCmd.Description = "Execute LAB3";
                    var folder = getPathToFile();
                    var input = setCmd.Option("--input", "Input file path", CommandOptionType.SingleValue);
                    var output = setCmd.Option("--output", "Output file path", CommandOptionType.SingleValue);
                    input.DefaultValue = $"{folder}/Lab3INPUT.txt";
                    output.DefaultValue = $"{folder}/Lab3OUTPUT.txt";
                    setCmd.OnExecute(() =>
                    {
                        bool inExists = System.IO.File.Exists(input.Value());
                        bool outExists = System.IO.File.Exists(input.Value());
                        Console.WriteLine("############################################################################");
                        Console.WriteLine($"Input file {input.Value()} {FileExistsToString(inExists)}");
                        Console.WriteLine($"Input file {output.Value()} {FileExistsToString(outExists)}");
                        Console.WriteLine("############################################################################");
                        Console.WriteLine("############################# INPUT FILE CONTENT ###########################");
                        string[] inputData = System.IO.File.ReadAllLines(input.Value());
                        foreach (string v in inputData)
                            Console.WriteLine(v);
                        Console.WriteLine("############################################################################");
                        Console.WriteLine("");
                        Console.WriteLine("############################################################################");
                        Console.WriteLine("############################# RESULT FILE CONTENT ##########################");
                        var result = PR3.Main(inputData);
                        System.IO.File.WriteAllText(output.Value(), result);
                        Console.WriteLine(result);
                        Console.WriteLine("############################################################################");
                    });
                });
                configCmd.Command("set-path", setCmd =>
                {
                    setCmd.Description = "Set data folder";
                    var path = setCmd.Option("--path", "Path to data files", CommandOptionType.SingleValue);

                    string labPath = Environment.GetEnvironmentVariable("LAB_PATH") ?? "";
                    Console.WriteLine($"LAB_PATH: {labPath}\n");
                    setCmd.OnExecute(() => {
                        if (path.Value() == null || path.Value() == "")
                        {
                            Console.WriteLine("Specify path with --path option");
                            Console.WriteLine(path.Value());
                        }
                        else
                        {
                            Console.WriteLine(path.Value());
                            Environment.SetEnvironmentVariable("LAB_PATH", path.Value(), EnvironmentVariableTarget.User);
                            Console.WriteLine("Path added succsesfully");
                        }
                            
                    });
                });
            });
            
            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 1;

            });
            return app.Execute(args);
        }
        
    }
}
