using System;
using System.Collections.Generic;

namespace TravelAndTours.Web.Models.AffiliateRewards
{
    public class AffiliateRewardsIndexViewModel
    {
        public string? ErrorMessage { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int Total { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
        public List<AffiliateRewardLogItemViewModel> Items { get; set; } = new();
    }

    public class AffiliateRewardLogItemViewModel
    {
        public int IncomeLogId { get; set; }
        public string? FromFullName { get; set; }
        public string? FromUserName { get; set; }
        public string? ToFullName { get; set; }
        public string? ToUserName { get; set; }
        public string? WalletBucketName { get; set; }
        public string? WalletBucket { get; set; }
        public decimal Amount { get; set; }
        public string? Reference { get; set; }
        public string? Remarks { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
