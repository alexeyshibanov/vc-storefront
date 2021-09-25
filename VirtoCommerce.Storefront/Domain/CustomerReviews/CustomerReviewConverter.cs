using VirtoCommerce.Storefront.Model.CustomerReviews;
using reviewsDto = VirtoCommerce.Storefront.AutoRestClients.CustomerReviewsModuleApi.Models;

namespace VirtoCommerce.Storefront.Domain
{
    public static partial class CustomerReviewConverter
    {
        public static CustomerReview ToCustomerReview(this reviewsDto.CustomerReview itemDto)
        {
            var result = new CustomerReview
            {
                Id = itemDto.Id,
                AuthorNickname = itemDto.AuthorNickname,
                Content = itemDto.Content,
                IsActive = itemDto.IsActive,
                ProductId = itemDto.ProductId,

                CreatedBy = itemDto.CreatedBy,
                CreatedDate = itemDto.CreatedDate,
                ModifiedBy = itemDto.ModifiedBy,
                ModifiedDate = itemDto.ModifiedDate,
            };

            return result;
        }

        public static reviewsDto.CustomerReviewSearchCriteria ToSearchCriteriaDto(this CustomerReviewSearchCriteria criteria)
        {
            var result = new reviewsDto.CustomerReviewSearchCriteria
            {
                IsActive = criteria.IsActive,
                ProductIds = criteria.ProductIds,

                Skip = criteria.Start,
                Take = criteria.PageSize,
                Sort = criteria.Sort
            };

            return result;
        }
    }
}
