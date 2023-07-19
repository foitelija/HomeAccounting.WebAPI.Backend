using HomeAccounting.Application.Commands.Categories.Requests.Commands;
using HomeAccounting.Application.Interfaces.Infrastructure;
using HomeAccounting.Application.Responses;
using HomeAccounting.Domain;
using MediatR;

namespace HomeAccounting.Application.Commands.Categories.Handlers.Commands
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, BaseServicesResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseServicesResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseServicesResponse();
            // var category = new Category(); заглушка для проверки.
            // Когда сделаю CRUD для остальных, сюда запихать логику проверки, если категория есть в ПОКУПКАХ, её удалить нельзя.
            try
            {
                var category = await _categoryRepository.GetById(request.Id);
                await _categoryRepository.Delete(category);

                response.Id = category.Id;
                response.Message = "Deleted";
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
