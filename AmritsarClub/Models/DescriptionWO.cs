using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmritsarClub.Models
{ 

    public class DescriptionWO
    {
        [JsonProperty("work_order_no")]
        public string WorkOrderNo { get; set; }

        [JsonProperty("DESC_CODE")]
        public string DescCode { get; set; }

        [JsonProperty("DESCRIPTION")]
        public string Description { get; set; }

        [JsonProperty("PARTY_CODE")]
        public string PartyCode { get; set; }

        [JsonProperty("Packing Type")]
        public string PackingType { get; set; }

        [JsonProperty("Qty. dosage unit per pack")]
        public string QtyDosageUnitPerPack { get; set; }

        [JsonProperty("No. of unit per shipper")]
        public string NoOfUnitPerShipper { get; set; }

        [JsonProperty("Gross Weight")]
        public decimal GrossWeight { get; set; }

        [JsonProperty("Volume Ship")]
        public string VolumeShip { get; set; }

        [JsonProperty("Size Per Shipper")]
        public string SizePerShipper { get; set; }

        [JsonProperty("Price Base")]
        public string PriceBase { get; set; }

        [JsonProperty("Price Per Unit")]
        public decimal PricePerUnit { get; set; }

        [JsonProperty("Currency")]
        public string Currency { get; set; }

        [JsonProperty("Order Date")]
        public string OrderDate { get; set; }
    }


}