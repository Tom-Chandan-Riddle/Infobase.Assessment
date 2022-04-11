using Infobase.Assessment;
using System.Diagnostics;
using System.Reflection;

Stopwatch main_timer = new();
Stopwatch file_timer = new();
Console.WriteLine("Timer Started...\n\n");
main_timer.Start();

//Initial data
var input_file_name = @"App_Data\words.txt";
var output_uniques_file_name = @"App_Data\uniques.txt";
var output_fullwords_file_name = @"App_Data\fullwords.txt";
//var folder_path = @"D:\VS_STUDIO_PROJECTS\Infobase.Assessment\Infobase.Assessment\Files";
//var folder_path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
var folder_path = Path.GetDirectoryName(Environment.ProcessPath);

var utilities = new Utilities();

Console.WriteLine("trying to read the content from input file : words.text ...");
var input_file_path = Path.Combine(folder_path, input_file_name);
if (!File.Exists(input_file_path))
    Console.Error.WriteLine("File does not exist!");

// Open the file to read from.
file_timer.Start();
string[] words = File.ReadAllLines(input_file_path);
file_timer.Stop();
Console.WriteLine("Elapsed time to read file : " + file_timer.ElapsedMilliseconds + "\n\n");
int char_num = 4;

Console.WriteLine("Processing the file words to find unique " + char_num + " letter sequential words...");
var seqCombinations = utilities.GetUniqueNLetterSequences(words.ToList<string>(), char_num);


Console.WriteLine("Total Unique combinations : " + seqCombinations.Count + "\n\n");

var sortedSeqCombinations = new SortedDictionary<string, string>(seqCombinations);

//Write content to files..
var output_uniques_filepath = Path.Combine(folder_path, output_uniques_file_name);
var output_fullwords_filepath = Path.Combine(folder_path, output_fullwords_file_name);
Console.WriteLine("Trying to write content to out files....");
file_timer.Start();

//Parallel.Invoke(
//    () => utilities.WriteMultiLinesToFile(sortedSeqCombinations.Keys.ToList<string>(), output_uniques_filepath),
//    () => utilities.WriteMultiLinesToFile(sortedSeqCombinations.Values.ToList<string>(), output_fullwords_filepath)
//    );

utilities.WriteMultiLinesToFile(sortedSeqCombinations.Keys.ToList<string>(), output_uniques_filepath);
utilities.WriteMultiLinesToFile(sortedSeqCombinations.Values.ToList<string>(), output_fullwords_filepath);

file_timer.Stop();
Console.WriteLine("OUTPUT (Uniques) : " + output_uniques_filepath);
Console.WriteLine("OUTPUT (fullwords) : " + output_fullwords_filepath);
Console.WriteLine("Elapsed time to write to output files : " + file_timer.ElapsedMilliseconds + " milliseconds. \n\n");

main_timer.Stop();
Console.WriteLine("Total Elapsed time : " + main_timer.ElapsedMilliseconds + " milliseconds");
Console.ReadLine();