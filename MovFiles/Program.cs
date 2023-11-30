class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the path of the directory:");
        string directoryPath = Console.ReadLine();

        if (Directory.Exists(directoryPath))
        {
            Console.WriteLine("\nList of folders and subfolders:");

            // Call the recursive function.
            DisplayFolders(directoryPath, "");

            Console.WriteLine("\nFinished processing.");
        }
        else
        {
            Console.WriteLine("Invalid directory path or directory does not exist.");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    
    }

    static void DisplayFolders(string folderPath, string indent)
    {
        string[] folders = Directory.GetDirectories(folderPath);

        foreach (var folder in folders)
        {
            Console.WriteLine($"{indent} - {Path.GetFileName(folder)}");

            // Check if .mov files exist in the current folder.
            string[] movFiles = Directory.GetFiles(folder, "*.mov");
            bool isCreated = false;

            if (movFiles.Length > 0)
            {
                string newPath = folder + "\\" + Path.GetFileName(folder);

                // Create "MOV" folder if it doesn't exist.
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                    isCreated = true;
                    Console.WriteLine($"Created 'MOV' folder in: {Path.GetFileName(newPath)}");
                }

                // Move files to new folder.
                foreach (var movFile in movFiles)
                {
                    string movFileName = Path.GetFileName(movFile);
                    string destinationPath = newPath + "\\" + movFileName;
                    File.Move(movFile, destinationPath);
                    Console.WriteLine($"Moved {Path.GetFileName(movFile)} to 'MOV' folder");
                }
            }

            // Do not check the new subfolder.
            if (isCreated)
            {
                continue;
            }

            // Recursively call the function for subfolders.
            DisplayFolders(folder, indent + "  ");
        }
    }
}
