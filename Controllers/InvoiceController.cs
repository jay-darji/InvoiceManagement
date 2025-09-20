using InvoiceManagement.DBContext;
using InvoiceManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuggyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        public readonly ILogger<InvoiceController> _logger;
        public readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;



        public InvoiceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetInvoice()
        {
            List<InvoiceItems> invoiceItems = new List<InvoiceItems>();

            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();

                var raqQuery = "SELECT * FROM InvoiceItems";
                using var cmd = new SqlCommand(raqQuery, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    invoiceItems.Add(new InvoiceItems
                    {
                        ItemId = reader.GetInt32(0),
                        InvoiceId = reader.GetInt32(1),
                        ItemName = reader.GetString(2),
                        ItemPrice = reader.GetDecimal(3)
                    });
                }

                conn.Close();
            }

            if (invoiceItems.Count == 0)
            {
                AddDummyData();
            }

            List<Item> items = new List<Item>();
            invoiceItems.ForEach(item =>
            {
                items.Add(new Item { name = item.ItemName, price = (double)item.ItemPrice });
            });

            if (items.Count > 0) // NullReferenceException
            {
                return Ok(new { items });
            }
            return NotFound("No invoice found");
        }

        public class Item
        {
            public string name { get; set; }
            public double price { get; set; }
        }

        public static List<InvoiceItems> AddDummyData()
        {
            return new List<InvoiceItems>
            {
                new InvoiceItems { ItemId = 1, InvoiceId = 101, ItemName = "Item A", ItemPrice = 10.5m },
                new InvoiceItems { ItemId = 2, InvoiceId = 102, ItemName = "Item B", ItemPrice = 20.0m },
                new InvoiceItems { ItemId = 3, InvoiceId = 103, ItemName = "Item C", ItemPrice = 15.75m }
            };
        }
    }
}
