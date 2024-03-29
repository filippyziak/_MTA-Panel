﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MTA.Core.Application.Models;
using MTA.Core.Application.Services;
using MTA.Core.Common.Helpers;

namespace MTA.Infrastructure.Shared.Services
{
    public class FilesManager : IFilesManager
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public string ProjectPath => webHostEnvironment.ContentRootPath;
        public string WebRootPath => webHostEnvironment.WebRootPath.Replace(@"\", @"\\");

        public IConfiguration Configuration { get; }

        public FilesManager(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this.webHostEnvironment = webHostEnvironment;

            this.Configuration = configuration;
        }

        public async Task<FileModel> Upload(IFormFile file, string filePath = null)
            => await UploadFile(file, filePath);

        public async Task<IList<FileModel>> Upload(IEnumerable<IFormFile> files, string filePath = null)
        {
            var uploadFiles = new List<FileModel>();

            foreach (var file in files)
                uploadFiles.Add(await UploadFile(file, filePath));

            return uploadFiles;
        }

        public void Delete(string path)
        {
            if (!FileExists(path))
                return;

            path = string.IsNullOrEmpty(path) ? $"{WebRootPath}/" : $"{WebRootPath}/{path}";

            File.Delete(path);
        }

        public void DeleteDirectory(string path, bool isRecursive = true)
        {
            path = string.IsNullOrEmpty(path) ? $"{WebRootPath}/" : $"{WebRootPath}/{path}";

            if (Directory.Exists(path))
                Directory.Delete(path, recursive: isRecursive);
        }

        public void DeleteByFullPath(string fullPath)
            => File.Delete(fullPath);

        public async Task<string> ReadFile(string filePath)
            => await File.ReadAllTextAsync($"{WebRootPath}/{filePath}");

        public async Task<string[]> ReadFileLines(string filePath)
            => await File.ReadAllLinesAsync($"{WebRootPath}/{filePath}");

        public async Task WriteFile(string file, string filePath)
            => await File.WriteAllTextAsync(filePath, file);

        public async Task ReplaceInFile(string filePath, string oldValue, string newValue)
        {
            var fileText = (await ReadFile(filePath)).Replace(oldValue, newValue);

            using (var stream = new StreamWriter($"{WebRootPath}{filePath}")) await stream.WriteAsync(fileText);
        }

        public bool FileExists(string filePath)
            => File.Exists($"{WebRootPath}/{filePath}");

        public IEnumerable<string> GetDirectoryFilesNames(string path)
            => Directory.GetFiles(path);

        #region private

        private async Task<FileModel> UploadFile(IFormFile file, string filePath)
        {
            if (file == null || file.Length <= 0)
                return null;

            var uploadFile = BuildFileModel(filePath, Path.GetExtension(file.FileName), file.Length);

            using (var stream = File.Create(uploadFile.FullPath))
            {
                await file.CopyToAsync(stream);
            }

            return uploadFile;
        }

        private FileModel BuildFileModel(string filePath, string fileExtension, long fileSize)
        {
            var (relativePath, fullPath) = ($"/files/{filePath}/", $"{WebRootPath}/files/{filePath}/");
            var fileUrl = $"{Configuration.GetValue<string>(AppSettingsKeys.ServerAddress)}/files/{filePath}/";

            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            string fileName = $"{Utils.NewGuid(length: 32)}{fileExtension}";

            relativePath += fileName;
            fullPath += fileName;
            fileUrl += fileName;

            return new FileModel(relativePath, fileUrl, fileSize, fullPath);
        }

        #endregion
    }
}