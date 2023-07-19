using HomeAccounting.Application.Commands.Categories.Requests.Commands;
using HomeAccounting.Application.Interfaces.Infrastructure;
using HomeAccounting.Application.Responses;
using MediatR;

namespace HomeAccounting.Application.Commands.Categories.Handlers.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, BaseServicesResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<BaseServicesResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseServicesResponse();
            try
            {
                var category = await _categoryRepository.GetById(request.Id);

                request.Category.Id = category.Id;
                category.Name = request.Category.Name;

                await _categoryRepository.Update(category);

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
