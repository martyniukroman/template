using System;
using System.IO;
using System.Threading.Tasks;
using hsl.api.Models;
using hsl.bl.Helpers;
using hsl.bl.Interfaces;
using hsl.db.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace hsl.bl.Services
{
    public class FileHandlerService : IFileHandler
    {
        
        const int MIN_HEIGHT = 150;
        const int MAX_HEIGHT = 3000;
        const int MIN_WIDTH = 150;
        const int MAX_WIDTH = 3000;
        const string PROFILE = "profile";
        const string PREVIEW = "preview";
        const string GALARY = "galary";

//        const string ARTICLE_ATTACHMENTS_FOLDER_NAME = "Attachments";
//        const string HOME_PAGE = "HomePage";
        private const int PREVIEW_MIN_WIDTH = 300;
        private const int PREVIEW_MAX_WIDTH = 3000;
        private const int PREVIEW_MIN_HEIGHT = 300;
        private const int PREVIEW_MAX_HEIGHT = 3000;

        private readonly HslapiContext _hslapiContext;
        private readonly string _rootDataFolder;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<AppUser> _userManager;
        
        public FileHandlerService(HslapiContext hslapiContextMir, IHostingEnvironment env, UserManager<AppUser> userManager)
        {
            _hslapiContext = hslapiContextMir;
            _env = env;
            _rootDataFolder = "~/App_Data";
            _userManager = userManager;
        }
        
        public async Task<IActionResult> HandleUserProfileImage(IFormFile formFile, string userId)
        {
            var isImageValid = ImageValidator.IsValidImageFile(formFile, MIN_HEIGHT, MAX_HEIGHT, MIN_WIDTH, MAX_WIDTH) &&
                                ImageValidator.IsImage(formFile);
            
            if(!isImageValid) throw new Exception("File is invalid");
            
            var webRootPath = _env.WebRootPath;
            var folderName = "Images";
            var avatarFolder = "ProfileImages";

            var fileDestinationDirectory = Path.Combine(webRootPath, folderName, avatarFolder);
            if (!Directory.Exists(fileDestinationDirectory))
            {
                Directory.CreateDirectory(fileDestinationDirectory);
            }
          
            var fileExtension = Path.GetExtension(formFile.FileName);
            var fileName = userId + fileExtension;
            var fileFullPath = Path.Combine(fileDestinationDirectory, fileName);
            
            using (var stream = new FileStream(fileFullPath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream); 
            }

            var user = _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            
            await _hslapiContext.AppImages.AddAsync(new AppImage()
            {
                
            });

            return new OkObjectResult("good");

        }
    }
}