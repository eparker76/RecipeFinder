using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Product model representing products shape
    /// </summary>
    public class ProductModel
    {
        // Recipe id
        public string Id { get; set; }

        // Recipe Maker name
        public string Maker { get; set; }

        // Recipe image
        [JsonPropertyName("img")]
        public string Image { get; set; }

        // Recipe URL
        public string Url { get; set; }
        
        // Recipe title
        [StringLength (maximumLength: 33, MinimumLength = 1, ErrorMessage = "The Title should have a length of more than {2} and less than {1}")]
        public string Title { get; set; }

        // Recipe description
        public string Description { get; set; }

        // Recipe ratings 
        public int[] Ratings { get; set; }

        // Ingredients mapped to their respective amounts 
        public Dictionary<int, int> Ingredients { get; set; } = new Dictionary<int, int>();

        // Instructions
        [StringLength (maximumLength: 400, ErrorMessage = "Limit Instructions to {1} characters please...")]
        public string Instructions { get; set; }

        // Product Type
        public List<int> ProductType { get; set; } = new List<int>();

        // Time Stamp
        public string TimeStamp { get; set; }

        // Store the Comments entered by the users on this product
        public List<CommentModel> CommentList { get; set; } = new List<CommentModel>();

        // ToString file - serializes product to json
        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);
    }
}