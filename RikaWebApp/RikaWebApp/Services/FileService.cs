using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RikaWebApp.Data;
using System.Diagnostics;

namespace RikaWebApp.Services;

public class FileService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;


    public async Task<UserProfileImage> CreateFileAsync(string filePath)
    {
        try
        {
            var findResult = await _context.UserProfileImage.FirstOrDefaultAsync(x => x.ProfileImage == filePath);

            if(findResult == null)
            {
                var imageEntity = new UserProfileImage
                {
                    ProfileImage = filePath
                };

                var createResult = await _context.UserProfileImage.AddAsync(imageEntity);

                await _context.SaveChangesAsync();


                return createResult.Entity;
               
            }

            return findResult!;

        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return null!;

    }


    public async Task<UserProfileImage> FindProfileImageAsync(string id)
    {

        var findResult = await _context.UserProfileImage.FirstOrDefaultAsync(x => x.Id == id);

        if(findResult != null)
        {

            return findResult;
        }

        return null!;
    }
}
