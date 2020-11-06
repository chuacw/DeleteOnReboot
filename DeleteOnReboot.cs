using System;
using System.Runtime.InteropServices;
using System.IO;

class DeleteOnReboot
{

const int MOVEFILE_REPLACE_EXISTING  = 1;
const int MOVEFILE_COPY_ALLOWED = 2;
const int MOVEFILE_DELAY_UNTIL_REBOOT  = 4;
const int MOVEFILE_WRITE_THROUGH  = 8;

[DllImport("kernel32.dll", SetLastError=true)]
static extern int MoveFileExA (string AExistingFileName, string ANewFileName, int AFlags);

   public static void Main(string[] args)
   {
     System.Console.WriteLine("DeleteOnReboot [filename]");
     System.Console.WriteLine();
     if (args.Length==0) {
       System.Console.WriteLine("No file specified to delete.");
       return;
     }
     string FileToDelete = args[0];
     if (File.Exists(FileToDelete)||Directory.Exists(FileToDelete))  {
       System.Console.WriteLine("{0} marked for deletion on reboot.", new Object[] {FileToDelete});
       MoveFileExA(FileToDelete, null, MOVEFILE_DELAY_UNTIL_REBOOT);
     } else {
        System.Console.WriteLine("File \"{0}\" doesn't exist.", new Object[] {FileToDelete});
     }
   }

}
