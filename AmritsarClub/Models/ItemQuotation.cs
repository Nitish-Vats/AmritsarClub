using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmritsarClub.Models
{
    public class ItemQuotation
    {
        public string QuotationStatus { get; set; }
        public string InquiryCode { get; set; }
        public string ItemCode { get; set; }
        public string PartyItemName { get; set; }
        public string ItemType { get; set; }
        public string CorrGenName { get; set; }
        public string DrugDossageForm { get; set; }
        public string PackingType { get; set; }
        public string Size { get; set; }
        public string Unit { get; set; }
        public decimal QtyPerPack { get; set; }
        public decimal NoOfPack { get; set; }
        public decimal TotalUnit { get; set; }
        public byte[] Product_Image { get; set; }
        public string Imagedata { get; set; }
        public decimal? ProposedPrice { get; set; }
        public decimal? ApprovedPricePerUnit { get; set; }
        public string Qty_shipr { get; set; }
        public decimal? WtOfShipr { get; set; } 
        public decimal? VolPershipr { get; set; }
        public string length { get; set; }
        public string breath { get; set; }
        public string height { get; set; }
        public string tot_shipr { get; set; }
        public decimal? tot_wt { get; set; }
        public decimal? tot_vol { get; set; }
        public decimal? FormulationUnit { get; set; }
        public decimal? ComponentPerUnit { get; set; }
        public decimal? Ex_fact_price { get; set; }
        public decimal? Total_val { get; set; }
        public decimal? Val_in_usd { get; set; }
    }
}