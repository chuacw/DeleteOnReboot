using System;
using System.Runtime.InteropServices;
using System.IO;

class DeleteOnRebootConsole
{

    const int MOVEFILE_REPLACE_EXISTING = 1;
    const int MOVEFILE_COPY_ALLOWED = 2;
    const int MOVEFILE_DELAY_UNTIL_REBOOT = 4;
    const int MOVEFILE_WRITE_THROUGH = 8;

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern int MoveFileExA(string AExistingFileName, string ANewFileName, int AFlags);

    public static void Main(string[] args)
    {
        Console.WriteLine("DeleteOnReboot v0.9 Copyright (C) 2017-20 Chua Chee Wee");
        Console.WriteLine("Usage: DeleteOnReboot [filename]");
        Console.WriteLine();
        
        try
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No file specified to delete.");
                return;
            }
            string FileToDelete = args[0];
            if (File.Exists(FileToDelete) || Directory.Exists(FileToDelete))
            {
                Console.WriteLine("{0} marked for deletion on reboot.", new object[] { FileToDelete });
                MoveFileExA(FileToDelete, null, MOVEFILE_DELAY_UNTIL_REBOOT);
            }
            else
            {
                Console.WriteLine("File \"{0}\" doesn't exist.", new object[] { FileToDelete });
            }
        }
        finally
        {
            Console.WriteLine();
        }
    }

}
