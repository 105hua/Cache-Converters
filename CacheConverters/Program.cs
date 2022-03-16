using System;
using System.Diagnostics;
using System.IO;

namespace CacheConverters // This is the namespace for the program.
{ // Beginning of namespace.
    class Program // This is the class for the main Program.
    { // Beginning of class.

        static void Main() // This is the main function, it will be executed first.
        { // Beginning of function.


            Console.WriteLine("Attempting to kill Discord to prevent errors..."); // Print to console saying that the program will try to kill discord.

            foreach (var process in Process.GetProcessesByName("Discord")) // For each currently running process called Discord.
            { // Beginning of foreach loop.

                process.Kill(); // Kill the process.

            } // Ending of foreach loop.

            Console.WriteLine("Attempt to kill Discord has been completed."); // Print to console saying that the kill attempt has been completed.
            Console.WriteLine("Attempting to access Discord's cache files..."); // Print to console saying that the program will now attempt to find Discord's cache files.

            string[] cache; // Create a string array to store the paths of each file in Discord's cache folder.

            try // Enter a try statement, as we cannot guarantee that the following code will not produce an error.
            { // Beginning of try statement.

                cache = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\discord\Cache"); // Get a string array for each file in Discord's cache folder.

            } catch (Exception exc) // End of try statement and start of catch statement.
            { // Beginning of catch statement.

                Console.ForegroundColor = ConsoleColor.Red; // Set the consoles foreground color to red.
                Console.WriteLine($"===============================\nERROR!\n\nMessage: \n{exc.Message}\n==============================="); // Print an error with the error message.
                Console.ForegroundColor = ConsoleColor.Gray; // Set the consoles foreground color to gray (Default color).
                Console.WriteLine("Press any key to exit..."); // Print to console saying that the user can press any key to exit.
                Console.ReadKey(); // Read the next key but don't do anything with it.
                Environment.Exit(0); // Exit the environment with code 0.
                return; // Return.

            }  // Ending of catch statement.

            Console.WriteLine("Cache Files have been accessed."); // Print to console saying that the cache files have been accessed.

            string convertedDestination = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\discord\Cache_Converted"; // Define the destination for the converted files. This will be a folder inside of Discord's app data.

            if (!Directory.Exists(convertedDestination)) // If the destination does not exist.
            { // Beginning of if statement.

                Directory.CreateDirectory(convertedDestination); // Create the directory.

            } // Ending of if statement.

            Console.WriteLine("Beginning conversion of files...");

            foreach (string file in cache) // For each file in the array of cache files.
            { // Beginning of foreach loop.

                FileStream source = File.Open(file, FileMode.Open); // Open the source file.
                string[] splitFileDirectory = source.Name.Split(@"\"); // Split the source files directory into parts.
                string destName = convertedDestination + "/" + splitFileDirectory[splitFileDirectory.Length - 1] + ".png"; // Create the converted files destination.

                if (File.Exists(destName)) // If the destination file exists.
                { // Beginning of if statement.

                    File.Delete(destName); // Delete the file.

                } // Ending of if statement.

                FileStream destination = File.Create(destName); // Create the destination file again.
                byte[] sourceData = new byte[source.Length]; // Create a new byte array to store the data from the source file.
                source.Read(sourceData); // Read the source file and store the contents in sourceData.
                destination.Write(sourceData); // Write sourceData to the destination file.

                Console.WriteLine($"Converted file stored at '{file}'"); // Print to console saying that the current file has been converted.

            } // Ending of foreach loop.

            ProcessStartInfo explorerInfo = new ProcessStartInfo // Create a new Process Start Info variable to open the "Cache_Converted" file.
            { // Beginning of parameters.

                Arguments = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\discord\Cache_Converted", // Set the arguments to the "Cache_Converted" file.
                FileName = "explorer.exe" // Set the file name to File Explorer.

            }; // Ending of parameters.

            Process.Start(explorerInfo); // Start a process with the explorer information.

            Console.WriteLine("Done."); // Print to console saying that the process has been completed.

            Environment.Exit(0); // Exit the environment with code 0.
            return; // Return.

        } // Ending of function.

    } // Ending of class.

} // Ending of namespace.
