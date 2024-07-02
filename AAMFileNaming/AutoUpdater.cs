using AAMFileNaming.Shared.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAMFileNaming
{
    internal class AutoUpdater
    {
        internal static string Update()
        {
            // check if Rhini 8 or Rhino 7
            string folder_server = $"X:\\AAM-WORKGROUP DATA\\AAM_BIM\\AAM_DesignTechnology\\03_Automation and Tool Development\\09_OfficeTools\\AAMFileNaming\\00_Current";
            string folder_local = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            DirectoryInfo local_directory = new DirectoryInfo(folder_local);
            FileInfo[] local_files = local_directory.GetFiles("*.*", SearchOption.AllDirectories);

            // check if folder exists
            if (!Directory.Exists(folder_server))
            {
                AAMLogger.Error("RENAAME : error updating could not find server folder");
                return "RENAAME : error updating could not find server folder";
            }

            if (!Directory.Exists(folder_local))
            {
                AAMLogger.Error("RENAAME : error updating could not find local folder");
                return "RENAAME : error updating could not find local folder";
            }


            foreach (FileInfo local_file in local_files)
            {
                try
                {
                    string server_file_path = folder_server + "\\" + local_file.FullName.Substring(folder_local.Length + 1);
                    if (File.Exists(server_file_path))
                    {
                        DateTime server_file_modified = File.GetLastWriteTime(server_file_path);
                        if (local_file.LastWriteTime < server_file_modified)
                        {
                            try
                            {
                                File.Copy(server_file_path, local_file.FullName, true);
                                AAMLogger.Info($"RENAAME : updated file {local_file.Name}");
                            }
                            catch (Exception ex)
                            {
                                AAMLogger.Error($"RENAAME : ERROR could not update file {local_file.Name}, error: {ex.Message}");
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    AAMLogger.Error($"RENAAME : error updating file {local_file.Name}, error: {ex.Message}");
                }

            }

            DirectoryInfo server_directory = new DirectoryInfo(folder_server);
            FileInfo[] server_files = server_directory.GetFiles("*.*", SearchOption.AllDirectories);


            foreach (FileInfo server_file in server_files)
            {
                try
                {
                    string local_file_path = folder_local + "\\" + server_file.FullName.Substring(folder_server.Length + 1);
                    if (!File.Exists(local_file_path))
                    {
                        try
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(local_file_path));
                            File.Copy(server_file.FullName, local_file_path);
                            AAMLogger.Info($"RENAAME : created file {server_file.Name}");

                        }
                        catch (Exception ex)
                        {
                            AAMLogger.Error($"RENAAME : ERROR could not create file {server_file.Name}, error: {ex.Message}");
                        }

                    }
                }
                catch (Exception ex)
                {
                    AAMLogger.Error($"RENAAME : error creating file {server_file.Name}, error: {ex.Message}");
                }

            }

            return "RENAAME : all files up to date";
        }
    }
}
