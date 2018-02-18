using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace EMX.JobyJobs.Admin.ASPCoreFwk.Helpers
{
  /// <summary>
  /// General Extension methods in the site level.
  /// </summary>
  public static class SiteExtensions
  {
    private static IConfiguration _config;
    private static General _configGeneral;

    /// <summary>
    /// To be called at the very beginning of the application.
    /// </summary>
    /// <param name="generalManager"></param>
    public static void Initialize(IConfiguration config)
    {
      _config = config;
      _configGeneral = config.Get<AppSettings>().General;
    }


    /// <summary>
    /// To be called at the very end of the application.
    /// </summary>
    public static void Close()
    {
      _config = null;
      _configGeneral = null;
    }

    /// <summary>
    /// Downloads the given word file from a controller.
    /// <returns></returns>
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="filePath">absolute path or relative path from user files dir</param>
    /// <param name="isUserFile">is absolute path or user file</param>
    /// <returns></returns>
    public static FileResult WordFile(this Controller controller, string filePath, bool isUserFile = true)
    {
      filePath = !isUserFile ? filePath : controller.ToAbsoluteUserFile(filePath);
      string dir = Path.GetDirectoryName(filePath);
      string fileName = Path.GetFileName(filePath);
      IFileProvider provider = new PhysicalFileProvider(dir);
      IFileInfo fileInfo = provider.GetFileInfo(fileName);
      var readStream = fileInfo.CreateReadStream();
      var mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
      return controller.File(readStream, mimeType, filePath);
    }

    /// <summary>
    /// Downloads the given word file from a controller.
    /// <returns></returns>
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="filePath">absolute path or relative path from user files dir</param>
    /// <param name="isUserFile">is absolute path or user file</param>
    /// <returns></returns>
    public static FileResult JpegFile(this Controller controller, string filePath, bool isUserFile = true)
    {
      filePath = !isUserFile ? filePath : controller.ToAbsoluteUserFile(filePath);
      string dir = Path.GetDirectoryName(filePath);
      string fileName = Path.GetFileName(filePath);
      IFileProvider provider = new PhysicalFileProvider(dir);
      IFileInfo fileInfo = provider.GetFileInfo(fileName);
      var readStream = fileInfo.CreateReadStream();
      var mimeType = "image/jpeg";
      return controller.File(readStream, mimeType, filePath);
    }

    /// <summary>
    /// Converts the given relative user file path to an absolute one.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string ToAbsoluteUserFile(this Controller controller, string relativePath)
    {
      return Path.Combine(_configGeneral.UserFilesUploadsFolder, relativePath);
    }

    /// <summary>
    /// Saves the given file to disk and returns its relative path.
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string SaveUploadedSeekerAvatarFile(this Controller controller, IFormFile file, string identity_user_id)
    {
      string relativePath = Path.Combine(identity_user_id, "Avatar_" + identity_user_id + ".png");
      string absolutepath = Path.Combine(_configGeneral.SeekerAvatarsUploadFolder, relativePath);
      string targetDir = Path.GetDirectoryName(absolutepath);
      Directory.CreateDirectory(targetDir);
      using (var fileStream = new FileStream(absolutepath, FileMode.Create))
      {
        file.CopyTo(fileStream);
      }
      return relativePath;
    }

    //private static string GetRelativePath(string fullDestinationPath, string startPath)
    //{
    //  string[] l_startPathParts = Path.GetFullPath(startPath).Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
    //  string[] l_destinationPathParts = fullDestinationPath.Split(Path.DirectorySeparatorChar);

    //  int l_sameCounter = 0;
    //  while ((l_sameCounter < l_startPathParts.Length) && (l_sameCounter < l_destinationPathParts.Length) && l_startPathParts[l_sameCounter].Equals(l_destinationPathParts[l_sameCounter], StringComparison.InvariantCultureIgnoreCase))
    //  {
    //    l_sameCounter++;
    //  }

    //  if (l_sameCounter == 0)
    //  {
    //    return fullDestinationPath; // There is no relative link.
    //  }

    //  StringBuilder l_builder = new StringBuilder();
    //  for (int i = l_sameCounter; i < l_startPathParts.Length; i++)
    //  {
    //    l_builder.Append(".." + Path.DirectorySeparatorChar);
    //  }

    //  for (int i = l_sameCounter; i < l_destinationPathParts.Length; i++)
    //  {
    //    l_builder.Append(l_destinationPathParts[i] + Path.DirectorySeparatorChar);
    //  }

    //  l_builder.Length--;

    //  return l_builder.ToString();
    //}


    /// <summary>
    /// Saves the given file to disk and returns its relative path.
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string SaveUploadedSeekerCVFile(this Controller controller, IFormFile file, string identity_user_id)
    {
      string relativePath = Path.Combine(identity_user_id, "CV_" + identity_user_id + ".docx");
      string absolutePath = Path.Combine(_configGeneral.SeekerCVsUploadFolder, relativePath);
      string targetDir = Path.GetDirectoryName(absolutePath);
      Directory.CreateDirectory(targetDir);
      using (var fileStream = new FileStream(absolutePath, FileMode.Create))
      {
        file.CopyTo(fileStream);
      }
      return relativePath;
    }
    /// <summary>
    /// Saves the given file to disk and returns its relative path.
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string SaveUploadedEmployerPersonAvatarFile(this Controller controller, IFormFile file, string identity_user_id)
    {
      string relativePath = Path.Combine(identity_user_id, "Avatar_" + identity_user_id, ".png");
      string absolutePath = Path.Combine(_configGeneral.EmployerPersonAvatarsUploadFolder, relativePath);
      string targetDir = Path.GetDirectoryName(absolutePath);
      Directory.CreateDirectory(targetDir);
      using (var fileStream = new FileStream(absolutePath, FileMode.Create))
      {
        file.CopyTo(fileStream);
      }
      return relativePath;
    }
    /// <summary>
    /// Saves the given file to disk and returns its relative path.
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string SaveUploadedEmployerLogoFile(this Controller controller, IFormFile file, Guid employer_UID)
    {
      string relativePath = Path.Combine(employer_UID.ToString(), "Logo_" + employer_UID, ".png");
      string absolutePath = Path.Combine(_configGeneral.EmployerPersonAvatarsUploadFolder, relativePath);
      string targetDir = Path.GetDirectoryName(absolutePath);
      Directory.CreateDirectory(targetDir);
      using (var fileStream = new FileStream(absolutePath, FileMode.Create))
      {
        file.CopyTo(fileStream);
      }
      return relativePath;
    }
  }
}
