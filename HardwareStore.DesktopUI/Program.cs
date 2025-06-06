using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HardwareStore.Core; // Assuming InventoryItem is in this namespace

namespace HardwareStore.DesktopUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var httpClient = new HttpClient())
            {
                // Adjust the port if your API runs on a different one (e.g., 5001 for HTTPS, or other configured ports)
                httpClient.BaseAddress = new Uri("http://localhost:5000");

                Console.WriteLine("Attempting to retrieve inventory items from API...");

                try
                {
                    var response = await httpClient.GetAsync("/api/inventory");

                    if (response.IsSuccessStatusCode)
                    {
                        var items = await response.Content.ReadFromJsonAsync<List<InventoryItem>>();

                        if (items != null && items.Count > 0)
                        {
                            Console.WriteLine("\n--- Inventory Items ---");
                            foreach (var item in items)
                            {
                                Console.WriteLine($"Product ID: {item.ProductId}");
                                Console.WriteLine($"Name: {item.Name}");
                                Console.WriteLine($"Description: {item.Description}");
                                Console.WriteLine($"Quantity in Stock: {item.QuantityInStock}");
                                Console.WriteLine($"Unit Price: {item.UnitPrice:C}"); // Format as currency
                                Console.WriteLine($"Supplier: {item.Supplier}");
                                Console.WriteLine("-------------------------");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No inventory items found or the list was empty.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: Failed to retrieve items. Status code: {response.StatusCode}");
                        string responseContent = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrWhiteSpace(responseContent))
                        {
                            Console.WriteLine($"Response content: {responseContent}");
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Request Error: An error occurred while sending the request: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                    Console.WriteLine("Ensure the HardwareStore.Api project is running and accessible at the specified base address.");
                }
                catch (Exception ex) // Catch other potential errors (e.g., Json deserialization)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
