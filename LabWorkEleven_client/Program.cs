using Newtonsoft.Json;
using System.Text;

class Program
{
    static async Task Main()
    {
        const string baseUrl = "https://localhost:5001/api/products";

        // Создаем новый продукт (POST)
        var newProduct = new Products { ProductName = "New Product" };
        await CreateProductAsync(baseUrl, newProduct);
       
        // Получаем список продуктов (GET)
        await GetProductsAsync(baseUrl);
       
        // Обновляем продукт (PUT)
        newProduct.ProductName = "Updated Product";
        await UpdateProductAsync(baseUrl, newProduct.ProductId, newProduct);

        // Получаем обновленный список продуктов (GET)
        await GetProductsAsync(baseUrl);

        // Удаляем продукт (DELETE)
        await DeleteProductAsync(baseUrl, newProduct.ProductId);

        // Получаем окончательный список продуктов (GET)
        await GetProductsAsync(baseUrl);
    }

    static async Task CreateProductAsync(string baseUrl, Products product)
    {
        using var httpClient = new HttpClient();
        var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(baseUrl, content);
        response.EnsureSuccessStatusCode();

        Console.WriteLine("Product created successfully.");
    }

    static async Task GetProductsAsync(string baseUrl)
    {
        using var httpClient = new HttpClient();

        try
        {
            var response = await httpClient.GetStringAsync(baseUrl);
            var products = JsonConvert.DeserializeObject<Products[]>(response);

            Console.WriteLine("Product List:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductId}, Name: {product.ProductName}");
            }
            Console.WriteLine();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error getting products: {ex.Message}");
        }
    }

    static async Task UpdateProductAsync(string baseUrl, int productId, Products product)
    {
        using var httpClient = new HttpClient();
        var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
      
        var response = await httpClient.PutAsync($"{baseUrl}/{productId}", content);
        response.EnsureSuccessStatusCode();

        Console.WriteLine("Product updated successfully.");
    }

    static async Task DeleteProductAsync(string baseUrl, int productId)
    {
        using var httpClient = new HttpClient();

        var response = await httpClient.DeleteAsync($"{baseUrl}/{productId}");
        Console.WriteLine("Id" + Convert.ToString(productId));
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Product deleted successfully.");
        }
        else
        {
            Console.WriteLine($"Error deleting product. Status code: {response.StatusCode}");
        }
    }


}

public class Products
{
    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public int? SupplierID { get; set; }

    public int? CategoryID { get; set; }

    public string QuantityPerUnit { get; set; }

    public decimal? UnitPrice { get; set; }

    public short? UnitsInStock { get; set; }

    public short? UnitsOnOrder { get; set; }

    public short? ReorderLevel { get; set; }

    public bool Discontinued { get; set; }
}
