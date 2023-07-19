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
        private readonly IPurchaseRepository _purchaseRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IPurchaseRepository purchaseRepository)
        {
            _categoryRepository = categoryRepository;
            _purchaseRepository = purchaseRepository;
        }

        public async Task<BaseServicesResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseServicesResponse();
            try
            {
                var category = await _categoryRepository.GetById(request.Id);
                var isCategoryUsed = await _purchaseRepository.IsCategoryUsedAsync(category.Id);

                if (isCategoryUsed)
                {
                    response.Success = false;
                    response.Id = category.Id;
                    response.Message = "You cannot delete a category because it is being used";

                    return response;
                }

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
