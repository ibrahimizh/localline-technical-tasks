using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;

namespace ListMatch
{
    class Program
    {
        public static SupplierData[] supplierData { get; set; }
        public static BuyerData[] buyerData { get; set; }
        public static List<PotentialBuyersForSuppliers> potentialBuyersForSuppliers { get; set; }

        static void Main(string[] args)
        {
            var supplierJSONData = File.ReadAllText("SuppliersData.json");
            supplierData = JsonSerializer.Deserialize<SupplierData[]>(supplierJSONData);

            var buyerJSONData = File.ReadAllText("BuyersData.json");
            buyerData = JsonSerializer.Deserialize<BuyerData[]>(buyerJSONData);

            GetMatch();

            Console.Read();
        }

        static void GetMatch()
        {
            potentialBuyersForSuppliers = new List<PotentialBuyersForSuppliers>();
            foreach (var buyer_data in buyerData)
            {
                foreach (var supplier_data in supplierData)
                {
                    if (supplier_data.product_category_id == buyer_data.product_category_id)
                    {
                        var newPotentialBuyerForSupplier = new PotentialBuyersForSuppliers(supplier_data.supplier_id, buyer_data.buyer_id);
                        // Console.WriteLine($"Object to Compare: Supplier Id: {newPotentialBuyerForSupplier.supplier_id}, Buyer Id: {newPotentialBuyerForSupplier.buyer_id}");
                        if (!potentialBuyersForSuppliers.Contains(newPotentialBuyerForSupplier))
                        {
                            potentialBuyersForSuppliers.Add(newPotentialBuyerForSupplier);
                            Console.WriteLine($"Supplier Id: {newPotentialBuyerForSupplier.supplier_id}, Buyer Id: {newPotentialBuyerForSupplier.buyer_id}");
                        }
                        // else Console.WriteLine($"Match Exist=> Supplier Id: {newPotentialBuyerForSupplier.supplier_id}, Buyer Id: {newPotentialBuyerForSupplier.buyer_id}");
                    }
                }
            }
        }


    }

    class SupplierData
    {
        public int supplier_id { get; set; }
        public int product_category_id { get; set; }
        public int products_count { get; set; }
    }

    class BuyerData
    {
        public int buyer_id { get; set; }
        public int order_id { get; set; }
        public int product_category_id { get; set; }
    }

    class PotentialBuyersForSuppliers : IEquatable<PotentialBuyersForSuppliers>
    {
        public PotentialBuyersForSuppliers() { }
        public PotentialBuyersForSuppliers(int supplierId, int buyerId)
        {
            this.supplier_id = supplierId;
            this.buyer_id = buyerId;
        }
        public int supplier_id { get; set; }
        public int buyer_id { get; set; }

        public bool Equals(PotentialBuyersForSuppliers other)
        {
            return this.supplier_id == other.supplier_id
            && this.buyer_id == other.buyer_id;
        }
    }
}
