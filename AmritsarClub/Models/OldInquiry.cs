using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmritsarClub.Models
{
    public class OldInquiry
    {
        public string QuotationStatus { get; set; }
        public string InquiryCode { get; set; }
        public string PartyItemName { get; set; }
        public string DrugDossageForm { get; set; }
        public string Size { get; set; }
        public string Unit { get; set; }
        public string PackingType { get; set; }
        public decimal QtyPerPack { get; set; }
        public decimal NoOfPack { get; set; }
        public decimal TotalUnit { get; set; }
        public decimal ProposedPrice { get; set; }
    }
}