using WebAPI.Data.Dto.File;
using WebAPI.Data.Enum;
using WebAPI.Utilities.Results;
using IResult = WebAPI.Utilities.Results.IResult;

namespace WebAPI.Services.FileUploader;

public class FileService
{
    private string _uploadsFolder;


    public FileService()
    {
        _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        if (!Directory.Exists(_uploadsFolder))
            Directory.CreateDirectory(_uploadsFolder);
    }

    public async Task<IResult<FileResponse>> SaveFileAsync(FileRequest request)
    {
        if (request.File == null! ||
            request.File.Length == 0)
            return new ErrorResult<FileResponse>("file is null");


        var combinedPath = Path.Combine(_uploadsFolder, request.Directory.ToString());

        if (!File.Exists(combinedPath))
            Directory.CreateDirectory(combinedPath);


        _uploadsFolder = combinedPath;


        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";

        var filePath = Path.Combine(_uploadsFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await request.File.CopyToAsync(stream);


        return new SuccessResult<FileResponse>(new FileResponse{FileName = fileName});
    }

    //
    // public async Task<IResult<List<UploaderResponse>>> SaveFileAsync(MultipleFileDto fileDto)
    // {
    //     if (fileDto.File.Count < 1)
    //         return new ErrorResult<List<UploaderResponse>>("file is null");
    //
    //
    //     var combinedPath = _uploadsFolder.Contains(fileDto.ParentDir.ToString())
    //         ? _uploadsFolder
    //         : Path.Combine(_uploadsFolder, fileDto.ParentDir.ToString());
    //
    //
    //     if (!File.Exists(combinedPath))
    //         Directory.CreateDirectory(combinedPath);
    //
    //     _uploadsFolder = combinedPath;
    //
    //     var fileResponse = new List<UploaderResponse>();
    //
    //     foreach (var file in fileDto.File)
    //     {
    //         var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
    //
    //         var filePath = Path.Combine(_uploadsFolder, fileName);
    //
    //         await using var stream = new FileStream(filePath, FileMode.Create);
    //
    //         await file.CopyToAsync(stream);
    //
    //         await stream.DisposeAsync();
    //
    //         fileResponse.Add(new UploaderResponse
    //         {
    //             Name = fileName,
    //             Size = file.Length
    //         });
    //     }
    //
    //
    //     return new SuccessResult<List<UploaderResponse>>(data: fileResponse);
    // }
    //
    // public async Task<IResult<UploaderResponse>> UpdateFileAsync(FileDto fileDto, string name)
    // {
    //     if (fileDto.File == null! ||
    //         fileDto.File.Length == 0)
    //         return new ErrorResult<UploaderResponse>("file is null");
    //
    //
    //     var combinedPath = Path.Combine(_uploadsFolder, fileDto.ParentDir.ToString());
    //
    //     if (!File.Exists(combinedPath))
    //         Directory.CreateDirectory(combinedPath);
    //
    //
    //     _uploadsFolder = combinedPath;
    //
    //
    //     var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileDto.File.FileName)}";
    //     var filePath = Path.Combine(_uploadsFolder, newFileName);
    //
    //     await using (var stream = new FileStream(filePath, FileMode.Create))
    //         await fileDto.File.CopyToAsync(stream);
    //
    //     //burayı düzelt
    //
    //     if (!string.IsNullOrWhiteSpace(name))
    //         if (File.Exists(Path.Combine(_uploadsFolder, name)))
    //             File.Delete(Path.Combine(_uploadsFolder, name));
    //
    //
    //     return new SuccessResult<UploaderResponse>(new UploaderResponse
    //     {
    //         Name = newFileName,
    //         Size = fileDto.File.Length
    //     });
    // }
    //
    public IResult DeleteFile(string name, FileDirectory directory)
    {
        var combinedPath = Path.Combine(_uploadsFolder, directory.ToString());


        if (!string.IsNullOrWhiteSpace(name))
            if (File.Exists(Path.Combine(combinedPath, name)))
                File.Delete(Path.Combine(combinedPath, name));


        return new SuccessResult("Silindi!");
    }
}