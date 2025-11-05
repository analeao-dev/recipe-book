using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;
public class RegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository;

    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
        // Gerar hash (cripitografia) da senha
        var cryptographyPassword = new PasswordEncripter();

        // Mapear request para entidade
        var autoMapper = new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper();

        Validate(request);

        var user = autoMapper.Map<Domain.Entities.User>(request);
        user.Password = cryptographyPassword.Encrypt(request.Password);

        // Salvar entidade no banco de dados
        await _writeOnlyRepository.Add(user);

        
        return new ResponseRegisterUserJson
        {
            Name = request.Name
        };
    }

    private void Validate(RequestRegisterUserJson request)
    {
       var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessasges = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessasges);
        }
    }
}
