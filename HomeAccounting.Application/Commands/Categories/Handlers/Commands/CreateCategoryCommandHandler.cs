using HomeAccounting.Application.Commands.Categories.Requests.Commands;
using HomeAccounting.Application.Interfaces.Persistence;
using HomeAccounting.Application.Responses;
using MediatR;

namespace HomeAccounting.Application.Commands.Categories.Handlers.Commands
{

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseServicesResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseServicesResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseServicesResponse();

            try
            {
                var test = request.Category;
                var category = await _categoryRepository.Add(request.Category);

                response.Success = true;
                response.Message = "Successful.";
                response.Id = category.Id;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Something went wrong.";
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
