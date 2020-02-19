using Pixselio.Dto;
using System.Collections.Generic;

namespace Pixselio.Business.Services
{
    public interface IPhotoService
    {
        PhotoDto GetPhotoById(int id);
        List<PhotoDto> GetPhotoByUserId(string createdUserName);
        string Add(PhotoDto dto); 
        string Delete(int id); 
    }
}
